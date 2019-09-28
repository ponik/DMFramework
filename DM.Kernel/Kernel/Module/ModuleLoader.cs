using System;
using System.Collections.Generic;
using System.Linq;
using DM.Kernel.Module.PlugIn;
using Microsoft.Extensions.DependencyInjection;

namespace DM.Kernel.Module
{
    public class ModuleLoader : IModuleLoader
    {
        public IModuleDescriptor[] LoadModules(
                   IServiceCollection services,
                   Type startupModuleType,
                   PlugInSourceList plugInSources)
        {
            Check.NotNull(services, nameof(services));
            Check.NotNull(startupModuleType, nameof(startupModuleType));
            Check.NotNull(plugInSources, nameof(plugInSources));

            var modules = LoadDescriptors(services, startupModuleType, plugInSources);
            SetDependencies(modules);

            modules = SortByDependency(modules, startupModuleType);
            ConfigureServices(modules, services);

            return modules.ToArray();
        }

        public IModuleDescriptor[] LoadModules<T>(IServiceCollection services, PlugInSourceList plugInSources)
        {
            return LoadModules(services, typeof(T), plugInSources);
        }

        protected List<IModuleDescriptor> LoadDescriptors(IServiceCollection services, Type startupModuleType, PlugInSourceList plugInSources)
        {
            List<IModuleDescriptor> modules = new List<IModuleDescriptor>();

            foreach (var moduleType in ModuleFinder.GetModuleWithDependencies(startupModuleType))
            {
                if (modules.Any(d => d.Type == moduleType)) { continue; }
                modules.Add(CreateModuleDescriptor(services, moduleType));
            }

            foreach (var moduleType in plugInSources.GetAllModules())
            {
                if (modules.Any(d => d.Type == moduleType)) { continue; }
                modules.Add(CreateModuleDescriptor(services, moduleType, true));
            }

            return modules;
        }

        protected virtual IModuleDescriptor CreateModuleDescriptor(IServiceCollection services, Type moduleType, bool isLoadedAsPlugIn = false)
        {
            return new ModuleDescriptor(moduleType, CreateAndRegisterModule(services, moduleType), isLoadedAsPlugIn);
        }

        protected virtual IFnModule CreateAndRegisterModule(IServiceCollection services, Type moduleType)
        {
            var module = (IFnModule)Activator.CreateInstance(moduleType);
            services.AddSingleton(moduleType, module);
            return module;
        }


        protected void SetDependencies(List<IModuleDescriptor> modules)
        {
            foreach (var module in modules)
            {
                SetDependencies(module, modules);
            }
        }

        protected void SetDependencies(IModuleDescriptor module, List<IModuleDescriptor> modules)
        {
            foreach (var dependedModuleType in ModuleFinder.GetAllDependsOnModules(module.Type))
            {
                var dependedModule = modules.FirstOrDefault(m => m.Type == dependedModuleType);
                if (dependedModule == null)
                {
                    throw new DMException("Could not set depended module " + dependedModuleType.AssemblyQualifiedName + " for " + module.Type.AssemblyQualifiedName);
                }

                module.AddDependency(dependedModule);
            }
        }

        protected virtual List<IModuleDescriptor> SortByDependency(List<IModuleDescriptor> modules, Type startupModuleType)
        {
            var sortedModules = modules.SortByDependencies(m => m.Dependencies);
            sortedModules.MoveItem(m => m.Type == startupModuleType, modules.Count - 1);
            return sortedModules;
        }


        protected virtual void ConfigureServices(List<IModuleDescriptor> modules, IServiceCollection services)
        {
            var context = new ModuleServiceConfigurationContext(services);
            services.AddSingleton(context);

            foreach (var module in modules)
            {
                if (module.Instance is FnModule fnModule)
                {
                    fnModule.ServiceConfigurationContext = context;
                }
            }

            //PreConfigureServices
            foreach (var module in modules.Where(m => m.Instance is IPreModuleConfigureServices))
            {
                ((IPreModuleConfigureServices)module.Instance).PreConfigureServices(context);
            }

            //ConfigureServices
            foreach (var module in modules)
            {
                if (module.Instance is FnModule fnModule)
                {
                    if (!fnModule.SkipAutoRegistration)
                    {
                        services.AddAssembly(module.Type.Assembly);
                    }
                }

                module.Instance.ConfigureServices(context);
            }

            //PostConfigureServices
            foreach (var module in modules.Where(m => m.Instance is IPostModuleConfigureServices))
            {
                ((IPostModuleConfigureServices)module.Instance).PostConfigureServices(context);
            }

            foreach (var module in modules)
            {
                if (module.Instance is FnModule fnModule)
                {
                    fnModule.ServiceConfigurationContext = null;
                }
            }
        }
    }
}
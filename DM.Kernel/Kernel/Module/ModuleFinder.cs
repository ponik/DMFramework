using System;
using System.Linq;
using DM.Kernel.Collections;

namespace DM.Kernel.Module
{
    public static class ModuleFinder
    {
        public static TypeList<IFnModule> GetModuleWithDependencies(Type startupModuleType)
        {
            var moduleTypes = new TypeList<IFnModule>();
            AddModuleAndDependenciesResursively(moduleTypes, startupModuleType);
            return moduleTypes;
        }

        private static void AddModuleAndDependenciesResursively(TypeList<IFnModule> moduleTypes, Type moduleType)
        {
            if (moduleTypes.Contains(moduleType)) { return; }

            moduleTypes.Add(moduleType);

            foreach (var dependsOnModule in GetAllDependsOnModules(moduleType))
            {
                AddModuleAndDependenciesResursively(moduleTypes, dependsOnModule);
            }
        }

        public static TypeList<IFnModule> GetAllDependsOnModules(Type moduleType)
        {
            FnModule.CheckModuleType(moduleType);

            TypeList<IFnModule> dependsOnModules = new TypeList<IFnModule>();

            var moduleDependencyProviders = moduleType
                .GetCustomAttributes(true)
                .OfType<IModuleDependencyProvider>();

            foreach (var provider in moduleDependencyProviders)
            {
                foreach (var type in provider.GetDependencies())
                {
                    if (!dependsOnModules.Contains(type))
                    {
                        dependsOnModules.Add(type);
                    }
                }
            }
            return dependsOnModules;
        }
    }
}
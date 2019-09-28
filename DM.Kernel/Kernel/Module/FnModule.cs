using System;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DM.Kernel.Module
{
    public abstract class FnModule :
        IFnModule,
        IPreModuleConfigureServices,
        IPostModuleConfigureServices,
        IOnEnginePreInitialization,
        IOnEngineInitialization,
        IOnEnginePostInitialization,
        IOnEngineShutdown
    {
        protected internal bool SkipAutoRegistration { get; protected set; }

        private ModuleServiceConfigurationContext _serviceConfigurationContext;

        protected internal ModuleServiceConfigurationContext ServiceConfigurationContext
        {
            get
            {
                if (_serviceConfigurationContext == null)
                {
                    throw new DMException($"{nameof(ModuleServiceConfigurationContext)} is only available in the {nameof(ConfigureServices)}, {nameof(PreConfigureServices)} and {nameof(PostConfigureServices)} methods.");
                }

                return _serviceConfigurationContext;
            }
            internal set => _serviceConfigurationContext = value;
        }
        public abstract void ConfigureServices(ModuleServiceConfigurationContext context);

        public static bool IsModule(Type type)
        {
            var typeInfo = type.GetTypeInfo();

            return
                typeInfo.IsClass &&
                !typeInfo.IsAbstract &&
                !typeInfo.IsGenericType &&
                typeof(IFnModule).GetTypeInfo().IsAssignableFrom(type);
        }

        public void OnEnginePostInitialization(IServiceProvider serivceProvider)
        {

        }

        public virtual void OnEngineInitialization(IServiceProvider serviceProvider)
        {

        }

        public void OnEnginePreInitialization(IServiceProvider serivceProvider)
        {

        }

        public virtual void OnEngineShutdown(IServiceProvider serviceProvider)
        {

        }

        public void PreConfigureServices(ModuleServiceConfigurationContext context)
        {

        }

        public void PostConfigureServices(ModuleServiceConfigurationContext context)
        {

        }

        internal static void CheckModuleType(Type moduleType)
        {
            if (!IsModule(moduleType))
            {
                throw new ArgumentException("Given type is not an DM module: " + moduleType.AssemblyQualifiedName);
            }
        }

        protected void Configure<TOptions>(Action<TOptions> configureOptions) 
            where TOptions : class
        {
            ServiceConfigurationContext.Services.Configure(configureOptions);
        }

        protected void Configure<TOptions>(string name, Action<TOptions> configureOptions)
            where TOptions : class
        {
            ServiceConfigurationContext.Services.Configure(name, configureOptions);
        }

        protected void Configure<TOptions>(IConfiguration configuration)
            where TOptions : class
        {
            ServiceConfigurationContext.Services.Configure<TOptions>(configuration);
        }

        protected void Configure<TOptions>(IConfiguration configuration, Action<BinderOptions> configureBinder)
            where TOptions : class
        {
            ServiceConfigurationContext.Services.Configure<TOptions>(configuration, configureBinder);
        }

        protected void Configure<TOptions>(string name, IConfiguration configuration)
            where TOptions : class
        {
            ServiceConfigurationContext.Services.Configure<TOptions>(name, configuration);
        }

        // protected void PreConfigure<TOptions>(Action<TOptions> configureOptions)
        //     where TOptions : class
        // {
        //     ServiceConfigurationContext.Services.PreConfigure(configureOptions);
        // }
    }
}
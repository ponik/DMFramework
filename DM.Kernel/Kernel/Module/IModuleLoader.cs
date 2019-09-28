using System;
using DM.Kernel.Module.PlugIn;
using Microsoft.Extensions.DependencyInjection;

namespace DM.Kernel.Module
{
    public interface IModuleLoader
    {
        IModuleDescriptor[] LoadModules(IServiceCollection services, Type startupModuleType, PlugInSourceList plugInSources);

        IModuleDescriptor[] LoadModules<T>(IServiceCollection services, PlugInSourceList plugInSources);
    }
}
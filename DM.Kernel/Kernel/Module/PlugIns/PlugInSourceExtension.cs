using System;
using System.Linq;

namespace DM.Kernel.Module.PlugIn
{
    public static class PlugInSourceExtension
    {
        public static Type[] GetAllModulesWithDependencies(this IPlugInSource plugInSource)
        {
            Check.NotNull(plugInSource, nameof(plugInSource));

            return plugInSource.GetModules()
            .SelectMany(p=>ModuleFinder.GetModuleWithDependencies(p))
            .Distinct()
            .ToArray();
        }
    }
}
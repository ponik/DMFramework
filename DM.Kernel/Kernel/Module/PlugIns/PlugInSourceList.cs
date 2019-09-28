using System;
using System.Collections.Generic;
using System.Linq;

namespace DM.Kernel.Module.PlugIn
{
    public class PlugInSourceList : List<IPlugInSource>
    {
        internal Type[] GetAllModules()
        {
            return this.SelectMany(p => p.GetAllModulesWithDependencies())
            .Distinct()
            .ToArray();
        }
    }
}
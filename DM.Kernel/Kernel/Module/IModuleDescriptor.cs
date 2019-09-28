using System;
using System.Collections.Generic;
using System.Reflection;

namespace DM.Kernel.Module
{
    public interface IModuleDescriptor
    {
        Type Type { get; }
        Assembly Assembly { get; }
        IFnModule Instance { get; }

        IReadOnlyList<IModuleDescriptor> Dependencies { get; }

        void AddDependency(IModuleDescriptor moduleDescriptor);
    }
}
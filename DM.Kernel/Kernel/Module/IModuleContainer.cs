using System.Collections.Generic;

namespace DM.Kernel.Module
{
    public interface IModuleContainer
    {
        IReadOnlyList<IModuleDescriptor> Modules { get; }
    }
}
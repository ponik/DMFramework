using System;
using System.Collections.Generic;
using System.Reflection;

namespace DM.Kernel.Module
{
    public interface IModulePacket
    {
        Type Type { get; }
        Assembly Assembly { get; }
        IDMModule Instance { get; }
        bool IsLoadedAsPlugIn { get; }

        IReadOnlyList<IModulePacket> Dependencies { get; }
    }
}
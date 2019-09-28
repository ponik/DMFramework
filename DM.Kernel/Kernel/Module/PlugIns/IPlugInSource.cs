using System;

namespace DM.Kernel.Module.PlugIn
{
    public interface IPlugInSource
    {
        Type[] GetModules();
    }
}
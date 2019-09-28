using System;

namespace DM.Kernel.Module
{
    public interface IModuleDependencyProvider
    {
        Type[] GetDependencies();
    }
}
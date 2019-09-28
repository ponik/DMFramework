using System;

namespace DM.Kernel.Module
{
    public class DependsOnAttribute : Attribute, IModuleDependencyProvider
    {
        private readonly Type[] _dependOnModuleTypes;
        public DependsOnAttribute(params Type[] dependOnModuleTypes)
        {
            _dependOnModuleTypes = dependOnModuleTypes ?? new Type[0];
        }
        public Type[] GetDependencies()
        {
            return _dependOnModuleTypes;
        }
    }
}
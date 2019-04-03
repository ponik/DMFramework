using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;

namespace DM.Kernel.Module
{
    public sealed class ModulePacket : IModulePacket
    {
        public Type Type { get; }

        public Assembly Assembly { get; }

        public IDMModule Instance { get; }

        public bool IsLoadedAsPlugIn { get; }

        public IReadOnlyList<IModulePacket> Dependencies => _dependencies.ToImmutableList();

        private readonly List<IModulePacket> _dependencies;
            
            public ModulePacket(
            Type type, 
            IDMModule instance, 
            bool isLoadedAsPlugIn)
        {
            if (!type.GetTypeInfo().IsAssignableFrom(instance.GetType()))
            {
                throw new ArgumentException($"Given module instance ({instance.GetType().AssemblyQualifiedName}) is not an instance of given module type: {type.AssemblyQualifiedName}");
            }

            Type = type;
            Assembly = type.Assembly;
            Instance = instance;
            IsLoadedAsPlugIn = isLoadedAsPlugIn;

            _dependencies = new List<IModulePacket>();
        }
    }
}
using System;
using Microsoft.Extensions.DependencyInjection;

namespace DM.Kernel.DependencyInjection
{
    public class RegisterAsAttribute : Attribute
    {
        public virtual ServiceLifetime? Lifetime { get; set; }

        public virtual bool TryRegister { get; set; }

        public virtual bool ReplaceServices { get; set; }

        public RegisterAsAttribute()
        {

        }

        public RegisterAsAttribute(ServiceLifetime lifetime)
        {
            Lifetime = lifetime;
        }
    }
}
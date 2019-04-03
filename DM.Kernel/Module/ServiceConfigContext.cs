using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace DM.Kernel.Module
{
    ///<summary>
    ///Shortcut to access service items
    ///</summary>
    public class ServiceConfigContext
    {
        IServiceCollection Services { get; }

        public IDictionary<string, object> Items { get; }

        public object this[string key]
        {
            get => Items.GetOrDefault(key);
            set => Items[key] = value;
        }

        public ServiceConfigContext(IServiceCollection services)
        {
            Services = services;
            Items = new Dictionary<string, object>();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DM.Kernel.DependencyInjection
{
    internal static class ExposedServiceExplorer
    {
        private static readonly ExposeServicesAttribute DefaultExposeServicesAttribute = new ExposeServicesAttribute
        {
            IncludeDefaults = true
        };
        internal static List<Type> GetExposedServices(Type type)
        {
            return type
                .GetCustomAttributes()
                .OfType<IExposedServiceTypesProvider>()
                .DefaultIfEmpty(DefaultExposeServicesAttribute)
                .SelectMany(p => p.GetExposedServiceTypes(type))
                .ToList();
        }
    }
}
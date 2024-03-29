﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DM.Kernel.DependencyInjection
{
    //TODO: Make DefaultConventionalRegistrar extensible, so we can only define GetLifeTimeOrNull to contribute to the convention. This can be more performant!
    public class DefaultConventionalRegistrar : ConventionalRegistrarBase
    {
        public override void AddType(IServiceCollection services, Type type)
        {
            if (IsConventionalRegistrationDisabled(type))
            {
                return;
            }

            var RegisterAsAttribute = GetRegisterAsAttributeOrNull(type);
            var lifeTime = GetLifeTimeOrNull(type, RegisterAsAttribute);

            if (lifeTime == null)
            {
                return;
            }

            var serviceTypes = ExposedServiceExplorer.GetExposedServices(type);

            // TriggerServiceExposing(services, type, serviceTypes);

            foreach (var serviceType in serviceTypes)
            {
                var serviceDescriptor = ServiceDescriptor.Describe(serviceType, type, lifeTime.Value);

                if (RegisterAsAttribute?.ReplaceServices == true)
                {
                    services.Replace(serviceDescriptor);
                }
                else if (RegisterAsAttribute?.TryRegister == true)
                {
                    services.TryAdd(serviceDescriptor);
                }
                else
                {
                    services.Add(serviceDescriptor);
                }
            }
        }

        // protected virtual void TriggerServiceExposing(IServiceCollection services, Type type, List<Type> serviceTypes)
        // {
        //     var exposeActions = services.GetExposingActionList();
        //     if (exposeActions.Any())
        //     {
        //         var args = new OnServiceExposingContext(type, serviceTypes);
        //         foreach (var action in exposeActions)
        //         {
        //             action(args);
        //         }
        //     }
        // }

        protected virtual bool IsConventionalRegistrationDisabled(Type type)
        {
            return type.IsDefined(typeof(DisableConventionalRegistrationAttribute), true);
        }

        protected virtual RegisterAsAttribute GetRegisterAsAttributeOrNull(Type type)
        {
            return type.GetCustomAttribute<RegisterAsAttribute>(true);
        }

        protected virtual ServiceLifetime? GetLifeTimeOrNull(Type type, RegisterAsAttribute registerAsAttribute)
        {
            return registerAsAttribute?.Lifetime ?? GetServiceLifetimeFromClassHierarcy(type);
        }

        protected virtual ServiceLifetime? GetServiceLifetimeFromClassHierarcy(Type type)
        {
            if (typeof(ITransientDependency).GetTypeInfo().IsAssignableFrom(type))
            {
                return ServiceLifetime.Transient;
            }

            if (typeof(ISingletonDependency).GetTypeInfo().IsAssignableFrom(type))
            {
                return ServiceLifetime.Singleton;
            }

            if (typeof(IScopedDependency).GetTypeInfo().IsAssignableFrom(type))
            {
                return ServiceLifetime.Scoped;
            }

            return null;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.BackOffice.Extensions
{
    public static class ServiceExtensions
    {
        public static T AddSettingsSingleton<T>(this IServiceCollection services, IConfiguration configuration, string settingsSection) where T : class
        {
            var settings = configuration.GetSection(settingsSection).Get<T>();
            services.AddSingleton(settings);
            return settings;
        }

        public static T AddSettingsScope<T>(this IServiceCollection services, IConfiguration configuration, string settingsSection) where T : class
        {
            var settings = configuration.GetSection(settingsSection).Get<T>();
            services.AddScoped(x => settings);
            return settings;
        }

        private static List<TypeInfo> GetTypesAssignableTo(this Assembly assembly, Type compareType)
        {
            var typeInfoList = assembly.DefinedTypes.Where(x => x.IsClass && !x.IsAbstract && x != compareType &&
                                                                x.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == compareType))
                                       ?.ToList();

            return typeInfoList;
        }

        public static void AddClassesAsImplementedInterface(this IServiceCollection services,
                                                            Assembly assembly,
                                                            Type compareType,
                                                            ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            assembly.GetTypesAssignableTo(compareType)
                    .ForEach((type) =>
                    {
                        foreach (var implementedInterface in type.ImplementedInterfaces)
                        {
                            switch (lifetime)
                            {
                                case ServiceLifetime.Scoped:
                                    services.AddScoped(implementedInterface, type);
                                    break;
                                case ServiceLifetime.Singleton:
                                    services.AddSingleton(implementedInterface, type);
                                    break;
                                case ServiceLifetime.Transient:
                                    services.AddTransient(implementedInterface, type);
                                    break;
                            }
                        }
                    });
        }
    }
}

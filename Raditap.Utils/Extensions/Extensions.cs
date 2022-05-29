using System;
using System.Reflection;
using Newtonsoft.Json;

namespace Raditap.Utilities.Extensions
{
    public static class Extensions
    {
        public static string GetJsonName<T>(this T value, string property)
        {
            var name = value.GetType().GetJsonName(property);
            return name;
        }

        public static string GetJsonName(this Type type, string propertyName)
        {
            var propInfo = type.GetProperty(propertyName);
            return propInfo?.GetCustomAttribute(typeof(JsonPropertyAttribute)) is JsonPropertyAttribute descriptionAttributes
                           ? descriptionAttributes.PropertyName
                           : propertyName;
        }
    }
}

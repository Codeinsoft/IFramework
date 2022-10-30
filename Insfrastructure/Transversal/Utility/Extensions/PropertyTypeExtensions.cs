using System;

namespace IFramework.Infrastructure.Utility.Extensions
{
    public static class PropertyTypeExtensions
    {
        public static bool IsPrimitive(this Type propertyType)
        {
            return propertyType.IsPrimitive || propertyType.IsValueType || (propertyType == typeof(string));
        }


    }
}

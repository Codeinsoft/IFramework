using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace IFramework.Infrastructure.Utility.Extensions
{
    /// <summary>
    /// Convert.To... methodlarını exception handling ile birlikte yürüten class.
    /// </summary>
    public static class ConvertToExtensions
    {
        public static T To<T>(this object obj)
        {
            Type t = typeof(T);
            if (!IsNullableType(t))
                return (T)Convert.ChangeType(obj, t);
            if (obj == null)
                return (T)(object)null;
            return (T)Convert.ChangeType(obj, Nullable.GetUnderlyingType(t));
        }

        public static T To<T>(this object value, T defaultValue)
        {
            try
            {
                return value.To<T>();
            }
            catch
            {
                return defaultValue;
            }
        }

        private static bool IsNullableType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
        public static string ToQueryString(this IEnumerable<string> items)
        {
            return items.Aggregate("", (curr, next) => curr + "mystring=" + next + "&");
        }

        public static string ToTitleCase(this string Text)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Text);
        }

        public static IList<T> ToDataList<T>(this IList<object> list)
        {
            IList<T> result = new List<T>();
            foreach (object o in list)
                result.Add((T)o);
            return result;
        }
    }
}

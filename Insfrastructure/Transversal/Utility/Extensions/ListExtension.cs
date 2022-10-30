using System;
using System.Collections.Generic;
using System.Text;

namespace IFramework.Infrastructure.Utility.Extensions
{
    public static class ListExtension
    {
        public static List<T> GetByRange<T>(this List<T> list, int start, int range)
        {

            if (list == null)
            {
                return null;
            }

            if (list.Count == 0)
            {
                return list;
            }

            var result = new List<T>();

            for (int i = start; i < start + range; i++)
            {
                T success;
                if (TryIndex<T>(list, i, out success))
                {
                    result.Add(list[i]);
                }
            }

            return result;


        }

        public static bool TryIndex<T>(this List<T> list, int index, out T result)
        {
            result = default(T);

            if (list == null)
            {
                return false;
            }

            var array = list.ToArray();
            index = Math.Abs(index);

            bool success = false;

            if (array != null && index < array.Length)
            {
                result = (T)array.GetValue(index);
                success = true;
            }

            return success;
        }

    }
}

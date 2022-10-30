using System;
using System.Collections.Generic;
using System.Text;

namespace IFramework.Infrastructure.Utility.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmptyOrWhitespace(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return true;
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                return true;
            }

            return false;
        }
    }
}

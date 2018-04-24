using System;

namespace RTWTR.Infrastructure
{
    public static class NullChecker
    {
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool IsNullEmptyOrWhitespace(this string str)
        {
            return str.IsNullOrEmpty() || string.IsNullOrWhiteSpace(str);
        }
    }
}

using System;

namespace RTWTR.Infrastructure
{
    public static class NullChecker
    {
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        public static bool IsNullOrWhitespace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }
    }
}

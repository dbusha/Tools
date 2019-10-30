using System;

namespace Tools.ExtensionMethods
{
    public static class StringExtensions
    {
        public static bool IsEqualToCI(this string value1, string value2)
        { return IsEqualToImpl(value1, value2, StringComparison.OrdinalIgnoreCase); }
        
        
        public static bool IsEqualToCS(this string value1, string value2)
        { return IsEqualToImpl(value1, value2, StringComparison.Ordinal); }


        public static bool IsNullOrWhitespace(this string value1) => string.IsNullOrWhiteSpace(value1);
            
            
        private static bool IsEqualToImpl(this string value1, string value2, StringComparison stringComparer)
        {
            if (value1 == null && value2 == null)
                return true;
            if (value1 == null || value2 == null)
                return false;
            return value1.Equals(value2, stringComparer);
        }
    }
}
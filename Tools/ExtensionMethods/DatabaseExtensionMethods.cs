using System;

namespace Tools.ExtensionMethods
{
    public static class DatabaseExtensionMethods
    {
        public static object ValueOrDBNull(this object value)
        {
            return value ?? DBNull.Value;
        }
    }
}
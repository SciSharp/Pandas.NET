using System;
using System.Collections.Generic;

public static class TypeExtended
{
    private static HashSet<Type> NumericTypes = new HashSet<Type>
    {
        typeof(Int16),
        typeof(UInt16),
        typeof(int),
        typeof(uint),
        typeof(long),
        typeof(ulong),
        typeof(double),
        typeof(decimal),
        typeof(float),
        typeof(Single)
    };

    public static bool IsNumericType(this Type type)
    {
        if(type == null)
            return false;

        return NumericTypes.Contains(type) ||
               NumericTypes.Contains(Nullable.GetUnderlyingType(type));
    }
}
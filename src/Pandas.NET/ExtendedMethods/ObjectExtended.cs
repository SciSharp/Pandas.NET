using System;
using System.Collections.Generic;

public static class ObjectExtended
{
    public static bool IsNumeric(this Object obj)
    {
        if(obj == null)
            return false;

        Type type = obj.GetType();
        return type.IsNumericType();
    }
}
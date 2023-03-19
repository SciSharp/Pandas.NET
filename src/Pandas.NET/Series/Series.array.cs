namespace PandasNet;

using System;
using System.Linq;

public partial class Series
{
    public T[] array<T>()
    {
        if (typeof(T) == typeof(float))
        {
            if (dtype == typeof(string))
            {
                return (_data as string[]).Select(x => float.Parse(x)).ToArray() as T[];
            }
            else if (dtype == typeof(int))
            {
                return (_data as int[]).Select(x => Convert.ToSingle(x)).ToArray() as T[];
            }
        }
        return _data as T[];
    }
}

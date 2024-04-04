using System;
using System.Collections.Generic;
using System.Linq;

namespace PandasNet;

public partial class Series
{
    public Series drop(int[] index)
    {
        var data = _data switch
        {
            bool[] => Copy(array<bool>(), index),
            int[] => Copy(array<int>(), index),
            float[] => Copy(array<float>(), index),
            double[] => Copy(array<double>(), index),
            DateTime[] => Copy(array<DateTime>(), index),
            string[] => Copy(array<string>(), index),
            _ => throw new NotImplementedException("")
        };

        if (_index == null)
        {
            throw new NotSupportedException("_index was null for this series.");
        }
        if (index == null)
        {
            throw new NotSupportedException("array of indexes to drop cannot be null");
        }
        if (_index.array<int>() == null)
        {
            throw new NotSupportedException("_index.array<int>() was null for this series.");
        }
        var index2 = _index.array<int>().Where(x => !index.Contains(x)).ToArray();
        return new Series(data, column: _column, index: new Series(index2));
    }

    private Array Copy<T>(T[] array, int[] excluded = null)
    {
        var data = new List<T>();
        for (int i = 0; i < array.Length; i++)
        {
            if (excluded != null && excluded.Contains(i))
                continue;
            data.Add(array[i]);
        }
        return data.ToArray();
    }
}

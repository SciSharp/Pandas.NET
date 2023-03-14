using System;

namespace PandasNet;

public partial class Series
{
    public Series copy()
    {
        var data = _data switch
        {
            bool[] => Copy(array<bool>()),
            int[] => Copy(array<int>()),
            float[] => Copy(array<float>()),
            double[] => Copy(array<double>()),
            DateTime[] => Copy(array<DateTime>()),
            string[] => Copy(array<string>()),
            _ => throw new NotImplementedException("")
        };

        return new Series(data, column: _column, index: _index?.copy());
    }
}

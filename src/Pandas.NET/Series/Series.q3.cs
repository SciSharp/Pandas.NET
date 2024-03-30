using System;

namespace PandasNet;

public partial class Series
{
    public double q3()
    {
        double p = 0.75;
        return _data switch
        {
            int[] data => Quantile<int>(p),
            float[] data => Quantile<float>(p),
            double[] data => Quantile<double>(p),
            _ => throw new NotImplementedException("")
        };
    }
}
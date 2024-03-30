using System;

namespace PandasNet;

public partial class Series
{
    public double q2()
    {
        double p = 0.5;
        return _data switch
        {
            int[] data => Quantile<int>(p),
            float[] data => Quantile<float>(p),
            double[] data => Quantile<double>(p),
            _ => throw new NotImplementedException("")
        };
    }
}
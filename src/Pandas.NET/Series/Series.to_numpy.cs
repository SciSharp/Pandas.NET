using System;
using Tensorflow.NumPy;

namespace PandasNet;

public partial class Series
{
    public NDArray to_numpy()
    {
        return _data switch
        {
            bool[] => np.array(array<bool>()),
            int[] => np.array(array<int>()),
            float[] => np.array(array<float>()),
            double[] => np.array(array<double>()),
            _ => throw new NotImplementedException("")
        };
    }
}

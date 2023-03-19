using Tensorflow.NumPy;

namespace PandasNet;

public partial class Series
{
    public static implicit operator NDArray(Series series)
        => series.to_numpy();
}

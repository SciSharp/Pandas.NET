using Tensorflow.NumPy;

namespace PandasNet;

public partial class DataFrame
{
    public static implicit operator NDArray(DataFrame df)
        => df.to_numpy();
}

using Tensorflow;

namespace PandasNet;

public partial class DataFrame
{
    public DataFrame head(int n = 5)
    {
        return this[new Slice(0, n, 1)];
    }
}

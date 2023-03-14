using Tensorflow;

namespace PandasNet;

public partial class DataFrame
{
    public DataFrame tail(int n = 5)
    {
        return this[new Slice(start: _data.Count - n, stop: _data.Count)];
    }
}

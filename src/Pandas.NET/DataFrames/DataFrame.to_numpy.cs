using System.Collections.Generic;
using System.Linq;
using Tensorflow;
using Tensorflow.NumPy;

namespace PandasNet;

public partial class DataFrame
{
    public NDArray to_numpy()
    {
        var array = new float[_index.size, _data.Count];
        // loop column
        for (var col = 0; col < _data.Count; col++)
        {
            var data = _data[col].array<float>();
            for (var row = 0; row < _index.size; row++)
            {
                array[row, col] = data[row];
            }
        }
        return array;
    }
}

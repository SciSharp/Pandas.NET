using System.Collections.Generic;
using System.Linq;

namespace PandasNet;

public partial class DataFrame
{
    public DataFrame drop(int[] index = null)
    {
        var data = new List<Series>();
        foreach (var s in _data)
        {
            var series = s.drop(index);
            data.Add(series);
        }
        var index2 = _index.array<int>().Where(x => !index.Contains(x)).ToArray();
        return new DataFrame(data, columns: _columns, index: new Series(index2));
    }
}

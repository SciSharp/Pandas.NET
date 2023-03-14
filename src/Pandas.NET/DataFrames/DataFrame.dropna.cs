using System.Collections.Generic;
using System.Linq;

namespace PandasNet;

public partial class DataFrame
{
    public DataFrame dropna()
    {
        var excludeRowIndex = new List<int>();
        for (int i = 0; i < _index.size; i++)
        {
            if (HasNullValue(_data, i))
                excludeRowIndex.Add(i);
        }

        var excludeRowIndexArray = excludeRowIndex.ToArray();
        var data = new List<Series>();
        foreach (var s in _data)
        {
            var series = s.drop(excludeRowIndexArray);
            data.Add(series);
        }
        return new DataFrame(data, columns: _columns);
    }

    private bool HasNullValue(List<Series> data, int row)
    {
        return data.Any(x => x.IsNull(row));
    }
}

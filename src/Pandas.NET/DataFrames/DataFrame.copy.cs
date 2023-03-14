using System.Collections.Generic;
using System.Linq;

namespace PandasNet;

public partial class DataFrame
{
    public DataFrame copy()
    {
        var data = new List<Series>();
        foreach (var s in _data)
        {
            data.Add(s.copy());
        }

        var columns = new List<Column>();
        foreach(var c in _columns)
        {
            columns.Add(new Column { Name = c.Name, DType = c.DType });
        }

        return new DataFrame(data, columns: columns, index: _index.copy());
    }
}

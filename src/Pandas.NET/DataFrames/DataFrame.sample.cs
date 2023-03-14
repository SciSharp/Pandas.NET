using System;
using System.Collections.Generic;
using System.Linq;

namespace PandasNet;

public partial class DataFrame
{
    public DataFrame sample(float frac= 0.8f, int random_state = 0)
    {
        var rnd = new Random(random_state);
        var n = (int)Math.Ceiling((1 - frac) * _index.size);
        var excludeRowIndexArray = new int[n];
        for (int i = 0; i < n; i++)
        {
            excludeRowIndexArray[i] = rnd.Next(0, _index.size - 1);
        }

        var data = new List<Series>();
        foreach (var s in _data)
        {
            var series = s.drop(excludeRowIndexArray);
            data.Add(series);
        }
        var index = _index.array<int>().Where(x => !excludeRowIndexArray.Contains(x)).ToArray();
        return new DataFrame(data, columns: _columns, index: new Series(index));
    }
}

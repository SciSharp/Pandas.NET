using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PandasNet
{
    public partial class DataFrame
    {
        public DataFrame transpose()
        {
            var index = new Series(_data.Select(x => x.name).ToArray());
            var data = new List<Series>();

            for (var col = 0; col < _index.size; col++)
            {
                var series = new Series(new Column
                {
                    DType = typeof(double),
                    Name = _index.GetValue(col).ToString()
                });
                series.Allocate(_columns.Count);
                series.SetIndex(index);
                data.Add(series);
            }

            for (var col = 0; col < _data.Count; col++)
            {
                for(var row = 0; row < _index.size; row++)
                {
                    data[row].SetValue(_data[col].GetValue(row), col);
                }
            }

            return new DataFrame(data, index: index);
        }
    }
}

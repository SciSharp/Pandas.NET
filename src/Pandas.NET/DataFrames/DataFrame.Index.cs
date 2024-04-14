using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using Tensorflow;

namespace PandasNet
{
    public partial class DataFrame
    {
        public DataFrame this[Slice slice]
        {
            get => Slice(slice);
        }

        public object this[int row, string columName]
        {
            get
            {
                return _data.FirstOrDefault(x => x.name == columName).GetValue(row);
            }
        }

        public Series this[string columName]
        {
            get
            {
                return _data.FirstOrDefault(x => x.name == columName);
            }

            set
            {
                _data.Remove(_data.FirstOrDefault(x => x.name == columName));
                _columns.Remove(_columns.FirstOrDefault(x => x.Name == columName));

                _data.Add(value);
                _columns.Add(new Column
                {
                    Name = columName,
                    DType = value.dtype
                });
            }
        }

        public DataFrame this[params string[] columNames]
        {
            get => new DataFrame(_data.Where(x => columNames.Contains(x.name)).ToList());
        }

        DataFrame Slice(Slice slice)
        {
            var start = slice.Start ?? 0;
            var stop = slice.Stop ?? _index.size;
            var step = slice.Step;
            var rowCount = (stop - start) / step;

            var data1 = new List<Series>();
            for (int col = 0; col < _columns.Count; col++)
            {
                var series = new Series(_columns[col]);
                series.Allocate(rowCount);
                data1.Add(series);
            }

            var data1RowIndex = 0;
            var index = new Series(new Column
            {
                Name = _index.name,
                DType = _index.dtype
            });
            index.Allocate(rowCount);
            for (int row = start; row < stop; row += step)
            {
                if (data1RowIndex >= rowCount)
                    break;

                for (int col = 0; col < _columns.Count; col++)
                {
                    data1[col].SetValue(_data[col].GetValue(row), data1RowIndex);
                }
                index.SetValue(_index.GetValue(row), data1RowIndex);
                data1RowIndex++;
            }

            foreach (var d in data1)
                d.SetIndex(index);

            return new DataFrame(data1, index, _columns);
        }
    }
}

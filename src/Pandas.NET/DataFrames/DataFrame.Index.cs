using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PandasNet
{
    public partial class DataFrame
    {
        public DataFrame this[int start, int step]
        {
            get
            {
                return Slice(start, step: step);
            }
        }

        public DataFrame this[int stop]
        {
            get
            {
                return Slice(0, stop: stop);
            }
        }

        public Series this[int row, string columName]
        {
            get
            {
                return _data.FirstOrDefault(x => x.name == columName);
            }

            set
            {
                throw new NotImplementedException("");
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
                _data.Add(value);
                _columns.Add(new Column
                {
                    Name = columName,
                    DType = value.dtype
                });
            }
        }

        DataFrame Slice(int start, int stop = -1, int step = 1)
        {
            if (stop < 0)
                stop = _index.size;

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

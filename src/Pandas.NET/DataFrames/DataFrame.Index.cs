using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PandasNet
{
    public partial class DataFrame
    {
        public DataFrame this[int start, int step = 1]
        {
            get
            {
                var rowCount = (index.Length - start) / step;

                var data1 = new List<Array>();
                for (int col = 0; col < columns.Count; col++)
                {
                    if (columns[col].DType == typeof(int))
                        data1.Add(new int[rowCount]);
                    else if (columns[col].DType == typeof(string))
                        data1.Add(new string[rowCount]);
                }

                var data1RowIndex = 0;
                for (int row = start; row < index.Length; row += step)
                {
                    for (int col = 0; col < columns.Count; col++)
                    {
                        data1[col].SetValue(data[col].GetValue(row), data1RowIndex);
                    }
                    data1RowIndex++;
                }

                return new DataFrame(data1, Enumerable.Range(0, rowCount).ToArray(), columns);
            }
        }
    }
}

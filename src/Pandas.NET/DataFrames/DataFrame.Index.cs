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

                return new DataFrame(data1, Enumerable.Range(0, data1RowIndex).ToArray(), columns);
            }
        }

        public DataFrame this[int stop]
        {
            get
            {
                var rowCount = stop;

                var data1 = new List<Array>();
                for (int col = 0; col < columns.Count; col++)
                {
                    if (columns[col].DType == typeof(int))
                        data1.Add(new int[rowCount]);
                    else if (columns[col].DType == typeof(string))
                        data1.Add(new string[rowCount]);
                }

                for (int row = 0; row < rowCount; row++)
                {
                    for (int col = 0; col < columns.Count; col++)
                    {
                        data1[col].SetValue(data[col].GetValue(row), row);
                    }
                }

                return new DataFrame(data1, Enumerable.Range(0, rowCount).ToArray(), columns);
            }
        }
    }
}

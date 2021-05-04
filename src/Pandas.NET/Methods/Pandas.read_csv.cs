using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace PandasNet
{
    public partial class Pandas
    {
        public DataFrame read_csv(string path)
        {
            var rows = File.ReadAllLines(path);
            var columns = rows[0].Split(',').Select(x => new Column
            {
                Name = x.Trim('\"')
            }).ToList();
            var index = new Series(Enumerable.Range(0, rows.Length).ToArray());

            // add columns
            var data = new List<Series>();
            for (int col = 0; col < columns.Count; col++)
            {
                columns[col].DType = InferDataType(rows[1], col);
                var series = new Series(columns[col]);
                series.Allocate(rows.Length);
                series.SetIndex(index);
                data.Add(series);
            }

            // set values
            for (int row = 1; row < rows.Length; row++)
            {
                var values = rows[row].Split(',');
                for (int col = 0; col < columns.Count; col++)
                    data[col].SetValue(values[col], row - 1);
            }

            return new DataFrame(data, index: index, columns: columns);
        }

        Type InferDataType(string row, int col)
        {
            var val = row.Split(',')[col];
            if (int.TryParse(val, out var int32))
                return typeof(int);
            else if (double.TryParse(val, out var float64))
                return typeof(double);
            else if (DateTime.TryParse(val, out var datetime))
                return typeof(DateTime);
            return typeof(string);
        }
    }
}

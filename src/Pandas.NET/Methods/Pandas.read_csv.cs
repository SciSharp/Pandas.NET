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

            // add index column
            columns.Insert(0, new Column
            {
                Name = string.Empty,
                DType = typeof(int)
            });

            var data = new List<Array>();
            data.Add(new int[rows.Length]);

            for (int col = 1; col < columns.Count; col++)
            {
                columns[col].DType = InferDataType(rows[1]);
                if(columns[col].DType == typeof(string))
                {
                    data.Add(new string[rows.Length]);
                }
            }

            for (int row = 1; row < rows.Length; row++)
            {
                data[0].SetValue(row - 1, row - 1);
                var values = rows[row].Split(',');
                for (int col = 1; col < columns.Count; col++)
                {
                    data[col].SetValue(values[col-1], row - 1);
                }
            }

            return new DataFrame(data, Enumerable.Range(0, rows.Length - 1).ToArray(), columns);
        }

        Type InferDataType(string row)
        {
            var val = row.Split(',')[0];
            if (int.TryParse(val, out var intValue))
                return typeof(int);
            if (float.TryParse(val, out var floatValue))
                return typeof(float);
            return typeof(string);
        }
    }
}

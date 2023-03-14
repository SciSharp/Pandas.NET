using System;
using System.Linq;

namespace PandasNet;

public partial class Pandas
{
    public DataFrame get_dummies(DataFrame data,
        string[] columns = null,
        string prefix = "",
        string prefix_sep = "")
    {
        foreach (var colName in columns)
        {
            // remove column
            var column = data.columns.First(x => x.Name == colName);
            data.columns.Remove(column);
            var series = data.data.First(x => x.column.Name == colName);
            data.data.Remove(series);

            // expand columns
            var values = series.array<string>();
            foreach (var col in values.Distinct())
            {
                var array = values.Select(x => x == col ? 1 : 0).ToArray();
                var newColumn = new Column { Name = col, DType = typeof(int) };
                var newSeries = new Series(array, data.index, column);
                data.columns.Add(newColumn);
                data.data.Add(newSeries);
            }
        }
        return data;
    }
}

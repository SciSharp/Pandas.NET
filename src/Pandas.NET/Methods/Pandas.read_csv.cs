using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using PandasNet.Utils;

namespace PandasNet;

public partial class Pandas
{
    public DataFrame read_csv(string path, 
        string[] names = null,
        char sep = ',', 
        char? na_values = null,
        int header = 1, 
        char? comment = null,
        bool skipinitialspace = false)
    {
        // Download for web
        if (IsWebUrl(path))
        {
            var fileName = path.Split('/').Last();
            string data_dir = Path.GetTempPath();
            Web.Download(path, data_dir, fileName);
            path = Path.Combine(data_dir, fileName);
        }

        if (names != null)
        {
            header = 0;
        }

        var rows = File.ReadAllLines(path);
        var columns = names?.Select(x => new Column
        {
            Name = x
        }).ToList() ?? rows[0].Split(sep).Select(x => new Column
        {
            Name = x.Trim('\"')
        }).ToList();
        var index = new Series(Enumerable.Range(0, rows.Length).ToArray());

        // add columns
        var data = new List<Series>();
        for (int col = 0; col < columns.Count; col++)
        {
            columns[col].DType = InferDataType(rows[header], col, sep);
            var series = new Series(columns[col]);
            series.Allocate(rows.Length);
            series.SetIndex(index);
            data.Add(series);
        }

        // set values
        for (int row = header; row < rows.Length; row++)
        {
            var str = rows[row];
            if (comment.HasValue)
            {
                str = str.Split(comment.Value).First();
            }

            if (skipinitialspace)
            {
                str = Regex.Replace(str, "( )+", " ");
            }

            var values = str.Split(sep);
            for (int col = 0; col < columns.Count; col++)
            {
                var val = values[col];
                if (na_values.HasValue && val.Equals(na_values.ToString()))
                {
                    data[col].SetNull(row);
                    break;
                }
                data[col].SetValue(val, row);
            }
        }

        return new DataFrame(data, index: index, columns: columns);
    }

    Type InferDataType(string row, int col, char sep)
    {
        var val = row.Split(sep)
            .Where(x => !string.IsNullOrEmpty(x))
            .ToArray()[col];
        if (int.TryParse(val, out var _))
            return typeof(int);
        else if (float.TryParse(val, out var _))
            return typeof(float);
        else if (double.TryParse(val, out var _))
            return typeof(double);
        return typeof(string);
    }

    bool IsWebUrl(string url)
    {
        return url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) 
            || url.StartsWith("https://", StringComparison.OrdinalIgnoreCase);
    }
}

using NumSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PandasNET.Extensions
{
    public static partial class PandasExtensions
    {
        public static DataFrame<double> read_csv(this Pandas pd, string filepath_or_buffer, string sep = ",")
        {
            var data = new List<double[]>();
            var columns = new List<string>();

            using (StreamReader reader = new StreamReader(filepath_or_buffer))
            {
                string line = String.Empty;
                while (!String.IsNullOrEmpty(line = reader.ReadLine()))
                {
                    var tokens = line.Split(',');
                    // for header
                    if(columns.Count == 0)
                    {
                        columns.AddRange(tokens);
                        continue;
                    }

                    data.Add(tokens.Select(x => double.Parse(x)).Take(tokens.Length).ToArray());
                }
            }

            var nd = new NumPy<double>().array(data.ToArray());
            var df = pd.DataFrame(nd, columns: columns);

            return df;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PandasNET.Extensions
{
    public static partial class PandasExtensions
    {
        public static DataFrame<double> read_csv(this Pandas pd, string filepath_or_buffer)
        {
            using (StreamReader reader = new StreamReader(filepath_or_buffer))
            {
                string line = String.Empty;

                while (!String.IsNullOrEmpty(line = reader.ReadLine()))
                {
                    var tokens = line.Split(',');
                    /*x1.AddRange(tokens.Select(x => double.Parse(x)).Take(tokens.Length - 1));

                    var _y = int.Parse(tokens[tokens.Length - 1]);
                    if (!labels.Contains(_y))
                    {
                        labels.Add(_y);
                    }
                    y1.Add(labels.FindIndex(l => l == _y));

                    length1d++;
                    length2d = tokens.Length - 1;*/
                }
            }

            return null;
        }
    }
}

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
        
        public static DataFrame<int,double> read_csv(this Pandas pd, string filepath_or_buffer, string sep = ",", string delimiter = ",", int? header = 0,
            List<IColumnType> dtype = null)
        {
            var data = new List<double[]>();
            string[] headers = null;

            using (StreamReader reader = new StreamReader(filepath_or_buffer))
            {
                string line = String.Empty;
                int row = 0;
                
                while (!String.IsNullOrEmpty(line = reader.ReadLine()))
                {
                    var tokens = line.Split(',');
                    // for header
                    if(row == header)
                    {
                        headers = tokens;
                        row++;
                        continue;
                    }

                    row++;
                    data.Add(tokens.Select(x => double.Parse(x)).Take(tokens.Length).ToArray());
                }
            }

            if (dtype == null) dtype = new List<IColumnType>();
            for(int i = 0; i < headers.Length; i++)
            {
                dtype.Add(ColumnHelper.Infer(headers[i], data[i]));
            }

            var nd = new NumPy<double>().array(data.ToArray());
            //var df = pd.DataFrame<int,double>(nd, columns: columns);

            return null;
        }
        
    }
}

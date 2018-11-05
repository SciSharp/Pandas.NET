using NumSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PandasNET.Extensions
{
    public static partial class PandasExtensions
    {
        public static DataFrame<T> DataFrame<T>(this Pandas pd, NDArray<T> data, IList<int> index = null, IList<string> columns = null)
        {
            var df = new DataFrame<T>();
            df.Columns.Array(columns);
            df.Data = data.Data;
            df.Shape = data.Shape;

            return df;
        }
    }
}

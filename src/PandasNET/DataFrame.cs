using NumSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace PandasNET
{
    /// <summary>
    /// Two-dimensional size-mutable, potentially heterogeneous tabular data structure with labeled axes (rows and columns).
    /// https://pandas.pydata.org/pandas-docs/stable/generated/pandas.DataFrame.html
    /// </summary>
    public class DataFrame<T> : NDArray<T>
    {
        public DataFrame()
        {
            Columns = new Index();
        }

        public Index Columns { get; set; }

        public NDArray<T> Values { get; set; }
    }
}

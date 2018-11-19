using NumSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace PandasNET.Extensions
{
    public static partial class PandasExtensions
    {
        public static Series<TIndex, TValue> Series<TIndex, TValue>(this Pandas pd, NDArray<TValue> data, NDArray<TIndex> index = null)
        {
            Series<TIndex, TValue> res = new Series<TIndex, TValue>();
            res.Values = data;
            if (index != null)
                res.Index = new Index<TIndex>(index);
            else

            return res;
        }
    }
}

using NumSharp;
using NumSharp.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PandasNET
{
    public static partial class PandasExtensions
    {
        public static Series Series(this Pandas pd, NDArray nd, Index index = null)
        {
            var res = new Series(nd);
            res.index = index;

            return res;
        }
    }
}

using NumSharp;
using NumSharp.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PandasNET
{
    public static partial class PandasExtensions
    {
        public static Index Index(this Pandas pd, params int[] items)
        {
            return new Index(items);
        }

        public static Index Index(this Pandas pd, params string[] items)
        {
            return new Index(items);
        }
    }
}

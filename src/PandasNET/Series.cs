using NumSharp;
using NumSharp.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PandasNET
{
    public class Series : PandasObject
    {
        public Series(NDArray nd)
        {
            values = nd;
        }
    }
}

using NumSharp;
using NumSharp.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PandasNET
{
    public partial class Series : PandasObject
    {
        public Index index { get; set; }

        public Series(NDArray nd)
        {
            values = nd;
        }

        public object this[int index]
        {
            get
            {
                return values[index];
            }

            set
            {
                this[index] = value;
            }
        }
    }
}

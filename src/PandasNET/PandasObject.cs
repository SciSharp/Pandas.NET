using NumSharp;
using NumSharp.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PandasNET
{
    public class PandasObject
    {
        public string name { get; set; }

        public NDArray values { get; set; }

        public Type dtype => values.dtype;

        public int ndim => values.NDim;

        public Shape shape => values.Shape;

        public int size => values.Size;
    }
}

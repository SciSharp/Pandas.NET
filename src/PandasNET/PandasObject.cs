using NumSharp;
using NumSharp.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PandasNET
{
    public class PandasObject
    {
        public Type dtype { get; set; }

        public string name { get; set; }

        public NDArray values { get; set; }

        public int ndim => values.NDim;

        public Shape shape => values.Shape;

        public int size => values.Size;
    }
}

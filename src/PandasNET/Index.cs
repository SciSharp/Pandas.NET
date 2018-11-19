using NumSharp;
using System;
using System.Collections.Generic;
using System.Text;
using PandasNET;

namespace PandasNET
{
    public class Index<T> : PandasObject<T>
    {
        public Index(NDArray<T> array) : base(array)
        {

        }
    }
}

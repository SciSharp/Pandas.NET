using NumSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace PandasNET
{
    public class Series<T> : PandasObject<T>
    {
        public Series(NDArray<T> array) : base(array)
        {

        }
    }
}

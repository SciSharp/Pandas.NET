using NumSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace PandasNET
{
    public class Series
    {
        public String Name { get; set; }

        public NDArray<object> Values { get; set; }
    }
}

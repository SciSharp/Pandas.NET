using NumSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace PandasNET
{
    public class Index : NDArray<string>
    {
        public NDArray<string> Values { get; set; }
    }
}

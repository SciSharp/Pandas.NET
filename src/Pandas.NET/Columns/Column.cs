using System;
using System.Collections.Generic;
using System.Text;

namespace PandasNet
{
    public class Column
    {
        public string Name { get; set; }
        public Type DType { get; set; }

        public override string ToString()
            => $"{Name} {DType}";
    }
}

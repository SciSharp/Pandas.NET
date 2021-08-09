using System;
using System.Collections.Generic;
using System.Text;

namespace PandasNet
{
    public class Column
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public Type DType { get; set; }

        public override string ToString()
            => $"{Name} {DType}";
    }
}

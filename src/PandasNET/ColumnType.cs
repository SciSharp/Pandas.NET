using System;
using System.Collections.Generic;
using System.Text;

namespace PandasNET
{
    public class ColumnType<T> : IColumnType
    {
        public ColumnType(string name = "")
        {
            Name = name;
        }

        public string Name { get; set; }
        public string Type => typeof(T).Name;
    }
}

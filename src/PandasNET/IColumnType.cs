using System;
using System.Collections.Generic;
using System.Text;

namespace PandasNET
{
    public interface IColumnType
    {
        string Type { get; }
        string Name { get; set; }
    }
}

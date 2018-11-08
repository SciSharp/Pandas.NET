using System;
using System.Collections.Generic;
using System.Text;

namespace PandasNET
{
    public class ColumnHelper
    {
        public static IColumnType Infer(string name, object value)
        {
            return new ColumnType<string>(name);
        }

        public static ColumnType<double> Double(string name = "")
        {
            return new ColumnType<double>(name);
        }

        public static ColumnType<int> Int32(string name = "")
        {
            return new ColumnType<int>(name);
        }

        public static ColumnType<string> String(string name = "")
        {
            return new ColumnType<string>(name);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace PandasNet
{
    public interface IColumnIndexable
    {
        IDataIndex Columns { get; }
         
        SeriesBase this[string columnLabel] { get; set; }

        IDataFrame this[params string[] columnLabels] { get; }

        SeriesBase this[int columnIndex] { get; }

        IDataFrame this[params int[] columnIndexs] { get; }
    }
}

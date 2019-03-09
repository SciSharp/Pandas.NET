using System;
using System.Collections.Generic;
using System.Text;

namespace Pandas
{
    public interface IColumnIndexable
    {
        IDataIndex Columns { get; }
         
        SeriesBase this[string columnLabel] { get; set; }

        IDataFrame this[params string[] columnLabels] { get; }

        IDataFrame this[params int[] columnLabels] { get; }
    }
}

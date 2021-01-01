using NumSharp;
using PandasNet.Impl;
using System;
using System.Collections.Generic;
using System.Text;

namespace PandasNet.Iteration.Impl
{
    public class DataFrameLoc : ILoc, IILoc
    {
        private readonly IDataFrame _dataFrame;

        public DataFrameLoc(IDataFrame dataFrame)
        {
            _dataFrame = dataFrame;
        }
        public SeriesBase this[string rowLabel]
        {
            get
            {
                var rowIndex = _dataFrame.Index.GetPosition(rowLabel);
                return this[rowIndex];
            }
        }

        public IDataFrame this[string[,] rowAndColumnLabels] => throw new NotImplementedException();

        public SeriesBase this[int row]
        {
            get
            {
                var colLength = _dataFrame.Columns.Size;
                NDArray array = new object[colLength];
                for (var i = 0; i < colLength; i++)
                {
                    array[i] = _dataFrame.Values[row, i];
                }
                return new Series(array)
                {
                    Name = _dataFrame.Index.Values.Array.GetValue(row),
                    Index = _dataFrame.Columns
                };
            }
        }

        public object this[string rowLabel, string columnLabel] => throw new NotImplementedException();
    }
}

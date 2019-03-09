using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NumSharp.Core;
using Pandas.Iteration;
using Pandas.Iteration.Impl;

namespace Pandas.Impl
{
    public partial class DataFrame<TIndex> : PandasObject, IDataFrame
    {
        public IDataIndex Index { get; internal set; }

        public IDataIndex Columns { get; internal set; }

        /// <summary>
        /// 返回ISeries, 设置可为单个值也可是数组
        /// </summary>
        /// <param name="columnLabel"></param>
        /// <returns></returns>
        public SeriesBase this[string columnLabel]
        {
            get
            {
                var columnIndex = Columns.GetPosition(columnLabel);
                return this[columnIndex];
            }
            set
            {
                var columnIndex = Columns.GetPosition(columnLabel);
                if (columnIndex == -1)
                {
                    AddColumn(columnLabel, value);
                }
                else
                {
                    this[columnIndex] = value;
                }
            }
        }

        public SeriesBase this[int columnIndex]
        {
            get
            {
                NDArray array = new NDArray(typeof(object), new Shape(_rowSize));
                for (var rowIndex = 0; rowIndex < _rowSize; rowIndex++)
                {
                    array[rowIndex] = Values[rowIndex, columnIndex];
                }
                var columnLabel = Columns.Values[columnIndex];
                return new Series(array)
                {
                    Name = columnLabel,
                    Index = Index
                };
            }
            set
            {
                SetColumnValue(columnIndex, value);
            }
        }

        #region 列设置为单个值

        public void SingleColumn<T>(string columnLabel, T singleValue)
        {
            var columnIndex = Columns.GetPosition(columnLabel);
            if (columnIndex == -1)
            {
                AddColumnLabel(columnLabel);
                AddColumnValue(singleValue);
            }
            else
            {
                SetColumnValue(columnIndex, singleValue);
            }
        }

        public void SingleColumn<T>(int columnIndex, T singleValue)
        {
            SetColumnValue(columnIndex, singleValue);
        }

        protected void SetColumnValue<T>(int columnIndex, T singleValue)
        {
            for (var i = 0; i < _rowSize; i++)
            {
                Values[i, columnIndex] = singleValue;
            }
        }

        #endregion

        #region 列设置为类数组值
        public void Column(string columnLabel, NDArray value)
        {
            var columnIndex = Columns.GetPosition(columnLabel);
            if (columnIndex == -1)
            {
                AddColumnLabel(columnLabel);
                AddColumnValue(value);
            }
            else
            {
                Column(columnIndex, value);
            }
        }

        public void Column(int columnIndex, NDArray value)
        {
            if (value.size != _rowSize)
            {
                throw new ArgumentException("输入数组的元素格式不等于dataframe的行数");
            }
            for (var i = 0; i < _rowSize; i++)
            {
                Values[i, columnIndex] = value[i];
            }
        }

        protected void SetColumnValue(int columnIndex, SeriesBase value)
        {
            Column(columnIndex, value.Values);
        }
        #endregion 

        public IDataFrame this[params string[] columnLabels]
        {
            get
            {
                var columnIndexs = Columns.GetPosition(columnLabels).ToArray();
                return this[columnIndexs];
            }
        }

        public IDataFrame this[params int[] columnIndexs]
        {
            get
            {
                var colLength = columnIndexs.Length;
                NDArray array = new object[_rowSize, colLength];
                for (var rowIndex = 0; rowIndex < _rowSize; rowIndex++)
                {
                    for (var col = 0; col < colLength; col++)
                    {
                        array[rowIndex, col] = Values[rowIndex, columnIndexs.ElementAt(col)];
                    }
                }
                if (colLength == 1)
                {
                    array = array.reshape(new Shape(_rowSize));
                }
                var columnLabels = columnIndexs.Select(x => Columns.Values[x].ToString()).ToList();
                var result = new DataFrame<TIndex>(array, this._rawIndex, columnLabels, null);
                return result;
            }
        }

        private DataFrameLoc _loc = null;
        public ILoc loc
        {
            get
            {
                if (_loc == null)
                {
                    _loc = new DataFrameLoc(this);
                }
                return _loc;
            }
        }

#pragma warning disable IDE1006 // 命名样式
        public IILoc iloc
#pragma warning restore IDE1006 // 命名样式
        {
            get
            {
                if (_loc == null)
                {
                    _loc = new DataFrameLoc(this);
                }
                return _loc;
            }
        }

        /// <summary>
        /// 切片
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public IDataFrame this[Slice s] {
            get
            {
                List<object> objs = new List<object>();
                NDArray nDArray = null;
                //验证长度
                var lengthVail = Values.size / Values.ndim;
                var length = s.End;
                if (s.End > lengthVail)
                    length = lengthVail;
                for (int i=s.Start;i< length; i+=s.Step)
                {
                    var value = Values[i] as NDArray;
                    objs.AddRange(value.Storage.GetData<object>());
                }
                nDArray=np.array(objs.ToArray(),Values.Storage.DType);
                nDArray.reshape(objs.Count/ Values.ndim, Values.ndim);
                var result= new DataFrame<TIndex>(nDArray, null, _rawColumns,_dtype);
                return result;
            }

        }

        public IDataFrame Head(int rowSize)
        {
            throw new NotImplementedException();
        }
    }
}

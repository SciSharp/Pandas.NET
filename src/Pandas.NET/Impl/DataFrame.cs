using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NumSharp;
using PandasNet.Iteration;
using PandasNet.Iteration.Impl;

namespace PandasNet.Impl
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
                Values[i, columnIndex].SetData(singleValue);
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
        public IDataFrame this[Slice s]
        {
            get
            {
                List<object> objs = new List<object>();
                List<TIndex> indexs = null;
                if (_rawIndex != null)
                    indexs = new List<TIndex>();
                NDArray nDArray = null;
                //验证长度
                var lengthVail = Values.size / Values.shape[1];
                var length = s.End;
                if (s.End > lengthVail)
                    length = lengthVail;
                //验证开始和结束位置
                VailStartAndEnd(s.Start, length, lengthVail);
                bool desc = s.Step < 0;
                int start = s.Start;
                //修正start和length,降序
                if (desc)
                {
                    start = length-1;
                    length = s.Start;
                }
                for (int i = start; Calc(i, length, desc); i += s.Step)
                {
                    //获取下标
                    int index = GetIndex(i, lengthVail);
                    if (_rawIndex != null)
                        indexs.Add(_rawIndex[index]);
                    var value = Values[index] as NDArray;
                    objs.AddRange(value.Data<object>());
                }
                nDArray = np.array(objs.ToArray(), Values.dtype);
                int ndim = Columns.Size;
                nDArray.reshape(objs.Count / ndim, ndim);
                var result = new DataFrame<TIndex>(nDArray, indexs, _rawColumns, _dtype);
                return result;
            }
        }

        /// <summary>
        /// 判断计算
        /// </summary>
        /// <param name="i"></param>
        /// <param name="length"></param>
        /// <param name="desc"></param>
        /// <param name="label"></param>
        /// <returns></returns>
        private bool Calc(int i, int length, bool desc, bool label = false)
        {
            if (desc && !label)
                return i > length;
            else if (desc && label)
            {
                return i >= length;
            }
            else if (!desc && label)
            {
                return i <= length;
            }
            else
                return i < length;
        }

        /// <summary>
        /// 获取实际的下标值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private int GetIndex(int value, int length)
        {
            if (value >= 0)
                return value;
            else
                return length + value;
        }

        /// <summary>
        /// 验证开始和结束位置
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="lengthVail"></param>
        private void VailStartAndEnd(int start, int end, int lengthVail)
        {
            if (start > end)
                throw new ArgumentException("开始位置不能大于结束位置");
            if(start*end<0)
            {
                if (Math.Abs(start) + Math.Abs(end) > lengthVail)
                    throw new ArgumentException("开始位置和结束位置之和不能大于总长度");
            }
        }

        /// <summary>
        /// 切片
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public IDataFrame this[SliceLabel s]
        {
            get
            {
                List<object> objs = new List<object>();
                List<TIndex> indexs = null;
                if (_rawIndex != null)
                    indexs = new List<TIndex>();
                NDArray nDArray = null;
                //转换获取的标签行索引
                int start = 0;
                int end = int.MaxValue;

                if (!string.IsNullOrEmpty(s.StartLabel))
                {
                    start = Index.GetPosition(s.StartLabel);
                }
                if (!string.IsNullOrEmpty(s.EndLabel))
                {
                    end = Index.GetPosition(s.EndLabel);
                }
                //验证长度
                var lengthVail = Values.size / Values.shape[1] - 1;
                var length = end;
                if (end > lengthVail)
                    length = lengthVail;

                //验证开始和结束位置
                VailStartAndEnd(start, length, lengthVail);
                bool desc = s.Step < 0;
                //修正start和length,降序
                if (desc)
                {
                    int temp = start;
                    start = length-1;
                    length = temp;
                }
                for (int i = start; Calc(i, length, desc, true); i += s.Step)
                {
                    //获取下标
                    int index = GetIndex(i, lengthVail);
                    if (_rawIndex != null)
                        indexs.Add(_rawIndex[index]);
                    var value = Values[index] as NDArray;
                    objs.AddRange(value.Data<object>());
                }
                nDArray = np.array(objs.ToArray(), Values.dtype);
                int ndim = Columns.Size;
                nDArray.reshape(objs.Count / ndim, ndim);
                var result = new DataFrame<TIndex>(nDArray, indexs, _rawColumns, _dtype);
                return result;
            }

        }



        public IDataFrame Head(int rowSize)
        {
            throw new NotImplementedException();
        }
    }
}

using NumSharp;
using PandasNet.Iteration;
using System;
using System.Collections.Generic;
using System.Text;

namespace PandasNet
{
    public interface IDataFrame : IPandasObject, IRowIndexable, IColumnIndexable, ISliceable<IDataFrame>
    {
        ILoc loc { get; }

        IILoc iloc { get; }

        IDataFrame Head(int rowSize);

        /// <summary>
        /// 设置列以及列的值；当列不存在时创建
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="columnLabel"></param>
        /// <param name="value"></param>
        void SingleColumn<T>(string columnLabel, T value);

        /// <summary>
        /// 设置列以及列的值；当列不存在时报异常
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="columnIndex"></param>
        /// <param name="value"></param>
        void SingleColumn<T>(int columnIndex, T value);

        /// <summary>
        ///  设置列以及列的值；当列不存在时创建
        /// </summary>
        /// <param name="columnLabel"></param>
        /// <param name="value"></param>
        void Column(string columnLabel, NDArray value);

        /// <summary>
        /// 设置列以及列的值；当列不存在时报异常
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <param name="value"></param>
        void Column(int columnIndex, NDArray value);
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Pandas.Iteration
{
    /// <summary>
    /// 基于标签位置的索引器
    /// </summary>
    public interface ILoc
    {
        /// <summary>
        /// 单一标签(行) 作为ISeries返回。
        /// </summary>
        /// <param name="rowLabel"></param>
        /// <returns></returns>
        SeriesBase this[string rowLabel]
        {
            get;
        }

        /// <summary>
        /// 行和列的单个标签
        /// </summary>
        /// <param name="rowLabel"></param>
        /// <param name="columnLabel"></param>
        /// <returns></returns>
        object this[string rowLabel, string columnLabel]
        {
            get;
        } 

        IDataFrame this[string[,] rowAndColumnLabels] { get; }
    }
}

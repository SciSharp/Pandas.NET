using NumSharp.Core;
using PandasNet.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PandasNet
{
    public static class DataFrameMethods
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="df"></param>
        /// <param name="other"></param>
        /// <param name="lsuffix">左边重叠字段的后缀</param>
        /// <param name="rsuffix">右边重叠字段的后缀</param>
        /// <param name="sort">结果是否根据连接键进行排序</param>
        /// <returns></returns>
        public static IDataFrame join(this IDataFrame df, IDataFrame other, string lsuffix = "", string rsuffix = "", bool sort = false)
        {
            throw new NotImplementedException();
        }

        #region groupby

        /// <summary>
        /// 分组
        /// </summary>
        /// <param name="df"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IGroupBy groupby(this IDataFrame df, string key)
        {
            return new DataFrameGroupBy(df, new Grouper(key));
        }

        /// <summary>
        /// 分组
        /// </summary>
        /// <param name="df"></param>
        /// <param name="grouper"></param>
        /// <returns></returns>
        public static IGroupBy groupby(this IDataFrame df, IGrouper grouper)
        {
            return new DataFrameGroupBy(df, grouper);
        }
        #endregion

        /// <summary>
        /// 按列名对应值进行排序
        /// </summary>
        /// <param name="df"></param>
        /// <param name="columns">列名数组</param>
        /// <param name="ascending">升序/降序，默认升序</param>
        /// <param name="kind">排序算法，默认快速排序</param>
        /// <returns></returns>
        public static IDataFrame sort_values(this IDataFrame df, string[] columns, bool ascending = true, SortKind kind = SortKind.quicksort)
        {
            var sorter = new Sorter(df, columns.ToList(), ascending, kind);
            var sort = new DataFrameSort(df, sorter);
            return sort.ExcuteSort();
        }

        /// <summary>
        /// 按列索引对应值进行排序 
        /// </summary>
        /// <param name="df"></param>
        /// <param name="indexs">列名索引</param>
        /// <param name="ascending">升序/降序，默认升序</param>
        /// <param name="kind">排序算法，默认快速排序</param>
        /// <returns></returns>
        public static IDataFrame sort_values(this IDataFrame df, int[] indexs, bool ascending = true, SortKind kind = SortKind.quicksort)
        {
            var sorter = new Sorter(df, indexs.ToList(), ascending, kind);
            var sort = new DataFrameSort(df, sorter);
            return sort.ExcuteSort();
        }
    }
}

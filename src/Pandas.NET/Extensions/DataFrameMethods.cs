using PandasNet.Impl;
using System;
using System.Collections.Generic;
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
    }
}

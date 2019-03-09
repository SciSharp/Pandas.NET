using System;
using System.Collections.Generic;
using System.Text;

namespace Pandas
{
    /// <summary>
    /// 分组结果
    /// </summary>
    public interface IGroupBy
    {
        /// <summary>
        /// 分组后值与标签的结果
        /// </summary>
        Dictionary<object, IDataIndex> Groups { get; }

        /// <summary>
        /// 分组后值与索引的结果
        /// </summary>
        Dictionary<object, IDataIndex> Indices { get; }

        /// <summary>
        /// 从分组结果中获取指定分组key的结果集
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IDataFrame GetGroup(object name);
    }
}

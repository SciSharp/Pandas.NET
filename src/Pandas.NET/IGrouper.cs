using System;
using System.Collections.Generic;
using System.Text;

namespace PandasNet
{
    public interface IGrouper
    {
        string Level { get; }

        /// <summary>
        /// 分组关键字
        /// </summary>
        string Key { get; }

        /// <summary>
        /// 如果目标选择（通过键或级别）是类似日期时间的对象，则将按指定的频率进行分组
        /// </summary>
        string Freq { get; }

        /// <summary>
        /// 分组的轴，默认0（行）
        /// </summary>
        int Axis { get; }

        /// <summary>
        /// 是否排序，默认false;
        /// </summary>
        bool Sort { get; }
    }
}

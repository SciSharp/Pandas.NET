using System;
using System.Collections.Generic;
using System.Text;

namespace Pandas.Impl
{
    public class Grouper : IGrouper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key">分组关键字</param>
        /// <param name="level"></param>
        /// <param name="freq">如果目标选择（通过键或级别）是类似日期时间的对象，则将按指定的频率进行分组</param>
        /// <param name="axis">0:行轴，1:列轴</param>
        /// <param name="sort"></param>
        /// <param name=""></param>
        public Grouper(string key, string level = null, string freq = null, int axis = 0, bool sort = false)
        {
            Key = key;
            Level = level;
            Freq = freq;
            Axis = axis;
            Sort = sort;
        }

        public string Level { get; }

        public string Key { get; }

        /// <summary>
        /// 如果目标选择（通过键或级别）是类似日期时间的对象，则将按指定的频率进行分组
        /// </summary>
        public string Freq { get; }

        /// <summary>
        /// 分组的轴，默认0（行）
        /// </summary>
        public int Axis { get; }

        public bool Sort { get; }
    }
}

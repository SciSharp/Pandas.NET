using System;
using System.Collections.Generic;
using System.Text;

namespace PandasNet
{
    public interface IDataIndex: IPandasObject
    {
        /// <summary>
        /// 获取标签值的索引下标
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        int GetPosition<T>(T key);

        /// <summary>
        /// 获取标签值的索引下标
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys"></param>
        /// <returns></returns>
        IEnumerable<int> GetPosition<T>(params T[] keys);
    }
}

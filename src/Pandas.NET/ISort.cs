using System;
using System.Collections.Generic;
using System.Text;

namespace PandasNet
{
    /// <summary>
    /// 排序结果
    /// </summary>
    public interface ISort
    {
        /// <summary>
        /// 排序后源数组行数的索引集合
        /// </summary>
        List<int> SourceIndexs { get; }

    }
}

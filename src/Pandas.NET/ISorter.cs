using System;
using System.Collections.Generic;
using System.Text;

namespace PandasNet
{
    public interface ISorter
    {
        /// <summary>
        /// 需要排序的列索引集合
        /// </summary>
        List<int> ColumnIndexs { get; }

        /// <summary>
        /// 升序/降序
        /// </summary>
        SortType Type { get; }

        /// <summary>
        /// 排序种类
        /// </summary>
        SortKind Kind { get;}
    }

    /// <summary>
    /// 排序类型
    /// </summary>
    public enum SortType
    {
        /// <summary>
        /// 升序
        /// </summary>
        ascending,
        /// <summary>
        /// 降序
        /// </summary>
        descending
    }

    public enum SortKind
    {
        /// <summary>
        /// 快速排序
        /// </summary>
        quicksort,
        /// <summary>
        /// 归并排序（暂未实现）
        /// </summary>
        mergesort,
        /// <summary>
        /// 堆排序（暂未实现）
        /// </summary>
        heapsort
    }

}

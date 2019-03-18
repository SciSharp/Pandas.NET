using System;
using System.Collections.Generic;
using System.Text;

namespace PandasNet
{
    public interface ISorter
    {
        /// <summary>
        /// 需要排序的列名集合
        /// </summary>
        List<string> ColumnNames { get;  }

        /// <summary>
        /// 需要排序的列索引集合
        /// </summary>
        List<int> ColumnIndexs { get; }

        /// <summary>
        /// 升序/降序
        /// </summary>
        bool Ascending { get; }

        /// <summary>
        /// 排序种类
        /// </summary>
        SortKind Kind { get;}
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

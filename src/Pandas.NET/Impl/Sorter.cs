using System;
using System.Collections.Generic;
using System.Text;

namespace PandasNet.Impl
{
    public class Sorter : ISorter
    {
        /// <summary>
        /// 需要排序的列名集合
        /// </summary>
        public List<string> ColumnNames { get; set; }

        /// <summary>
        /// 需要排序的列索引集合
        /// </summary>
        public List<int> ColumnIndexs { get; set; }

        /// <summary>
        /// 升序/降序,默认升序True
        /// </summary>
        public bool Ascending { get; set; } = true;

        /// <summary>
        /// 排序种类
        /// </summary>
        public SortKind Kind { get; set; }
    }

}

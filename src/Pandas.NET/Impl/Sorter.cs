using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PandasNet.Impl
{
    public class Sorter : ISorter
    {
        private IDataFrame _dataFrame;
        private List<string> _columnNames;
        private List<int> _columnIndexs;

        public Sorter(IDataFrame dataFrame, List<string> columnNames, bool ascending = true, SortKind kind = SortKind.quicksort)
        {
            _dataFrame = dataFrame;
            _columnNames = columnNames;
            Ascending = ascending;
            Kind = kind;
        }

        public Sorter(IDataFrame dataFrame, List<int> columnIndexs, bool ascending = true, SortKind kind = SortKind.quicksort)
        {
            _dataFrame = dataFrame;
            _columnIndexs = columnIndexs;
            Ascending = ascending;
            Kind = kind;
        }

        /// <summary>
        /// 需要排序的列名集合
        /// </summary>
        public List<string> ColumnNames
        {
            get
            {
                if (_columnNames != null)
                {
                    return _columnNames;
                }
                else
                {
                    var columnLabels = _columnIndexs.Select(x => _dataFrame.Columns.Values[x].ToString()).ToList();
                    return columnLabels;
                }
            }
        }

        /// <summary>
        /// 需要排序的列索引集合
        /// </summary>
        public List<int> ColumnIndexs
        {
            get
            {
                if (_columnIndexs != null)
                {
                    return _columnIndexs;
                }
                else
                {
                    var str = _dataFrame.Columns.GetPosition(ColumnNames.ToArray());
                    return str.ToList();
                }
            }
        }

        /// <summary>
        /// 升序/降序,默认升序True
        /// </summary>
        public bool Ascending { get; }

        /// <summary>
        /// 排序种类
        /// </summary>
        public SortKind Kind { get; }
    }

}

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

        public Sorter(IDataFrame dataFrame, List<string> columnNames, SortType type=SortType.ascending, SortKind kind = SortKind.quicksort)
        {
            _dataFrame = dataFrame;
            _columnNames = columnNames;
            Type = type;
            Kind = kind;
        }

        public Sorter(IDataFrame dataFrame, List<int> columnIndexs, SortType type = SortType.ascending, SortKind kind = SortKind.quicksort)
        {
            _dataFrame = dataFrame;
            _columnIndexs = columnIndexs;
            Type = type;
            Kind = kind;
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
                    var str = _dataFrame.Columns.GetPosition(_columnNames.ToArray());
                    return str.ToList();
                }
            }
        }

        /// <summary>
        /// 升序/降序,默认升序True
        /// </summary>
        public SortType Type { get; }

        /// <summary>
        /// 排序种类
        /// </summary>
        public SortKind Kind { get; }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;

namespace PandasNet
{
    public partial class DataFrame
    {
        private List<Series> _data;
        public List<Series> data => _data;

        private Series _index;
        public Series index 
        { 
            get => _index;
            set => _index = value;
        }

        private List<Column> _columns;
        public List<Column> columns => _columns;

        public int ndim => _shape.Length;

        private int[] _shape;
        public int[] shape => _shape;

        public DataFrame(List<Series> data, Series index = null, List<Column> columns = null, bool copy = false)
        {
            if(index == null)
            {
                index = new Series(Enumerable.Range(0, data[0].size).ToArray());
            }

            if(columns == null)
            {
                columns = data.Select(x => x.column).ToList();
            }

            _data = data;
            _index = index;
            _columns = columns;

            _shape = new int[]
            {
                index.size,
                columns.Count
            };

            foreach (var s in _data)
            {
                s.SetIndex(_index);
            }

        }
    }
}

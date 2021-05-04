using System;
using System.Collections.Generic;
using System.Linq;

namespace PandasNet
{
    public partial class DataFrame
    {
        private List<Series> _data;
        private Series _index;
        private List<Column> _columns;

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
        }
    }
}

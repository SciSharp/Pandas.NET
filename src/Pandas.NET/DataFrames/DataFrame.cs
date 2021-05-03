using System;
using System.Collections.Generic;
using System.Text;

namespace PandasNet
{
    public partial class DataFrame
    {
        List<Array> data;
        Array index;
        List<Column> columns;

        public DataFrame(List<Array> data, Array index = null, List<Column> columns = null, bool copy = false)
        {
            this.data = data;
            this.index = index;
            this.columns = columns;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PandasNet
{
    public partial class DataFrame
    {
        public Series mean()
        {
            var index = new Series(_data.Select(x => x.column.Name).ToArray());
            var series = new Series(_data.Select(x => x.mean()).ToArray());
            series.SetIndex(index);
            return series;
        }

        public Series std()
        {
            var index = new Series(_data.Select(x => x.column.Name).ToArray());
            var series = new Series(_data.Select(x => x.std()).ToArray());
            series.SetIndex(index);
            return series;
        }
    }
}

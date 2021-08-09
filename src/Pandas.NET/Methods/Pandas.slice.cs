using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Globalization;
using Tensorflow;

namespace PandasNet
{
    public partial class Pandas
    {
        public Slice slice(int start, int? stop = null, int step = 1) => new Slice(start, stop, step);
    }
}

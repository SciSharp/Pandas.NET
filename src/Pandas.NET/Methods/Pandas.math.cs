using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Globalization;

namespace PandasNet
{
    public partial class Pandas
    {
        public float pi => (float)Math.PI;

        public Series sin(Series series) => series.sin();
        public Series cos(Series series) => series.cos();
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Tensorflow;

namespace PandasNet
{
    public partial class DataFrame
    {
        public DataFrame head()
        {
            return this[new Slice(0, 5, 1)];
        }
    }
}

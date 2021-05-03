using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PandasNet
{
    public partial class DataFrame
    {
        public DataFrame head()
        {
            return this[5];
        }
    }
}

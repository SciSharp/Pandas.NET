using NumSharp;
using NumSharp.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PandasNET
{
    public partial class Series : PandasObject
    {
        public Index index { get; set; }

        public Series(NDArray nd)
        {
            values = nd;
        }

        public object this[int index]
        {
            get
            {
                return values[index];
            }

            set
            {
                this[index] = value;
            }
        }

        public object this[string idx]
        {
            get
            {
                int pos = -1;

                switch (dtype.Name)
                {
                    case "Double":
                        pos = Array.IndexOf(index.values.Data<double>(), idx);
                        return values.Data<double>()[pos];
                }

                return null;
            }
        }
    }
}

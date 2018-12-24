using NumSharp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PandasNET
{
    public partial class Series
    {
        /// <summary>
        /// Return the first n rows.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public Series head(int n = 5)
        {
            var nd = new NDArray(dtype, n);

            switch(dtype.Name)
            {
                case "Double":
                    nd.Storage.SetData(values.Data<double>().Take(n).ToArray());
                    break;
            }

            Series se = new Series(nd);

            return se;
        }
    }
}

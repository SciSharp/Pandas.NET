using NumSharp.Core;
using System;
using System.Collections.Generic;
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
            Series se = null;
            var nd = new NDArray(dtype);

            switch(dtype.Name)
            {
                case "Double":
                    se = new Series(nd);
                    break;
            }

            return se;
        }
    }
}

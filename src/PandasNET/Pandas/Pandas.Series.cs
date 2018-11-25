using NumSharp;
using NumSharp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PandasNET
{
    public static partial class PandasExtensions
    {
        public static Series Series(this Pandas pd, NDArray nd, Index index = null)
        {
            var res = new Series(nd);
            res.index = index;

            return res;
        }

        public static Series Series(this Pandas pd, object obj)
        {
            Series res = null;
            Type type = obj.GetType();

            if (type.Name.Contains("AnonymousType"))
            {
                var properties = type.GetProperties();

                var inferredDtype = properties[0].GetValue(obj).GetType();

                var nd = new NDArray(inferredDtype, properties.Length);

                res = new Series(nd);
                res.values.Set(properties.Select(x => x.GetValue(obj)).ToArray());
                res.index = new Index(properties.Select(x => x.Name).ToArray());
            }

            return res;
        }
    }
}

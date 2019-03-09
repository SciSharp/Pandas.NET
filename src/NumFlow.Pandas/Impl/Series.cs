using NumSharp.Core;
using Pandas.Iteration;
using Pandas.Iteration.Impl;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pandas.Impl
{
    public class Series : SeriesBase
    {
        public Series(NDArray nd)
        {
            Values = nd;
        }

        public override object this[int index]
        {
            get
            {
                return Values[index];
            }
            set
            {
                Values[index] = value;
            }
        }

        public override object this[string idx]
        {
            get
            {
                int pos = -1;
                pos = Array.IndexOf(Index.Values.Data<string>(), idx);
                return Values[pos];
            }
        }

        public override SeriesBase AsType<T>(bool copy = true)
        {
            throw new NotImplementedException();
        }

        //public static  implicit operator Series(int x)
        //{
        //    var series = new Series(new int[] { x });
        //    return series;
        //}

    }
}

using NumSharp.Core;
using Pandas.Impl;
using Pandas.Iteration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pandas
{
    public abstract class SeriesBase : PandasObject, IPandasObject, IRowIndexable
    {
        public IDataIndex Index { get; set; }

        public IDataFrame this[Slice s] => throw new NotImplementedException();



        /// <summary>
        ///  转换为指定的dtype
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="copy"></param>
        /// <returns></returns>
        public abstract SeriesBase AsType<T>(bool copy = true);

        public abstract object this[int index] { get; set; }

        public abstract object this[string idx] { get; }

        public static NDArray operator +(SeriesBase a, SeriesBase b)
        {
            if (a.Shape != b.Shape)
            {
                throw new Exception("相加的Series长度Shape不相等");
            }
            NDArray nd = new NDArray(typeof(object), a.Shape);

            for (var i = 0; i < a.Size; i++)
            {
                if (a[i] is string || b[i] is string)
                {
                    nd[i] = a[i].ToString() + b[i].ToString();
                }
                else
                {
                    try
                    {
                        nd[i] = Convert.ToDecimal(a[i]) + Convert.ToDecimal(b[i]);
                    }
                    catch (InvalidCastException)
                    {
                        nd[i] = null;
                    }
                }
            }
            return nd;
        }

        public static NDArray operator -(SeriesBase a, SeriesBase b)
        {
            if (a.Shape != b.Shape)
            {
                throw new Exception("相加的Series长度Shape不相等");
            }
            NDArray nd = new NDArray(typeof(object), a.Shape);
            for (var i = 0; i < a.Size; i++)
            {
                if (a[i] is string || b[i] is string)
                {
                    nd[i] = null;
                }
                else
                {
                    try
                    {
                        nd[i] = Convert.ToDecimal(a[i]) - Convert.ToDecimal(b[i]);
                    }
                    catch (InvalidCastException)
                    {
                        nd[i] = null;
                    }
                }
            }
            return nd;
        }

        public static NDArray operator *(SeriesBase a, SeriesBase b)
        {
            if (a.Shape != b.Shape)
            {
                throw new Exception("相加的Series长度Shape不相等");
            }
            NDArray nd = new NDArray(typeof(object), a.Shape);
            for (var i = 0; i < a.Size; i++)
            {
                try
                {
                    nd[i] = Convert.ToDecimal(a[i]) * Convert.ToDecimal(b[i]);
                }
                catch (InvalidCastException)
                {
                    nd[i] = null;
                }
            }
            return nd;
        }

        public static NDArray operator /(SeriesBase a, SeriesBase b)
        {
            if (a.Shape != b.Shape)
            {
                throw new Exception("相加的Series长度Shape不相等");
            }
            NDArray nd = new NDArray(typeof(object), a.Shape);
            for (var i = 0; i < a.Size; i++)
            {
                try
                {
                    nd[i] = Convert.ToDecimal(a[i]) / Convert.ToDecimal(b[i]);
                }
                catch (DivideByZeroException)
                {
                    nd[i] = null;
                }
            }
            return nd;
        }
    }
}

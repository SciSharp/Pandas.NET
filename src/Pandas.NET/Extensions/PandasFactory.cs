using NumSharp;
using PandasNet.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PandasNet
{
    public static class PandasFactory
    {
        #region Series
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pd"></param>
        /// <param name="data"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static SeriesBase Series(this Pandas pd, Array data)
        {
            Series series = new Series(data);
            return series;
        }

        public static SeriesBase Series(this Pandas pd, NDArray data)
        {
            Series series = new Series(data);
            return series;
        }

        public static SeriesBase Series<T>(this Pandas pd, T data)
        {
            if (data is Array)
            {
              return pd.Series(data as Array);
            }
            Type type = data.GetType();
            var properties = type.GetProperties();
            var nd = properties.Select(x => x.GetValue(data)).ToArray();
            Series res = new Series(new NDArray(nd))
            {
                Index = new DataIndex(properties.Select(x => x.Name).ToArray())
            };
            return res;
        }


        #endregion

        #region DataFrame
        public static IDataFrame DataFrame<TIndex>(this Pandas pd, NDArray data, IList<TIndex> index, IList<string> columns, Type dtype)
        {
            return new DataFrame<TIndex>(data, index, columns, dtype);
        }

        public static IDataFrame DataFrame(this Pandas pd, NDArray data, IList<string> index, IList<string> columns, Type dtype)
        {
            return pd.DataFrame<string>(data, index, columns, dtype);
        }

        public static IDataFrame DataFrame(this Pandas pd, IDictionary<string, NDArray> data, IList<string> index = null)
        {
            return pd.DataFrame<string>(data, index);
        }

        public static IDataFrame DataFrame<TIndex>(this Pandas pd, IDictionary<string, NDArray> data, IList<TIndex> index = null)
        {
            return new DataFrame<TIndex>(data, index);
        }

        public static IDataFrame DataFrame<T>(this Pandas pd, IList<T> data, IList<string> index = null)
        {
            var type = typeof(T);
            if(type.FullName == "System.Object")
            {
                type = data[0].GetType();
            }
            var props = type.GetProperties();
            var columnSize = props.Count();
            var rowSize = data.Count();
            var nd = new NDArray(typeof(object), new Shape(rowSize, columnSize));
            for (var i = 0; i < rowSize; i++)
            {
                for (var p = 0; p < columnSize; p++)
                {
                    nd[i, p].SetData(props[p].GetValue(data[i]));
                }
            }
            return pd.DataFrame(nd, index, props.Select(x => x.Name).ToArray(), typeof(object));
        }

        #endregion
    }
}

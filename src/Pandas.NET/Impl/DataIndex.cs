using NumSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace PandasNet.Impl
{
    public class DataIndex : PandasObject, IDataIndex
    {
        public DataIndex(NDArray array)
        {
            Values = array;
        }

        public DataIndex(params int[] items)
        {
            Values = new NDArray(typeof(int), items.Length);
        }

        public DataIndex(params string[] items)
        {
            Values = new NDArray(typeof(string), items.Length);
            Values.SetData(items);
        }

        public int GetPosition<T>(T key)
        {
            var data = Values.Data<T>();
            var pos = Array.IndexOf(Values.Data<T>(), key);
            return pos;
        }

        public IEnumerable<int> GetPosition<T>(params T[] keys)
        {
            var values = Values.Data<T>();
            if (values == null)
            {
                yield break;
            }
            foreach (var idx in keys)
            {
                var pos = Array.IndexOf(values, idx);
                yield return pos;
            }
        }
    }
}

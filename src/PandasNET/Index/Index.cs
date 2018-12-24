using NumSharp;
using System;
using System.Collections.Generic;
using System.Text;
using PandasNET;
using NumSharp.Core;

namespace PandasNET
{
    /// <summary>
    /// Immutable ndarray implementing an ordered, sliceable set. 
    /// </summary>
    public class Index : PandasObject
    {
        public Index()
        {

        }

        public Index(NDArray array)
        {

        }

        public Index(params int[] items)
        {
            values = new NDArray(typeof(int), items.Length);
        }

        public Index(params string[] items)
        {
            values = new NDArray(typeof(string), items.Length);
            values.Storage.SetData(items);
        }
    }
}

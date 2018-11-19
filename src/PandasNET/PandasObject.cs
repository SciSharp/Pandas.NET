using NumSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace PandasNET
{
    public class PandasObject<T>
    {
        public string Name { get; set; }
        public NDArray<T> Values { get; set; }
        public int NDim => Values.NDim;
        public Shape Shape => Values.Shape;
        public int Size => Values.Size;
        public Type DType => typeof(T);

        public PandasObject(NDArray<T> array)
        {
            Values = array;
        }
    }
}

using System;
using NumSharp;

namespace PandasNet
{
    public interface IPandasObject
    {
        Type DType { get; }
        object Name { get; set; }
        int NDIM { get; }
        Shape Shape { get; }
        int Size { get; }
        NDArray Values { get; set; }
    }
}
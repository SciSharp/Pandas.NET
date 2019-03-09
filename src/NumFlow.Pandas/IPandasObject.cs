using System;
using NumSharp.Core;

namespace Pandas
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
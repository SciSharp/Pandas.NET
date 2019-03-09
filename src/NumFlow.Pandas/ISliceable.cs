using System;
using System.Collections.Generic;
using System.Text;

namespace Pandas
{
    /// <summary>
    /// 允许切片
    /// </summary>
    public interface ISliceable<T>
    {
        T this[Slice s] { get; }
    }
}

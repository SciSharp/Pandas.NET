using System;
using System.Collections.Generic;
using System.Text;

namespace PandasNet.Iteration
{
    /// <summary>
    /// 基于整数位置的索引
    /// </summary>
    public interface IILoc
    {
        SeriesBase this[int row] { get; }
    }
}

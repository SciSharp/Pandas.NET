using PandasNet.Impl;
using PandasNet.Iteration;
using System;
using System.Collections.Generic;
using System.Text;

namespace PandasNet
{
    /// <summary>
    /// 允许行索引
    /// </summary>
    public interface IRowIndexable
    {
        IDataIndex Index { get; }
    }
}

using Pandas.Impl;
using Pandas.Iteration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pandas
{
    /// <summary>
    /// 允许行索引
    /// </summary>
    public interface IRowIndexable
    {



        IDataIndex Index { get; }
}
}

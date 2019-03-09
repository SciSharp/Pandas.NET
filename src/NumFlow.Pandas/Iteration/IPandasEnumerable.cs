using System;
using System.Collections.Generic;
using System.Text;

namespace Pandas.Iteration
{
    /// <summary>
    /// 索引，迭代接口
    /// </summary>
    public interface IIndexingAndIteration : ILoc
    {
        SeriesBase Get(int key);
        SeriesBase Get(string key);

        object At(int row, int column);
    }
}

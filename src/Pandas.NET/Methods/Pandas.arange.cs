using System;
using System.Linq;

namespace PandasNet
{
    public partial class Pandas
    {
        public T[] arange<T>(int start = 0, int stop = 0, int step = 1)
        {
            return typeof(T).Name switch
            {
                "Int32" => Enumerable.Range(0, stop).Select(x => (T)Convert.ChangeType(x, TypeCode.Int32)).ToArray(),
                _ => throw new NotImplementedException("")
            };
        }
    }
}

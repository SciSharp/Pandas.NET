using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PandasNet
{
    public partial class DataFrame
    {
        public static DataFrame operator -(DataFrame a, Series b)
        {
            var data = new List<Series>();

            for (int i = 0; i < a.data.Count; i++)
            {
                data.Add(a.data[i] - b.data switch
                {
                    float[] float32 => float32[i],
                    _ => throw new NotImplementedException("")
                });
            }

            return new DataFrame(data, index: a.index, columns: a.columns);
        }

        public static DataFrame operator /(DataFrame a, Series b)
        {
            var data = new List<Series>();

            for (int i = 0; i < a.data.Count; i++)
            {
                data.Add(a.data[i] / b.data switch
                {
                    float[] float32 => float32[i],
                    _ => throw new NotImplementedException("")
                });
            }

            return new DataFrame(data, index: a.index, columns: a.columns);
        }
    }
}

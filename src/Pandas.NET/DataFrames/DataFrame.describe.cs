using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PandasNet
{
    public partial class DataFrame
    {
        public DataFrame describe()
        {
            var stat_index = new Series(new string[]
            {
                "count", "mean", "std", "min", "25%", "50%", "75%", "max"
            });

            var data = _data.Where(s => new Type[] { typeof(int[]), typeof(float[]), typeof(double[]) }.Contains(s.data.GetType()))
            .Select(x =>
            {
                var series = new Series(new double[]
                {
                        x.count(),
                        x.mean(),
                        x.std(),
                        x.min(),
                        x.q1(),
                        x.q2(),
                        x.q3(),
                        x.max()
                }, new Column
                {
                    DType = typeof(double),
                    Name = x.name
                });
                series.SetIndex(stat_index);
                return series;
            }).ToList();

            return new DataFrame(data, index: stat_index);
        }
    }
}

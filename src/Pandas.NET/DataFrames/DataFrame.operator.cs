using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Tensorflow;

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
                    double[] double64 => double64[i],
                    float[] float32 => float32[i],
                    int[] int32 => int32[i],
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
                    double[] double64 => double64[i],
                    float[] float32 => float32[i],
                    int[] int32 => int32[i],
                    _ => throw new NotImplementedException("")
                });
            }

            return new DataFrame(data, index: a.index, columns: a.columns);
        }

        public static DataFrame operator *(DataFrame a, Series b)
        {
            var data = new List<Series>();
            for(int i=0;i<a.data.Count; i++)
            {
                data.Add(a.data[i] * Convert.ToDouble(b.data.GetValue(i)));
            }
            return new DataFrame(data, index: a.index, columns: a.columns);
        }

        public static bool operator ==(DataFrame a, DataFrame b)
        {
            // do they have the same number of columns?
            if (a.data.Count != b.data.Count) { return false; }

            // do the columns match?
            if (!a.columns.SequenceEqual(b.columns)) { return false; }

            // do they have the same number of rows
            if (a.shape[0] != b.shape[0]) { return false; }

            // do the rows match?
            for (int i = 0; i < a.columns.Count; i++)
            {
                // check for matching dtypes
                if (a.data[i].dtype != b.data[i].dtype) { return false; }
                bool matched = a.data[i].data switch
                {
                    bool[] => a.data[i].array<bool>().SequenceEqual(b.data[i].array<bool>()),
                    int[] => a.data[i].array<int>().SequenceEqual(b.data[i].array<int>()),
                    float[] => a.data[i].array<float>().SequenceEqual(b.data[i].array<float>()),
                    double[] => a.data[i].array<double>().SequenceEqual(b.data[i].array<double>()),
                    string[] => a.data[i].array<string>().SequenceEqual(b.data[i].array<string>()),
                    DateTime[] => a.data[i].array<DateTime>().SequenceEqual(b.data[i].array<DateTime>()),
                    _ => throw new NotImplementedException("")
                };
                if (!matched) { return false; }
            }

            return true;
        }

        public static bool operator !=(DataFrame a, DataFrame b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            return obj is DataFrame frame &&
                   data == frame.data &&
                   columns == frame.columns &&
                   index == frame.index;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(data, columns, index);
        }
    }
}

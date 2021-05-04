using System;
using System.Linq;

namespace PandasNet
{
    public partial class Series
    {
        public static Series operator !=(Series a, double b)
        {
            if (a.data is float[] float32)
                return new Series(float32.Select(x => x != b).ToArray());
            else if (a.data is double[] float64)
                return new Series(float64.Select(x => x != b).ToArray());
            throw new NotImplementedException("");
        }

        public static Series operator ==(Series a, double b)
        {
            if (a.data is float[] float32)
                return new Series(float32.Select(x => x == b).ToArray());
            else if (a.data is double[] float64)
                return new Series(float64.Select(x => x == b).ToArray());
            throw new NotImplementedException("");
        }

        public static Series operator *(Series a, Series b)
        {
            if (a.data is float[] float32a && b.data is float[] float32b)
            {
                var data = new double[a.index.size];
                for (var i = 0; i < data.Length; i++)
                    data[i] = float32a[i] * float32b[i];
                return new Series(data);
            }
            else if (a.data is double[] float64a && b.data is double[] float64b)
            {
                var data = new double[a.index.size];
                for (var i = 0; i < data.Length; i++)
                    data[i] = float64a[i] * float64b[i];
                return new Series(data);
            }
            throw new NotImplementedException("");
        }

        public static Series operator *(Series a, double b)
        {
            if (a.data is float[] float32)
                return new Series(float32.Select(x => x * b).ToArray());
            else if (a.data is double[] float64)
                return new Series(float64.Select(x => x * b).ToArray());
            throw new NotImplementedException("");
        }

        public static Series operator /(Series a, double b)
        {
            if (a.data is float[] float32)
                return new Series(float32.Select(x => x / b).ToArray());
            else if (a.data is double[] float64)
                return new Series(float64.Select(x => x / b).ToArray());
            throw new NotImplementedException("");
        }
    }
}

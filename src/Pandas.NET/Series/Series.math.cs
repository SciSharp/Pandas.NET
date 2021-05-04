using System;
using System.Linq;

namespace PandasNet
{
    public partial class Series
    {
        public int count() 
            => _data.Length;

        public double mean()
            => sum() / count();

        public double sum() => _data switch
        {
            int[] data => data.Sum(x => (float)x),
            float[] data => data.Sum(x => (float)x),
            double[] data => data.Sum(),
            _ => throw new NotImplementedException("")
        };

        public double min() => _data switch
        {
            int[] data => data.Min(),
            float[] data => data.Min(),
            double[] data => data.Min(),
            _ => throw new NotImplementedException("")
        };

        public double max() => _data switch
        {
            int[] data => data.Max(),
            float[] data => data.Max(),
            double[] data => data.Max(),
            _ => throw new NotImplementedException("")
        };

        public double std()
        {
            var avg = mean();
            var sum = _data switch
            {
                int[] data => data.Sum(x => (x - avg) * (x - avg)),
                float[] data => data.Sum(x => (x - avg) * (x - avg)),
                double[] data => data.Sum(x => (x - avg) * (x - avg)),
                _ => throw new NotImplementedException("")
            };

            return Math.Sqrt(sum / count());
        }

        public Series cos()
        {
            var cos = _data switch
            {
                int[] data => data.Select(x => Math.Cos(x)),
                float[] data => data.Select(x => Math.Cos(x)),
                double[] data => data.Select(x => Math.Cos(x)),
                _ => throw new NotImplementedException("")
            };

            return new Series(cos.ToArray());
        }

        public Series sin()
        {
            var cos = _data switch
            {
                int[] data => data.Select(x => Math.Sin(x)),
                float[] data => data.Select(x => Math.Sin(x)),
                double[] data => data.Select(x => Math.Sin(x)),
                _ => throw new NotImplementedException("")
            };

            return new Series(cos.ToArray());
        }
    }
}

using System;
using System.Linq;

namespace PandasNet
{
    public partial class Series
    {
        public int count() 
            => _data.Length;

        public float mean()
            => sum() / count();

        public float sum() => _data switch
        {
            int[] data => data.Sum(x => (float)x),
            float[] data => data.Sum(),
            _ => throw new NotImplementedException("")
        };

        public float min() => _data switch
        {
            int[] data => data.Min(),
            float[] data => data.Min(),
            _ => throw new NotImplementedException("")
        };

        public float max() => _data switch
        {
            int[] data => data.Max(),
            float[] data => data.Max(),
            _ => throw new NotImplementedException("")
        };

        public float std()
        {
            var avg = mean();
            var sum = _data switch
            {
                int[] data => data.Sum(x => (x - avg) * (x - avg)),
                float[] data => data.Sum(x => (x - avg) * (x - avg)),
                double[] data => data.Sum(x => (x - avg) * (x - avg)),
                _ => throw new NotImplementedException("")
            };

            return (float)Math.Sqrt(sum / count());
        }

        public Series cos()
        {
            var cos = _data switch
            {
                int[] data => data.Select(x => (float)Math.Cos(x)),
                float[] data => data.Select(x => (float)Math.Cos(x)),
                double[] data => data.Select(x => (float)Math.Cos(x)),
                _ => throw new NotImplementedException("")
            };

            return new Series(cos.ToArray());
        }

        public Series sin()
        {
            var sin = _data switch
            {
                int[] data => data.Select(x => (float)Math.Sin(x)),
                float[] data => data.Select(x => (float)Math.Sin(x)),
                double[] data => data.Select(x => (float)Math.Sin(x)),
                _ => throw new NotImplementedException("")
            };

            return new Series(sin.ToArray());
        }
    }
}

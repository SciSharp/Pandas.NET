using System;
using System.Linq;

namespace PandasNet
{
    public partial class Series
    {
        public double count() 
            => _data.Length;

        public double mean()
            => sum() / count();

        public double sum() => _data switch
        {
            int[] data => data.Sum(x => (double)x),
            float[] data => data.Sum(x => (double)x),
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
    }
}

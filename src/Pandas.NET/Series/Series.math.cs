using System;
using System.Collections.Generic;
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
            int[] data => data.Sum(x => (double)x),
            float[] data => data.Sum(),
            double[] data => data.Sum(),
            _ => throw new NotImplementedException("")
        };

        public double min() => _data switch
        {
            int[] data => data.Min(),
            float[] data => data.Min(),
            double[] data => data.Min(),
            _ => throw new NotImplementedException($"typeof {_data.GetType()} is not supported")
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
            return _data switch
            {
                int[] data => SampleStandardDeviation(data),
                float[] data => SampleStandardDeviation(data),
                double[] data => SampleStandardDeviation(data),
                _ => throw new NotImplementedException("")
            };
        }

        private double PopulationStandardDeviation(double[] data)
        {
            var avg = data.Average();
            var variance = data.Average(v => Math.Pow(v - avg, 2));
            return Math.Sqrt(variance);
        }
        private double PopulationStandardDeviation(float[] data) => PopulationStandardDeviation(data.Select(x => (double)x).ToArray());
        private double PopulationStandardDeviation(int[] data) => PopulationStandardDeviation(data.Select(x => (double)x).ToArray());


        private double SampleStandardDeviation(double[] data)
        {
            var avg = data.Average();
            var variance = data.Average(v => Math.Pow(v - avg, 2));
            return Math.Sqrt(variance * data.Length / (data.Length - 1));
        }
        private double SampleStandardDeviation(float[] data) => SampleStandardDeviation(data.Select(x => (double)x).ToArray());
        private double SampleStandardDeviation(int[] data) => SampleStandardDeviation(data.Select(x => (double)x).ToArray());


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

﻿using System;
using System.Linq;

namespace PandasNet
{
    public partial class Series
    {
        public static Series operator !=(Series a, float b)
        {
            if (a.data is float[] float32)
                return new Series(float32.Select(x => x != b).ToArray());
            else if (a.data is double[] float64)
                return new Series(float64.Select(x => x != b).ToArray());
            throw new NotImplementedException("");
        }

        public static Series operator ==(Series a, float b)
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
                var data = new float[a.index.size];
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

        public static Series operator +(Series a, float b)
        {
            if (a.data is float[] float32)
                return new Series(float32.Select(x => x + b).ToArray());
            else if (a.data is double[] float64)
                return new Series(float64.Select(x => x + b).ToArray());
            throw new NotImplementedException("");
        }

        public static Series operator -(Series a, double b)
        {
            if (a.data is float[] float32)
                return new Series(float32.Select(x => x - Convert.ToSingle(b)).ToArray(), a.column);
            else if (a.data is double[] float64)
                return new Series(float64.Select(x => x - b).ToArray(), a.column);
            else if (a.data is int[] int32)
                return new Series(int32.Select(x => x - Convert.ToInt32(b)).ToArray(), a.column);
            throw new NotImplementedException("");
        }

        public static Series operator *(Series a, float b)
        {
            if (a.data is float[] float32)
                return new Series(float32.Select(x => x * b).ToArray());
            else if (a.data is double[] float64)
                return new Series(float64.Select(x => x * b).ToArray());
            throw new NotImplementedException("");
        }

        public static Series operator /(Series a, float b)
        {
            if (a.data is float[] float32)
                return new Series(float32.Select(x => x / b).ToArray());
            else if (a.data is double[] float64)
                return new Series(float64.Select(x => x / b).ToArray());
            throw new NotImplementedException("");
        }
    }
}

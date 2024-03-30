using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MathNet.Numerics.Statistics;

namespace PandasNet;
public partial class Series
{
    public double Quantile<T>(double q)
    {
        IList<double> data = new List<double>();
        foreach (var item in _data)
        {
            data.Add((double)Convert.ChangeType(item, typeof(double)));
        }

        // in testing I have found that the R7 interpolation method is the most accurate when comparing the results to pandas in python.
        // https://www.itl.nist.gov/div898/handbook/prc/section2/prc262.htm
        // R7 is the default method used in R.
        return Statistics.QuantileCustom(data, q, QuantileDefinition.R7);
    }

}
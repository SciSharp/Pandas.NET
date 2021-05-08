using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Globalization;

namespace PandasNet
{
    public partial class Pandas
    {
        public Series to_datetime(Series series, string format = null)
        {
            var data = new DateTime[series.size];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = DateTime.ParseExact(series.data.GetValue(i).ToString(), format, CultureInfo.InvariantCulture);
            }

            return new Series(data);
        }

        readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public float timestamp(DateTime value)
        {
            TimeSpan elapsedTime = value - Epoch;
            return (float)elapsedTime.TotalSeconds;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PandasNet
{
    public partial class DataFrame
    {
        /// <summary>
        /// Calculates the mean (average) of each column in the DataFrame.
        /// </summary>
        /// <returns>
        /// A new Series where the index is the column names and the values are the means.
        /// </returns>
        public Series mean()
        {
            // Create a new Series for the index, using the names of the columns in the DataFrame
            var index = new Series(_data.Select(x => x.column.Name).ToArray());

            // Create a new Series for the data, using the mean of the values in each column
            var series = new Series(_data.Select(x => x.mean()).ToArray());

            // Set the index of the data Series to be the index Series we created earlier
            series.SetIndex(index);

            // Return the data Series, which now has the column names as its index and the means as its values
            return series;
        }


        /// <summary>
        /// Calculates the standard deviation of each column in the DataFrame.
        /// </summary>
        /// <returns>
        /// A new Series where the index is the column names and the values are the standard deviations.
        /// </returns>
        public Series std()
        {
            // Create a new Series for the index, using the names of the columns in the DataFrame
            var index = new Series(_data.Select(x => x.column.Name).ToArray());

            // Create a new Series for the data, using the standard deviation of the values in each column
            var series = new Series(_data.Select(x => x.std()).ToArray());

            // Set the index of the data Series to be the index Series we created earlier
            series.SetIndex(index);

            // Return the data Series, which now has the column names as its index and the standard deviations as its values
            return series;
        }
    }
}

using System.Linq;
using Tensorflow.NumPy;
using static PandasNet.PandasApi;
using Tensorflow;
using PandasNet;
using System.Collections.Generic;
using System.Text.Json;

namespace Pandas.Test
{
    public class DataFrameMathTest
    {
        [Fact]
        public void TestMean()
        {
            // Arrange
            var dfOdd = pd.DataFrame.from_dict("{'col_1': [1, 2, 3, 4, 5], 'col_2': [6, 7, 8, 9, 10]}");
            var dfEven = pd.DataFrame.from_dict("{'col_1': [1, 2, 3, 4], 'col_2': [5, 6, 7, 8]}");

            // Act
            var meanOdd = dfOdd.mean();
            var meanEven = dfEven.mean();

            // Assert
            Assert.Equal(3.0D, meanOdd.GetValue<double>(0));
            Assert.Equal("col_1", meanOdd.index.GetValue<string>(0));
            
            Assert.Equal(8.0D, meanOdd.GetValue<double>(1));

            Assert.Equal(2.5D, meanEven.GetValue<double>(0));
            Assert.Equal(6.5D, meanEven.GetValue<double>(1));
        }
    }
}
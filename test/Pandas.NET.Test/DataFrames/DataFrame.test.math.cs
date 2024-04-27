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
            Series meanOdd = dfOdd.mean();
            Series meanEven = dfEven.mean();

            // Assert
            Assert.Equal(3.0D, (double)meanOdd["col_1"]);
            Assert.Equal(8.0D, (double)meanOdd["col_2"]);

            Assert.Equal(2.5D, (double)meanEven["col_1"]);
            Assert.Equal(6.5D, (double)meanEven["col_2"]);
        }

        [Fact]
        public void TestStd()
        {
            // Arrange
            List<Series> data = new List<Series>
            {
                new Series(new double[] { 1, 2, 3, 4, 5 }, new Column { Name = "column1", DType = typeof(double) }),
                new Series(new double[] { 2, 4, 6, 8, 10 }, new Column { Name = "column2", DType = typeof(double) })
            };
            var dataFrame = new DataFrame(data);

            // Act
            Series result = dataFrame.std();

            // Assert
            // Expected values are calculated using numpy.std
            Assert.Equal(1.5811388300841898, result["column1"]);
            Assert.Equal(3.1622776601683795, result["column2"]);
        }
    }
}
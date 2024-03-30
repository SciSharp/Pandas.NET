using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Nodes;
using PandasNet;
using static PandasNet.PandasApi;

namespace Pandas.Test
{
    public class DataFrameDescribeTest
    {
        [Fact]
        public void TestDescribe1()
        {
            var df = pd.DataFrame.from_dict(JsonSerializer.Serialize(new Dictionary<string, int[]>
            {
                {"col_1",new int[] { 1, 2, 3 }},
                {"col_2", new int[] { 4, 5, 6 }},
                {"col_3 ", new int[] { 7, 8, 9 }}
            }));

            // Act
            var result = df.describe();

            // Assert
            Assert.Equal(3, result.columns.Count); // Expecting 3 statistical columns
            Assert.Equal(8, result.shape[0]); // Expecting 8 statistical rows

            // Assert specific values for "count", "mean", "std", "min", "25%", "50%", "75%", "max"
            Assert.Equal(3, result.data[0].GetValue<double>(0)); // count
            Assert.Equal(2, result.data[0].GetValue<double>(1)); // mean
            Assert.Equal(1, result.data[0].GetValue<double>(2)); // std
            Assert.Equal(1, result.data[0].GetValue<double>(3)); // min
            Assert.Equal(1.5, result.data[0].GetValue<double>(4)); // 25%
            Assert.Equal(2, result.data[0].GetValue<double>(5)); // 50%
            Assert.Equal(2.5, result.data[0].GetValue<double>(6)); // 75%
            Assert.Equal(3, result.data[0].GetValue<double>(7)); // max
        }

        [Fact]
        public void TestDescribe2()
        {
            var df = pd.DataFrame.from_dict(JsonSerializer.Serialize(new Dictionary<string, int[]>
            {
                {"col_1",new int[] { 1, 2, 3, 4, 5 }}
            }));

            // Act
            var result = df.describe();

            // Assert
            Assert.Single(result.columns); // Expecting 1 statistical columns
            Assert.Equal(8, result.shape[0]); // Expecting 8 statistical rows

            // Assert specific values for "count", "mean", "std", "min", "25%", "50%", "75%", "max"
            Assert.Equal(5, result.data[0].GetValue<double>(0)); // count
            Assert.Equal(3, result.data[0].GetValue<double>(1)); // mean
            Assert.Equal(1.5811388300841898, result.data[0].GetValue<double>(2)); // std
            Assert.Equal(1, result.data[0].GetValue<double>(3)); // min
            Assert.Equal(2, result.data[0].GetValue<double>(4)); // 25%
            Assert.Equal(3, result.data[0].GetValue<double>(5)); // 50%
            Assert.Equal(4, result.data[0].GetValue<double>(6)); // 75%
            Assert.Equal(5, result.data[0].GetValue<double>(7)); // max
        }
    }
}
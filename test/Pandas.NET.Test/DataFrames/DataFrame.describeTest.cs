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
        public void TestDescribe()
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
            Assert.Equal(3, result.columns.Count); // Expecting 5 statistical columns
            Assert.Equal(3, result.data.Count); // Expecting 5 statistical rows

            // Assert specific values for count, mean, std, min, max
            Assert.Equal(3, result.data[0].GetValue<double>(0));
            Assert.Equal(2, result.data[0].GetValue<double>(1));
            Assert.Equal(0.81649661064147949, result.data[0].GetValue<double>(2));
            Assert.Equal(1, result.data[0].GetValue<double>(3));
            Assert.Equal(3, result.data[0].GetValue<double>(4));
        }
    }
}
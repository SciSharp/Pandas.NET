using System;
using PandasNet;
using Xunit;
using static PandasNet.PandasApi;

namespace Pandas.Test
{
    public class DataFrameDropnaTest
    {
        [Fact]
        public void TestDropna()
        {
            // Arrange
            var df = pd.DataFrame.from_dict("{'col_1': [3, 2, 1, 0], 'col_2': ['a', 'b', 'c', 'd']}");
            Assert.Equal(4, df.shape[0]);
            Assert.Equal(2, df.shape[1]);
            df["col_1"].SetNull(1);
            df["col_2"].SetNull(2);

            // Act
            var result = df.dropna();

            // Assert
            Assert.Equal(2, result.shape[0]); //rows
            Assert.Equal(2, result.shape[1]); //columns
            Assert.Equal(new int[] { 3, 0 }, result["col_1"].array<int>());
            Assert.Equal(new string[] { "a", "d" }, result["col_2"].array<string>());
        }
    }
}
using Xunit;
using static PandasNet.PandasApi;

namespace Pandas.Test
{
    public class DataFrameDropTest
    {
        [Fact]
        public void TestDrop()
        {
            // Arrange
            var df = pd.DataFrame.from_dict("{'col_1': [3, 2, 1, 0], 'col_2': ['a', 'b', 'c', 'd']}");
            var indexToDrop = new[] { 1, 3 };

            // Act
            var result = df.drop(indexToDrop);

            // Assert
            // Verify that the dropped rows are not present in the result
            Assert.DoesNotContain(2, result["col_1"].array<int>());
            Assert.DoesNotContain(0, result["col_1"].array<int>());
            Assert.DoesNotContain("b", result["col_2"].array<string>());
            Assert.DoesNotContain("d", result["col_2"].array<string>());

            // Verify that the remaining rows are present in the result
            Assert.Contains(3, result["col_1"].array<int>());
            Assert.Contains(1, result["col_1"].array<int>());
            Assert.Contains("a", result["col_2"].array<string>());
            Assert.Contains("c", result["col_2"].array<string>());
        }
    }
}
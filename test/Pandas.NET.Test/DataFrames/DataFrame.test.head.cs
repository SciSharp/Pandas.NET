using static PandasNet.PandasApi;
using Tensorflow;
using System.Linq;

namespace Pandas.Test.DataFrames
{
    public class DataFrameHeadTest
    {
        [Fact]
        public void TestHead()
        {
            // Arrange
            var df = pd.DataFrame.from_dict("{'col_1': [5,4,3, 2, 1, 0], 'col_2': ['a', 'b', 'c', 'd','e','f']}");
            var expected = df[new Slice(0, 5, 1)];

            // Act
            var actual = df.head();

            // Assert
            Assert.True(expected == actual);
        }

        [Fact]
        public void TestHead_ReturnsCorrectNumberOfRows()
        {
            // Arrange
            var df = pd.DataFrame.from_dict("{'col_1': [1, 2, 3, 4, 5], 'col_2': ['a', 'b', 'c', 'd', 'e']}");

            // Act
            var result = df.head(3);

            // Assert
            Assert.Equal(3, result.shape[0]);
        }

        [Fact]
        public void TestHead_ReturnsCorrectColumns()
        {
            // Arrange
            var df = pd.DataFrame.from_dict("{'col_1': [1, 2, 3, 4, 5], 'col_2': ['a', 'b', 'c', 'd', 'e']}");

            // Act
            var result = df.head(3);

            // Assert
            Assert.Equal(new[] { "col_1", "col_2" }, result.columns.Select(c => c.Name).ToArray());
        }
    }
}

using Tensorflow;
using static PandasNet.PandasApi;

namespace Pandas.Test
{
    public class DataFrameCopyTest
    {
        [Fact]
        public void TestCopy()
        {
            // Arrange
            var df = pd.DataFrame.from_dict("{'col_1': [3, 2, 1, 0], 'col_2': ['a', 'b', 'c', 'd']}");

            // Act
            var copiedDf = df.copy();

            // Assert
            // Verify that the copied DataFrame is not the same instance as the original DataFrame
            Assert.NotSame(df, copiedDf);

            // Verify that the columns weren't just copied by reference
            Assert.NotSame(df.columns[0], copiedDf.columns[0]);

            // Verify that the copied DataFrame has the same data as the original DataFrame
            // same count of columns
            Assert.Equal<int>(df.columns.Count, copiedDf.columns.Count);

            // same count of rows
            Assert.Equal<int>(df[0].data.Count, copiedDf[0].data.Count);
        }
    }
}
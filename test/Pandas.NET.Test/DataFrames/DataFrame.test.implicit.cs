using System.Linq;
using Tensorflow.NumPy;
using static PandasNet.PandasApi;

namespace Pandas.Test
{
    public class DataFrameImplicitTest
    {
        /// <summary>
        /// Test implicit conversion of DataFrame to NDArray.
        /// 
        /// See related StackOverflow answer if you experience issues with TensorFlow DLL.
        /// <seealso cref="https://stackoverflow.com/questions/58549580/unable-to-load-dll-tensorflow-or-one-of-its-dependencies-ml-net"/>>
        /// </summary>
        [Fact]
        public void ImplicitConversionToNDArray()
        {
            // Arrange
            var df = pd.DataFrame.from_dict("{'col_1': [5, 4, 3, 2, 1, 0]}");

            // Act

            NDArray result = df;

            // Assert
            Assert.NotNull(result);

            // Add more assertions as needed
            Assert.Equal(6, result.shape[0]);
            Assert.Equal(1, result.shape[1]);

            // ensure they are the same element-wise
            Assert.True(df.data[0].array<int>().SequenceEqual([.. result[":", 0]]));
        }
    }
}
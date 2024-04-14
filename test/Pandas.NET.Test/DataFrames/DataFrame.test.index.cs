using System.Linq;
using Tensorflow.NumPy;
using static PandasNet.PandasApi;
using Tensorflow;
using PandasNet;
using System.Collections.Generic;
using System.Text.Json;

namespace Pandas.Test
{
    public class DataFrameIndexTest
    {
        [Fact]
        public void IndexTest1_Slice()
        {
            //Arrange
            var df = pd.DataFrame.from_dict("{'col_1': [1, 2, 3, 4, 5], 'col_2': ['a', 'b', 'c', 'd', 'e']}");

            //Act
            var slicedDf = df[new Slice(1, 4, 1)];

            //Assert
            Assert.Equal(3, slicedDf.shape[0]);
            Assert.Equal(2, slicedDf.shape[1]);

            Assert.Equal(2, slicedDf["col_1"].GetValue<int>(0));
            Assert.Equal("c", slicedDf["col_2"].GetValue<string>(1));
        }

        [Fact]
        public void TestIndexer()
        {
            // Arrange
            var df = pd.DataFrame.from_dict("{'col_1': [3, 2, 1, 0], 'col_2': ['a', 'b', 'c', 'd']}");


            // Act
            var indexedInt = df[0, "col_1"];
            var indexedString = df[1, "col_2"];

            // Assert
            Assert.Equal(df["col_1"].GetValue<int>(0), (int)indexedInt);
            Assert.Equal(df["col_2"].GetValue<string>(1), (string)indexedString);
        }

        [Fact]
        public void TestReturnsCorrectSeries()
        {
            // Arrange
            var df = pd.DataFrame.from_dict("{'col_1': [3, 2, 1, 0]}");

            // Act
            Series col2 = new Series(new int[] { 1, 2, 3, 4 }, new Column { Name = "col_2", DType = typeof(int) });
            df.data.Add(col2);

            // Assert
            Assert.Equal(col2, df["col_2"]);
            Assert.True(col2.array<int>().SequenceEqual(df["col_2"].array<int>()));
        }

        [Fact]
        public void TestMultiColumnIndexer()
        {
            // Arrange
            var df = pd.DataFrame.from_dict("{'col_1': [3, 2, 1, 0], 'col_2': ['a', 'b', 'c', 'd'], 'col_3': [1, 2, 3, 4]}");

            Assert.Equal(3, df.shape[1]);

            // Act
            var indexedDf = df["col_1", "col_2"];

            // Assert


            Assert.Equal(2, indexedDf.shape[1]);
            Assert.Equal(4, indexedDf.shape[0]);

            Assert.Equal(3, indexedDf["col_1"].GetValue<int>(0));
            Assert.Equal("b", indexedDf["col_2"].GetValue<string>(1));
        }

    }
}
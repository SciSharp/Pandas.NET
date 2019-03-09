using NumSharp.Core;
using Pandas.Impl;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Pandas.Test
{
    public class DataFrameTest
    {
        [Fact]
        public void Create_WithNDArray_Test()
        {
            NDArray array = np.arange(10);
            array.reshape(5, 2);
            var pd = new Pandas();
            var df1 = new DataFrame<string>(array, null, null, typeof(object));
            var one = df1[0];
            Assert.Equal(0, (one as SeriesBase).Name);
            Assert.Equal(4, (one as SeriesBase)[2]);
            var oneAndTwo = df1[0,1];
            var s = oneAndTwo.iloc[4];
            Assert.Equal(8, s[0]);
            Assert.Equal(9, s[1]);
        }

        [Fact]
        public void Create_WithDict_Test()
        {
            var dict = new Dictionary<string, NDArray>
            {
                { "one", np.arange(10000) },
                { "two", np.arange(10001, 20001) }
            };
            var pd = new Pandas();
            var df1 = new DataFrame<string>(dict);
            var one = df1["one"];
            Assert.Equal("one", (one as SeriesBase).Name);
            Assert.Equal(2, (one as SeriesBase)[2]);
            var oneAndTwo = df1["one", "two"];

            var s = oneAndTwo.iloc[9999];
            Assert.Equal(9999, s[0]);
            Assert.Equal(9999, s["one"]);
            Assert.Equal(20000, s[1]);
            Assert.Equal(20000, s["two"]);
        }

        [Fact]
        public void SetColumn_Test()
        {
            var dict = new Dictionary<string, NDArray>
            {
                { "one", np.arange(1000) },
                { "two", np.arange(1001, 2001) }
            };
            var pd = new Pandas();
            var df1 = pd.DataFrame<string>(dict);
            df1["three"] = new Series(np.arange(2001, 3001));
            Assert.Equal(3000, df1.Size);
            df1.SingleColumn("four", 1);
            Assert.Equal(1, df1["four"][500]);
            Assert.Equal(4000, df1.Size);

            df1.SingleColumn(1, 1);
            Assert.Equal(1, df1["two"][500]);
            df1.Column("five", np.arange(3001, 4001));
            Assert.Equal(3001, df1["five"][0]);

            df1.SingleColumn(4, 1);
            Assert.Equal(1, df1["five"][0]);
        }

        [Fact]
        public void Read_iloc_Test()
        {
            var dict = new Dictionary<string, NDArray>
            {
                { "one", np.arange(1000) },
                { "two", np.arange(1001, 2001) }
            };
            var pd = new Pandas();
            IDataFrame df1 = new DataFrame<string>(dict);
            Assert.Equal(2, df1.iloc[2].Name);
            Assert.Equal(2, df1.iloc[2]["one"]);
        }
        
        [Fact]
        public void Slice_Row_Test()
        {
            var dict = new Dictionary<string, NDArray>
            {
                { "one", np.arange(100) },
                { "two", np.arange(101, 201) }
            };
            var pd = new Pandas();
            var df1 = new DataFrame<string>(dict);
            var dfSl1 = df1[(Slice)"0:12:5"];
            var one = dfSl1["one"];
            Assert.Equal("one", (one as SeriesBase).Name);
            Assert.Equal(10, (one as SeriesBase)[2]);
            var dfSl2 = df1[(Slice)"0:10"];
            var two = dfSl2["two"];
            Assert.Equal("two", (two as SeriesBase).Name);
            Assert.Equal(110, (two as SeriesBase)[9]);
            var dfSl3 = df1[(Slice)"6:10:3"];
            var two2 = dfSl3["two"];
            Assert.Equal("two", (two2 as SeriesBase).Name);
            Assert.Equal(110, (two2 as SeriesBase)[1]);
            var dfSl4 = df1[(Slice)":"];
            var one2 = dfSl4["one"];
            Assert.Equal("one", (one2 as SeriesBase).Name);
            Assert.Equal(0, (one2 as SeriesBase)[0]);
        }
    }
}

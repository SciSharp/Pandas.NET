using PandasNet;
using System;
using Xunit;

namespace PandasNet.Test
{
    public class SeriesTest
    {
        private Pandas pd;
        public SeriesTest()
        {
            pd = new Pandas();
        }

        [Fact]
        public void CreateSeries_WithArray_Test()
        {
            var series = pd.Series(new int[] { 1, 2, 3 });
            Assert.Equal(3, series.Size);
            Assert.Equal(3, series[2]);
        }

        [Fact]
        public void CreateSeries_WithObject_Test()
        {
            var series = pd.Series(new { a = 1, b = "2" });
            Assert.Equal(2, series.Size);
            Assert.Equal(1, series["a"]);
            Assert.Equal("2", series["b"]);
        }

        [Fact]
        public void Addition_Test()
        {
            var series1 = pd.Series(new { a = 1, b = "2" });
            var series2 = pd.Series(new { a = 2, b = 2 });
            var nd = series1 + series2;
            Assert.Equal(3m, (decimal)nd[0]);
            Assert.Equal("22", nd[1]);
        }

        [Fact]
        public void Subtraction()
        {
            var series1 = pd.Series(new { a = 1, b = "2" });
            var series2 = pd.Series(new { a = 2, b = 2 });
            var nd = series1 - series2;
            Assert.Equal(-1m, (decimal)nd[0]);
            Assert.Null(nd[1]);
        }

        [Fact]
        public void Multiplication_Test()
        {
            var series1 = pd.Series(new { a = 1, b = "2" });
            var series2 = pd.Series(new { a = 2, b = 2 });
            var nd = series1 * series2;
            Assert.Equal(2m, (decimal)nd[0]);
            Assert.Equal(4m, (decimal)nd[1]);
        }

        [Fact]
        public void Division_Test()
        {
            var series1 = pd.Series(new { a = 1, b = "2", c = 1 });
            var series2 = pd.Series(new { a = 2, b = 2, c = 0 });
            var nd = series1 / series2;
            Assert.Equal(0.5m, (decimal)nd[0]);
            Assert.Equal(1m, (decimal)nd[1]);
            Assert.Null(nd[2]);
        }
    }
}

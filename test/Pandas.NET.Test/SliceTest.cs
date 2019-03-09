using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using PandasNet.Impl;

namespace PandasNet.Test
{
    public class SliceTest
    {
        [Fact]
        public void Create_implicit()
        {
            Slice slice = "1:2:3";
            Assert.Equal(1, slice.Start);
            Assert.Equal(2, slice.End);
            Assert.Equal(3, slice.Step);

            slice = ":2:3";
            Assert.Equal(0, slice.Start);
            Assert.Equal(2, slice.End);
            Assert.Equal(3, slice.Step);

            slice = "::3";
            Assert.Equal(0, slice.Start);
            Assert.Equal(int.MaxValue, slice.End);
            Assert.Equal(3, slice.Step);

            slice = ":3";
            Assert.Equal(0, slice.Start);
            Assert.Equal(3, slice.End);
            Assert.Equal(1, slice.Step);

            slice = "1::";
            Assert.Equal(1, slice.Start);
            Assert.Equal(int.MaxValue, slice.End);
            Assert.Equal(1, slice.Step);

            slice = "1:";
            Assert.Equal(1, slice.Start);
            Assert.Equal(int.MaxValue, slice.End);
            Assert.Equal(1, slice.Step);

            slice = "1:2";
            Assert.Equal(1, slice.Start);
            Assert.Equal(2, slice.End);

            slice = ":";
            Assert.Equal(0, slice.Start);
            Assert.Equal(int.MaxValue, slice.End);
        }
    }
}

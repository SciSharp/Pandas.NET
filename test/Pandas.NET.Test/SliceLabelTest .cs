using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using PandasNet.Impl;

namespace PandasNet.Test
{
    public class SliceLabelTest
    {
        [Fact]
        public void Create_implicit()
        {
            SliceLabel sliceLabel = "row1:row2:3";
            Assert.Equal("row1", sliceLabel.StartLabel);
            Assert.Equal("row2", sliceLabel.EndLabel);
            Assert.Equal(3, sliceLabel.Step);
        }
    }
}

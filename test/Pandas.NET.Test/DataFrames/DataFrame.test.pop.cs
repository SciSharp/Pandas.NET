using System;
using System.Collections.Generic;
using System.Linq;
using PandasNet;
using Tensorflow;

namespace Pandas.Test;

public class DataFramePopTest
{
    [Fact]
    public void TestPopMethod()
    {
        // Arrange
        var dataFrameData = new List<Series>();
        dataFrameData.Add(new Series(new float[] { 1, 2, 3, 4, 5 }, new Column("column1", typeof(float))));
        dataFrameData.Add(new Series(new float[] { 6, 7, 8, 9, 10 }, new Column("column2", typeof(float))));
        var dataFrame = new DataFrame(dataFrameData);

        // Act
        var poppedSeries = dataFrame.pop("column1");

        // Assert
        Assert.True(poppedSeries.array<float>().SequenceEqual<float>([1, 2, 3, 4, 5]));
        Assert.False(dataFrame.columns.Where(c => c.Name == "column1").Any());
        Assert.True(dataFrame.columns.Where(c => c.Name == "column2").Any());
    }
}
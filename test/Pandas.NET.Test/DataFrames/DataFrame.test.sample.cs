using PandasNet;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pandas.Test;
public class DataFrameSampleTests
{
    [Fact]
    public void TestSampleMethod()
    {
        // Arrange
        List<Series> data = new List<Series>
        {
            new Series(new double[] { 1, 2, 3, 4, 5 }, new Column { Name = "column1", DType = typeof(double) }),
            new Series(new double[] {6, 7, 8, 9, 10 }, new Column { Name = "column2", DType = typeof(double) })
        };
        var dataFrame = new DataFrame(data);

        // Act
        var sampledDataFrame = dataFrame.sample(n: 3, random_state: 1);

        // Assert
        Assert.Equal(3, sampledDataFrame.index.size);
        Assert.True(sampledDataFrame.columns.Where(x => x.Name == "column1").Any());
        Assert.True(sampledDataFrame.columns.Where(x => x.Name == "column2").Any());
    }

    [Fact]
    public void TestSampleMethodWithFrac()
    {
        // Arrange
        List<Series> data = new List<Series>
        {
            new Series(new double[] { 1, 2, 3, 4, 5 }, new Column { Name = "column1", DType = typeof(double) }),
            new Series(new double[] {6, 7, 8, 9, 10 }, new Column { Name = "column2", DType = typeof(double) })
        };
        var dataFrame = new DataFrame(data);

        // Act
        var sampledDataFrame = dataFrame.sample(frac: 0.4f, random_state: 1);
        var sampledDataFrame2 = dataFrame.sample(frac: 0.4f, random_state: 2);

        // Assert
        Assert.Equal(2, sampledDataFrame.index.size); // 40% of 5 is 2
        Assert.True(sampledDataFrame.columns.Where(x => x.Name == "column1").Any());
        Assert.True(sampledDataFrame.columns.Where(x => x.Name == "column2").Any());
        Assert.Equal(2.0D, sampledDataFrame["column1"].GetValue(0));
        Assert.Equal(7.0D, sampledDataFrame["column2"].GetValue(0));
        
        // assert for other state
        Assert.Equal(2, sampledDataFrame2.index.size); // 40% of 5 is 2
        Assert.Equal(5.0D, sampledDataFrame2["column1"].GetValue(0));
        Assert.Equal(10.0D, sampledDataFrame2["column2"].GetValue(0));
    }

    [Fact]
    public void TestSampleMethodThrowsException()
    {
        // Arrange
        List<Series> data = new List<Series>
        {
            new Series(new double[] { 1, 2, 3, 4, 5 }, new Column { Name = "column1", DType = typeof(double) }),
            new Series(new double[] {6, 7, 8, 9, 10 }, new Column { Name = "column2", DType = typeof(double) })
        };
        var dataFrame = new DataFrame(data);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => dataFrame.sample());
        Assert.Throws<ArgumentException>(() => dataFrame.sample(n: 3, frac: 0.4f));
        Assert.Throws<ArgumentException>(() => dataFrame.sample(n: 6));
    }
}
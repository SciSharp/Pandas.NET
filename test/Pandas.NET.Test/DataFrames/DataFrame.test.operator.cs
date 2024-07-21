using System;
using System.Collections.Generic;
using HDF5CSharp;
using PandasNet;


namespace Pandas.Test;

public class DataFrameOperatorTests
{
    [Fact]
    public void TestSubtractionOperator()
    {
        // Arrange
        List<Series> data = new List<Series>
        {
            new Series(new double[] { 1, 2, 3, 4, 5 }, new Column { Name = "column1", DType = typeof(double) }),
            new Series(new double[] { 2, 4, 6, 8, 10 }, new Column { Name = "column2", DType = typeof(double) })
        };
        var df = new DataFrame(data);

        var series = new Series(new double[] { 0.5, 1.5 }, new Series(new string[]{"column1","column2"}), new Column("sub", typeof(double)));

        // Act
        var result = df - series;

        // Assert
        Assert.Equal(new double[] { 0.5, 1.5, 2.5, 3.5, 4.5 }, result["column1"].data as double[]);
        Assert.Equal(new double[] { 0.5, 2.5, 4.5, 6.5, 8.5}, result["column2"].data as double[]);
    }

    [Fact]
    public void TestDivisionOperator()
    {
        List<Series> data = new List<Series>
        {
            new Series(new double[] { 5, 10, 15, 20, 25 }, new Column { Name = "column1", DType = typeof(double) }),
            new Series(new double[] { 2, 4, 6, 8, 10 }, new Column { Name = "column2", DType = typeof(double) })
        };
        var df = new DataFrame(data);

        var series = new Series(new double[] { 5, 2 });

        // Act
        var result = df / series;

        // Assert
        Assert.Equal(new double[] { 1, 2, 3, 4, 5 }, result["column1"].data as double[]);
        Assert.Equal(new double[] { 1, 2, 3, 4, 5 }, result["column2"].data as double[]);
    }

    [Fact]
    public void TestMultiplicationOperator()
    {
        List<Series> data = new List<Series>
        {
            new Series(new double[] { 5, 10, 15, 20, 25 }, new Column { Name = "column1", DType = typeof(double) }),
            new Series(new double[] { 2, 4, 6, 8, 10 }, new Column { Name = "column2", DType = typeof(double) })
        };
        var df = new DataFrame(data);

        var series = new Series(new double[] { 1, 10 });

        // Act
        var result = df * series;

        // Assert
        Assert.Equal(new double[] { 5, 10, 15, 20, 25 }, result["column1"].data as double[]);
        Assert.Equal(new double[] { 20, 40, 60, 80, 100 }, result["column2"].data as double[]);
    }

    [Fact]
    public void TestEqualityOperator()
    {
        // Arrange
        List<Series> data = new List<Series>
        {
            new Series(new float[] { 1, 2, 3, 4, 5 }, new Column { Name = "column1", DType = typeof(float) }),
            new Series(new float[] { 6, 7, 8, 9, 10 }, new Column { Name = "column2", DType = typeof(float) })
        };
        var df = new DataFrame(data);
        var df2 = new DataFrame(data);

        // Arrange: DataFrame with different column name
        List<Series> badColumnData = new List<Series>
        {
            new Series(new float[] { 1, 2, 3, 4, 5 }, new Column { Name = "Column1", DType = typeof(float) }),
            new Series(new float[] { 6, 7, 8, 9, 10 }, new Column { Name = "column2", DType = typeof(float) })
        };
        var badColumnDf = new DataFrame(badColumnData);
        // Arrange: DataFrame with different data
        List<Series> badData = new List<Series>
        {
            new Series(new float[] { 1, 2, 3, 4, 5 }, new Column { Name = "column1", DType = typeof(float) }),
            new Series(new float[] { 6, 7, 8, 9, 11 }, new Column { Name = "column2", DType = typeof(float) })
        };

        // Act
        bool areEqual = df == df2;
        bool badColumnInequal = df == badColumnDf;
        bool badDataInequal = df == new DataFrame(badData);

        // Assert
        Assert.True(areEqual);
        Assert.False(badColumnInequal);
        Assert.False(badDataInequal);
    }

    [Fact]
    public void TestInEqualityOperator()
    {
        // Arrange
        List<Series> data = new List<Series>
        {
            new Series(new float[] { 1, 2, 3, 4, 5 }, new Column { Name = "column1", DType = typeof(float) }),
            new Series(new float[] { 6, 7, 8, 9, 10 }, new Column { Name = "column2", DType = typeof(float) })
        };
        var df = new DataFrame(data);
        var df2 = new DataFrame(data);

        // Arrange: DataFrame with different column name
        List<Series> badColumnData = new List<Series>
        {
            new Series(new float[] { 1, 2, 3, 4, 5 }, new Column { Name = "Column1", DType = typeof(float) }),
            new Series(new float[] { 6, 7, 8, 9, 10 }, new Column { Name = "column2", DType = typeof(float) })
        };
        var badColumnDf = new DataFrame(badColumnData);
        // Arrange: DataFrame with different data
        List<Series> badData = new List<Series>
        {
            new Series(new float[] { 1, 2, 3, 4, 5 }, new Column { Name = "column1", DType = typeof(float) }),
            new Series(new float[] { 6, 7, 8, 9, 11 }, new Column { Name = "column2", DType = typeof(float) })
        };

        // Act
        bool areEqual = df != df2;
        bool badColumnInequal = df != badColumnDf;
        bool badDataInequal = df != new DataFrame(badData);

        // Assert
        Assert.False(areEqual);
        Assert.True(badColumnInequal);
        Assert.True(badDataInequal);
    }
}

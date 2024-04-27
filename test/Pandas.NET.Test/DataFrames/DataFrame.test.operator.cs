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
            new Series(new int[] { 1, 2, 3, 4, 5 }, new Column { Name = "column1", DType = typeof(int) }),
            new Series(new int[] { 2, 4, 6, 8, 10 }, new Column { Name = "column2", DType = typeof(int) })
        };
        var df = new DataFrame(data);

        var series = new Series(new int[] { 1, 2 }, new Series(new string[]{"column1","column2"}), new Column("sub", typeof(int)));

        // Act
        var result = df - series;

        // Assert
        Assert.Equal(new int[] { 0, 1, 2, 3, 4 }, result["column1"].data as int[]);
        Assert.Equal(new int[] { 0, 2, 4, 6, 8}, result["column2"].data as int[]);
    }


    [Fact]
    public void TestSubtractionOperator_Float()
    {
        // Arrange
        List<Series> data = new List<Series>
        {
            new Series(new float[] { 1, 2, 3, 4, 5 }, new Column { Name = "column1", DType = typeof(float) }),
            new Series(new float[] { 2, 4, 6, 8, 10 }, new Column { Name = "column2", DType = typeof(float) })
        };
        var df = new DataFrame(data);

        var series = new Series(new float[] { 0.5F, 1.5F }, new Series(new string[]{"column1","column2"}), new Column("sub", typeof(float)));

        // Act
        var result = df - series;

        // Assert
        Assert.Equal(new float[] { 0.5F, 1.5F, 2.5F, 3.5F, 4.5F }, result["column1"].data as float[]);
        Assert.Equal(new float[] { 0.5F, 2.5F, 4.5F, 6.5F, 8.5F}, result["column2"].data as float[]);
    }


    [Fact]
    public void TestSubtractionOperator_Double()
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
}

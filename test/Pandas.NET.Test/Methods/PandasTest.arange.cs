using System;
using static PandasNet.PandasApi;

namespace Pandas.Test;

public partial class PandasTest
{

    /// <summary>
    /// Test arange method for int
    /// </summary>
    [Fact]
    public void TestArangeInt()
    {
        //create an array of 10 elements from 0 to 9
        var arange = pd.arange<int>(0, 10, 1);
        Assert.Equal(10, arange.Length);
        for (int i = 0; i < arange.Length; i++)
        {
            Assert.Equal(i, arange[i]);
        }
    }

    /// <summary>
    /// Test arange method for float
    /// </summary>
    [Fact]
    public void TestArangeFloat()
    {
        // Only int is supported
        Assert.Throws<NotImplementedException>(() => pd.arange<float>(0, 10, 1));
    }
}
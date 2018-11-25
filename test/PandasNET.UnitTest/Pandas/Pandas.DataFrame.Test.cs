using Microsoft.VisualStudio.TestTools.UnitTesting;
using NumSharp;
using NumSharp.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PandasNET.UnitTest.Extensions
{
    [TestClass]
    public class PandasDataFrameTest
    {
        [TestMethod]
        public void DataFrame()
        {
            var np = new NumPy();
            var pd = new Pandas();
            var array = np.random.randint(low: 0, high: 10, size: new Shape(5, 5));
            // var df = pd.DataFrame<int, >((NDArray<object>)array, columns: new string[] { "a", "b", "c", "d", "e" });

            /*var column1 = df["a"];

            for (int idx = 0; idx < 5; idx++)
                Assert.IsTrue(column1[idx] == array[idx,0]);

            var column2 = df["b"];

            for (int idx = 0; idx < 5; idx++)
                Assert.IsTrue(column2[idx] == array[idx,1]);*/
        }
    }
}

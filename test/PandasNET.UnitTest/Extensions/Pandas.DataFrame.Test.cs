using Microsoft.VisualStudio.TestTools.UnitTesting;
using NumSharp;
using NumSharp.Extensions;
using PandasNET.Extensions;
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
            var np = new NumPy<int>();
            var pd = new Pandas();
            var array = np.random.randint(low: 0, high: 10, size: new Shape(5, 5));
            var df = pd.DataFrame(array, columns: new string[] { "a", "b", "c", "d", "e" });
        }
    }
}

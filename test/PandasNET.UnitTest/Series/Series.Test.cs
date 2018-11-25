using Microsoft.VisualStudio.TestTools.UnitTesting;
using PandasNET.UnitTest;
using System;
using System.Collections.Generic;
using System.Text;

namespace PandasNET.SeriesUnitTest
{
    [TestClass]
    public class SeriesTest : TestBase
    {
        [TestMethod]
        public void ConstructByNDArray()
        {
            var nd = np.random.randn(10);
            var s = pd.Series(nd);
        }

        [TestMethod]
        public void ConstructByNDArrayWindIndex()
        {
            var nd = np.random.randn(5);
            var s = pd.Series(nd, new Index("a", "b", "c", "d", "e"));
        }
    }
}

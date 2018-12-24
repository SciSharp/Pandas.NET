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
            //var nd = np.random.randn(10);
            //var s = pd.Series(nd);
        }

        [TestMethod]
        public void ConstructByNDArrayWithIndex()
        {
            //var nd = np.random.randn(5);
            //var se = pd.Series(nd, new Index("a", "b", "c", "d", "e"));

            //Assert.AreEqual(se[4], se["e"]);
        }

        [TestMethod]
        public void ConstructByAnonymousObject()
        {
            var se = pd.Series(new { b = 1, a = 0, c = 2 });

            Assert.AreEqual(se[2], se["c"]);
        }
    }
}

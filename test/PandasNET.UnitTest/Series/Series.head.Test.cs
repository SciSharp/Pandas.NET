using Microsoft.VisualStudio.TestTools.UnitTesting;
using NumSharp.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PandasNET.UnitTest
{
    [TestClass]
    public class Series : TestBase
    {
        [TestMethod]
        public void head()
        {
            var nd = np.random.randn(10);
            var se = pd.Series(nd);
            var hd = se.head();

            Assert.IsTrue(hd.size == 5);
            Assert.IsTrue(se[4].Equals(hd[4]));
        }
    }
}

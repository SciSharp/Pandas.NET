using Microsoft.VisualStudio.TestTools.UnitTesting;
using PandasNET.UnitTest;
using System;
using System.Collections.Generic;
using System.Text;

namespace PandasNET.IndexUnitTest
{
    [TestClass]
    public class IndexTest : TestBase
    {
        [TestMethod]
        public void ConstructByIntArray()
        {
            var index = pd.Index(1, 2, 3);
        }
    }
}

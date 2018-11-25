using Microsoft.VisualStudio.TestTools.UnitTesting;
using NumSharp.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PandasNET.UnitTest
{
    [TestClass]
    public class Series
    {
        NumPy np = new NumPy();

        [TestMethod]
        public void head()
        {
            var pd = new Pandas();
            var nd = np.random.randn(10);
            var s = pd.Series(nd);
        }
    }
}

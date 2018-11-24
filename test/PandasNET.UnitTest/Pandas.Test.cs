using Microsoft.VisualStudio.TestTools.UnitTesting;
using NumSharp;
using NumSharp.Core;
using PandasNET.Extensions;

namespace PandasNET.UnitTest
{
    [TestClass]
    public class PandasTest
    {
        [TestMethod]
        public void TestPandas()
        {
            var np = new NumPy();
            var pd = new Pandas();
            var nd = np.random.randn(10);
            var s = pd.Series(nd);
        }
    }
}

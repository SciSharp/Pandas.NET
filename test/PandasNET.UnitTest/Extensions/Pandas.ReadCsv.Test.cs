using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using PandasNET.Extensions;

namespace PandasNET.UnitTest.Extensions
{
    [TestClass]
    public class PandasReadCsvTest
    {
        [TestMethod]
        public void read_csv()
        {
            var pd = new Pandas();
            var a = pd.read_csv("./data/train.csv");
        }
    }
}

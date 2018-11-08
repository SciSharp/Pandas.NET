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

            var trainData = System.IO.Path.GetFullPath("../../../../../data/train.csv");

            var a = pd.read_csv(trainData);

            /*var column1 = a["Lag1"];
            var column2 = a["Lag2"];

            Assert.IsTrue(column1.Size == 998);
            Assert.IsTrue(column2.Size == 998);*/
        }
    }
}

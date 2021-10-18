using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PandasNet.PandasApi;

namespace Pandas.Test
{
    [TestClass]
    public class IndexSliceTest
    {
        [TestMethod]
        public void Test()
        {
            var df = pd.DataFrame.from_dict("{'col_1': [3, 2, 1, 0], 'col_2': ['a', 'b', 'c', 'd']}");
            var df1 = df[new[] { "col_1" }];
        }
    }
}

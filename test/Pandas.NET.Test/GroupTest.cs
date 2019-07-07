using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using PandasNet.Impl;
using NumSharp;

namespace PandasNet.Test
{
    public class GroupTest
    {
        [Fact]
        public void Create_Group_Test()
        {
            var dict = new Dictionary<string, NDArray>
            {
               { "col1",np.arange(5) },
               { "col2",np.arange(5) },
               { "col3",np.arange(5) },
               { "col4",np.arange(5) },
               { "col5",np.arange(5) },
            };

            var pd = new Pandas();
            var df1 = pd.DataFrame<string>(dict, new string[] {
                "row1","row2","row3","row4","row5"
            });
            var gro = df1.groupby("col2");
            var group = gro.Groups;
            var a = gro.Groups[1];
            Assert.Equal("row2", a.Values[0]);
        }
    }
}

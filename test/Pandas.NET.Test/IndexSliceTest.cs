using static PandasNet.PandasApi;

namespace Pandas.Test
{
    public class IndexSliceTest
    {
        [Fact]
        public void Test()
        {
            var df = pd.DataFrame.from_dict("{'col_1': [3, 2, 1, 0], 'col_2': ['a', 'b', 'c', 'd']}");
            var df1 = df[new[] { "col_1" }];
        }
    }
}

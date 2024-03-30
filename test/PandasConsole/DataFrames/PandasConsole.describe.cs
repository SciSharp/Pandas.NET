using System.Collections.Generic;
using System.Text.Json;
using PandasNet;
using static PandasNet.PandasApi;

namespace PandasConsole.Methods
{
    public class PandasConsoleDescribe
    {
        public DataFrame GetSampleDataFrame()
        {
            var df = pd.DataFrame.from_dict(JsonSerializer.Serialize(new Dictionary<string, int[]>
            {
                { "col_1",new int[] { 1, 2, 3, 4 }},
                { "col_2", new int[] { 4, 5, 6, 7 }},
                { "col_3 ", new int[] { 7, 8, 9, 10 }}
            }));
            return df;
        }

        public DataFrame DescribeDataFrame()
        {
            var df = GetSampleDataFrame();
            return df.describe();
        }
    }
}

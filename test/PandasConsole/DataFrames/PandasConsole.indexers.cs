using System.Collections.Generic;
using System.Text.Json;
using PandasNet;
using static PandasNet.PandasApi;

namespace PandasConsole.Methods
{
    public class PandasConsoleIndexers
    {
        public DataFrame GetSampleDataFrame()
        {
            var df = pd.DataFrame.from_dict(JsonSerializer.Serialize(new Dictionary<string, int[]>
            {
                { "col_1",new int[] { 1, 2, 3, 4, 5 }},
                { "col_2", new int[] { 4, 5, 6, 7, 8 }},
                { "col_3 ", new int[] { 7, 8, 9, 10, 11 }}
            }));
            return df;
        }
        
        // index on multiple columns
        public (DataFrame, DataFrame) MultiColumnIndexer()
        {
            var df = GetSampleDataFrame();
            return (df, df["col_1","col_2"]);
        }
    }
}

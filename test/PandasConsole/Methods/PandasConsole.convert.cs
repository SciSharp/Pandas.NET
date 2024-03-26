using PandasNet;
using static PandasNet.PandasApi;

namespace PandasConsole.Methods
{
    public class PandasConsoleConvert
    {
        public DataFrame GetSampleDataFrame()
        {
            return pd.DataFrame.from_dict("{'col_1': [3, 2, 1, 0], 'col_2': ['3', '2', '1', '0']}");
        }
    }
}

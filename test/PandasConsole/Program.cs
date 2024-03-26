using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CommandLine;
using PandasConsole.Methods;
using Tensorflow;
using static PandasNet.PandasApi;

namespace PandasConsole
{
    class Program
    {
        public class Options
        {
            [Option("info", Required = false, HelpText = "Print the info a DataFrame")]
            public bool ConvertSample { get; set; }
        }


        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(o =>
                {
                    if (o.ConvertSample)
                    {
                        CommandRunners.RunInfoSample();
                    }
                })
                .WithNotParsed(CommandRunners.HandleParseError);

            var df = pd.DataFrame.from_dict("{'col_1': [3, 2, 1, 0], 'col_2': ['a', 'b', 'c', 'd']}");
            var df1 = df[new[] { "col_1" }];
        }

        public static class CommandRunners
        {
            public static void HandleParseError(IEnumerable<Error> errs)
            {
                Console.WriteLine("An Error Occurred");
            }
            public static void RunInfoSample()
            {
                var converter = new PandasConsoleConvert();
                var df = converter.GetSampleDataFrame();
                Utils.PrintDataFrameInfo(df);
            }
        }
    }
}

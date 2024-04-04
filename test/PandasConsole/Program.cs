using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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

            [Option("describe", Required = false, HelpText = "Print the describe a DataFrame")]
            public bool DescribeSample { get; set; }
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
                    if (o.DescribeSample)
                    {
                        CommandRunners.RunDescribeSample();
                    }
                })
                .WithNotParsed(CommandRunners.HandleParseError);
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

            public static void RunDescribeSample()
            {
                var describe = new PandasConsoleDescribe();
                var df = describe.DescribeDataFrame();

                Console.Write(Utils.RenderDataTable(Utils.DataFrameToTable(df)));
            }
        }
    }
}

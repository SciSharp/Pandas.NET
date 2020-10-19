using System;
using System.Collections.Generic;
using System.Text;
using PandasNet.Impl;

namespace PandasNet
{
    public static class PandasMethods
    {
        /// <summary>
        /// Read a comma-separated values (csv) file into DataFrame.
        /// </summary>
        /// <param name="pd"></param>
        /// <param name="filepath_or_buffer"></param>
        /// <param name="sep"></param>
        /// <param name="names"></param>
        /// <returns></returns>
        public static IDataFrame read_csv(this Pandas pd, string filepath, string sep = ",", string[] names = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Write object to a comma-separated values (csv) file.
        /// </summary>
        /// <param name="filepath">File path.</param>
        /// <param name="sep">Field delimiter for the output file.</param>
        /// <param name="na_rep">Missing data representation.</param>
        /// <param name="float_format">Format string for floating point numbers.</param>
        /// <param name="columns">Columns to write.</param>
        /// <param name="header">Write out the column names.</param>
        /// <param name="quoting">
        /// Defaults to QUOTE_MINIMAL. If you have set a float_format then
        /// floats are converted to strings and thus QUOTE_NONNUMERIC will
        /// treat them as non-numeric.
        /// </param>
        /// <param name="quotechar">Character used to quote fields.</param>
        /// <param name="line_terminator">
        /// The newline character or character sequence to use in the output
        /// file. Defaults to os.linesep, which depends on the OS in which this
        /// method is called (‘n’ for linux, ‘rn’ for Windows, i.e.).
        /// </param>
        public static void to_csv(this IDataFrame df, string filepath, char sep = ',',
            string na_rep = "", string float_format = null, IEnumerable<string> columns = null,
            bool header = true, int quoting = (int) CsvQuoteStyle.QUOTE_MINIMAL,
            char quotechar = '"', string line_terminator = null)
        {
            new CsvWriter(sep, na_rep, float_format, header,
                (CsvQuoteStyle) quoting, quotechar, string.IsNullOrEmpty(
                    line_terminator) ? Environment.NewLine : line_terminator,
                new UTF8Encoding(false)).Write(filepath, df, columns);
        }
    }
}

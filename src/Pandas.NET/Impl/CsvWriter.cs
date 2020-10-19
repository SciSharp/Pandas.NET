using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NumSharp;

namespace PandasNet.Impl
{
    internal class CsvWriter
    {
        private readonly byte delimiter;
        private readonly byte[] noValue;
        private readonly string floatFormat;
        private readonly bool header;
        private readonly CsvQuoteStyle quotingStyle;
        private readonly byte quotebyte;
        private readonly char[] specialChars;
        private readonly Encoding encoding;
        private readonly byte[] lfBytes;

        internal CsvWriter(char sep, string na_rep, string floatFormat,
            bool header, CsvQuoteStyle quotingStyle, char quotechar,
            string lineTerminator, Encoding encoding)
        {
            delimiter = (byte) sep;
            noValue = encoding.GetBytes(na_rep);
            this.floatFormat = floatFormat;
            this.header = header;
            this.quotingStyle = quotingStyle;
            quotebyte = (byte) quotechar;
            specialChars = lineTerminator.Length > 1 ?
                new char[] { sep, quotechar, lineTerminator[0], lineTerminator[1] }
                : new char[] { sep, quotechar, lineTerminator[0] };
            this.encoding = encoding;
            lfBytes = encoding.GetBytes(lineTerminator);
        }

        internal void Write(string filepath, IDataFrame df,
            IEnumerable<string> columns)
        {
            var columnLabels = columns == null ?
                df.Columns.Values.Data<string>() : columns.ToArray();
            var columnCount = columnLabels.Length;
            int rowCount = df.Index.Size;
            var data = df[columnLabels].Values;
            using (var fs = File.Create(filepath))
            {
                if (columnCount == 0) { return; }
                else if (header) { WriteHeader(fs, columnLabels); }
                for (var i = 0; i < rowCount; i++)
                {
                    WriteField(data[i][0], fs);
                    for (var j = 1; j < columnCount; j++)
                    {
                        fs.WriteByte(delimiter);
                        WriteField(data[i][j], fs);
                    }
                    fs.Write(lfBytes, 0, lfBytes.Length);
                }
            }
        }

        private void WriteField(NDArray fieldValue, Stream fs)
        {
            var needsQuoting = NeedsQuoting(fieldValue);
            if (needsQuoting) { fs.WriteByte(quotebyte); }
            var bytes = noValue;
            if (fieldValue.size > 0)
            {
                var fieldValueFormatted = floatFormat != null &&
                    (fieldValue.dtype == np.float32 || fieldValue.dtype == np.float64)
                    ? ((double) fieldValue).ToString(floatFormat)
                    : fieldValue.ToString();
                bytes = encoding.GetBytes(fieldValueFormatted);
            }
            fs.Write(bytes, 0, bytes.Length);
            if (needsQuoting) { fs.WriteByte(quotebyte); }
        }

        private bool NeedsQuoting(object field)
        {
            switch (quotingStyle)
            {
                case CsvQuoteStyle.QUOTE_MINIMAL:
                    return !IsNumber(field) && -1 != field.ToString().IndexOfAny(specialChars);
                case CsvQuoteStyle.QUOTE_ALL:
                    return true;
                case CsvQuoteStyle.QUOTE_NONNUMERIC:
                    return !IsNumber(field);
                case CsvQuoteStyle.QUOTE_NONE:
                    return false;
                default:
                    throw new ArgumentException("Invalid value", nameof(quotingStyle));
            }
        }

        /// <summary>
        /// Writes the columnLabels on one line to the FileStream.
        /// </summary>
        /// <param name="fs">Output stream</param>
        /// <param name="encoding">Byte encoding used</param>
        /// <param name="columnLabels">Column names</param>
        /// <param name="delimiter">Separator for columns</param>
        /// <param name="lfBytes">Line-break bytes.</param>
        private void WriteHeader(Stream fs, string[] columnLabels)
        {
            var bytes = encoding.GetBytes(columnLabels[0]);
            fs.Write(bytes, 0, bytes.Length);
            for (var i = 1; i < columnLabels.Length; i++)
            {
                fs.WriteByte(delimiter);
                bytes = encoding.GetBytes(columnLabels[i]);
                fs.Write(bytes, 0, bytes.Length);
            }
            fs.Write(lfBytes, 0, lfBytes.Length);
        }

        private static bool IsNumber(object value)
        {
            return value is sbyte || value is byte || value is short ||
                value is ushort || value is int || value is uint ||
                value is long || value is ulong || value is float ||
                value is double || value is decimal;
        }
    }

    internal enum CsvQuoteStyle
    {
        /// <summary>
        /// Instructs writer objects to only quote those fields which
        /// contain special characters such as delimiter, quotechar or any
        /// of the characters in lineterminator.
        /// </summary>
        QUOTE_MINIMAL = 0,
        /// <summary>
        /// Instructs writer objects to quote all fields.
        /// </summary>
        QUOTE_ALL = 1,
        /// <summary>
        /// <para>Instructs writer objects to quote all non-numeric
        /// fields.</para>
        /// <para>Instructs the reader to convert all non-quoted fields
        /// to type float.</para>
        /// </summary>
        QUOTE_NONNUMERIC = 2,
        /// <summary>
        /// <para>Instructs writer objects to never quote fields. When the
        /// current delimiter occurs in output data it is preceded by the
        /// current escapechar character. If escapechar is not set, the
        /// writer will raise Error if any characters that require escaping
        /// are encountered.</para>
        /// <para>Instructs reader to perform no special processing of
        /// quote characters.</para>
        /// </summary>
        QUOTE_NONE = 3
    }
}

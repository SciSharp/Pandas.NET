using System;
using System.Text;
using PandasNet;
using System.Linq;
using System.Data;
using System.Collections.Generic;

namespace PandasConsole;
public static class Utils
{
    public static void PrintDataFrameInfo(DataFrame df)
    {
        int indexLength = df.shape[0];
        float minIndex = df.index.min();
        float maxIndex = df.index.max();
        int columnCount = df.columns.Count;

        StringBuilder sb = new();
        sb.AppendLine("**Dataframe Info**");
        sb.AppendLine($"Range Index: {indexLength} entries, {minIndex} to {maxIndex}");
        sb.AppendLine($"Data Columns: (total {columnCount} columns)");
        Console.WriteLine(sb.ToString());


        DataTable table = new DataTable();
        table.Columns.Add("#", typeof(int));
        table.Columns.Add("Column", typeof(string));
        table.Columns.Add("Non-Null Count", typeof(int));
        table.Columns.Add("DType", typeof(string));

        for (int i = 0; i < df.columns.Count; i++)
        {
            table.Rows.Add(i, df.columns[i].Name, df[df.columns[i].Name].count(), df[df.columns[i].Name].dtype ?? typeof(String));
        }
        Console.Write(RenderDataTable(table));
    }

    /// <summary>
    /// Renders a DataTable to a markdown table
    /// </summary>
    /// <remarks>Code from StackOverflow contributor david-liebeherr</remarks>
    /// <param name="table"></param>
    /// <returns></returns>
    public static string RenderDataTable(DataTable table)
    {
        String GetCellValueAsString(DataRow row, DataColumn column)
        {
            var cellValue = row[column];
            var cellValueAsString = cellValue is null or DBNull ? "{{null}}" : cellValue.ToString();

            return cellValueAsString;
        }

        var columnWidths = new Dictionary<DataColumn, Int32>();

        foreach (DataColumn column in table.Columns)
        {
            columnWidths.Add(column, column.ColumnName.Length);
        }

        foreach (DataRow row in table.Rows)
        {
            foreach (DataColumn column in table.Columns)
            {
                columnWidths[column] = Math.Max(columnWidths[column], GetCellValueAsString(row, column).Length);
            }
        }

        var resultBuilder = new StringBuilder();

        resultBuilder.Append("| ");

        foreach (DataColumn column in table.Columns)
        {
            resultBuilder.Append(column.ColumnName.PadRight(columnWidths[column]));
            resultBuilder.Append(" | ");
        }

        resultBuilder.AppendLine();
        resultBuilder.Append("| ");
        foreach (DataColumn column in table.Columns)
        {
            resultBuilder.Append("-".PadRight(columnWidths[column], '-'));
            resultBuilder.Append(" | ");
        }
        resultBuilder.AppendLine();

        foreach (DataRow row in table.Rows)
        {
            resultBuilder.Append("| ");

            foreach (DataColumn column in table.Columns)
            {
                resultBuilder.Append(GetCellValueAsString(row, column).PadRight(columnWidths[column]));
                resultBuilder.Append(" | ");
            }

            resultBuilder.AppendLine();
        }

        return resultBuilder.ToString();
    }
}
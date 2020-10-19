using Xunit;
using NumSharp;
using System.Linq;
using System.IO;

namespace PandasNet.Test
{
	public class DataFrameCsvTest
	{
		public DataFrameCsvTest()
		{}

		[Fact]
		public void WriteCsv_ToFile_Test()
		{
			var filepath = "write_test.csv";
			var array = np.arange(100).reshape(20, 5);
			var columnNames = new string[] { "first", "second", "third",
				"fourth", "fifth" };
			var pd = new Pandas();
			IDataFrame df1 = pd.DataFrame(array, null, columnNames, typeof(object));
			df1.to_csv(filepath);
			using (var fr = File.OpenText(filepath))
			{
				Assert.Equal(string.Join(',', columnNames), fr.ReadLine());
				for (var i = 0; i < array.shape[0]; i++)
				{
					Assert.Equal(string.Join(',', array[i].Data<int>()), fr.ReadLine());
				}
			}
		}

		[Fact]
		public void WriteCsvQuoted_ToFile_Test()
		{
			var filepath = "write_quoted_test.csv";
			var array = np.arange(100).reshape(20, 5);
			var columnNames = new string[] { "first", "second", "third",
				"fourth", "fifth" };
			var pd = new Pandas();
			IDataFrame df1 = pd.DataFrame(array, null, columnNames, typeof(object));
			df1.to_csv(filepath, quoting: 1);
			using (var fr = File.OpenText(filepath))
			{
				Assert.Equal(string.Join(',', columnNames), fr.ReadLine());
				for (var i = 0; i < array.shape[0]; i++)
				{
					Assert.Equal('"' + string.Join("\",\"", array[i].Data<int>()) + '"', fr.ReadLine());
				}
			}
		}

		[Fact]
		public void WriteCsvFormated_ToFile_Test()
		{
			var filepath = "write_quoted_test.csv";
			var array = np.arange(0, 50, 0.5).reshape(20, 5);
			var columnNames = new string[] { "first", "second", "third",
				"fourth", "fifth" };
			var floatFormat = "E03";
			var pd = new Pandas();
			IDataFrame df1 = pd.DataFrame(array, null, columnNames, typeof(object));
			df1.to_csv(filepath, float_format: floatFormat);
			using (var fr = File.OpenText(filepath))
			{
				Assert.Equal(string.Join(',', columnNames), fr.ReadLine());
				for (var i = 0; i < array.shape[0]; i++)
				{
					var formattedData = array[i].Data<double>().Select(
						x => x.ToString(floatFormat));
					Assert.Equal(string.Join(",", formattedData), fr.ReadLine());
				}
			}
		}
	}
}

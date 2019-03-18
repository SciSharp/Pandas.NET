using NumSharp.Core;
using PandasNet.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace PandasNet.Test
{
    public class DataFrameTest
    {
        private IDataFrame _dataFrame = null;

        public DataFrameTest()
        {
            CreateSliceModel();
        }

        private void CreateSliceModel()
        {
            int row = 3;
            int col = 4;
            var nd = np.random.randn(row, col);
            NDArray array = nd;
            //array.reshape(5, 4);
            var pd = new Pandas();
            List<string> indexs = new List<string>();
            List<string> columns = new List<string>();
            CreateRowAndCol(row, col, ref indexs, ref columns);
            IDataFrame df1 = pd.DataFrame(array, indexs, columns, typeof(object));
            _dataFrame = df1;
        }

        [Fact]
        public void Create_WithNDArray_Test()
        {
            NDArray array = np.arange(10);
            array.reshape(5, 2);
            var pd = new Pandas();
            IDataFrame df1 = new DataFrame<string>(array, null, null, typeof(object));
            var one = df1[0];
            Assert.Equal(0, (one as SeriesBase).Name);
            Assert.Equal(4, (one as SeriesBase)[2]);
            var oneAndTwo = df1[0, 1];
            var s = oneAndTwo.iloc[4];
            Assert.Equal(8, s[0]);
            Assert.Equal(9, s[1]);
        }

        [Fact]
        public void Create_WithDict_Test()
        {
            var dict = new Dictionary<string, NDArray>
            {
                { "one", np.arange(10000) },
                { "two", np.arange(10001, 20001) }
            };
            var pd = new Pandas();
            IDataFrame df1 = new DataFrame<string>(dict);
            var one = df1["one"];
            Assert.Equal("one", (one as SeriesBase).Name);
            Assert.Equal(2, (one as SeriesBase)[2]);
            var oneAndTwo = df1["one", "two"];

            var s = oneAndTwo.iloc[9999];
            Assert.Equal(9999, s[0]);
            Assert.Equal(9999, s["one"]);
            Assert.Equal(20000, s[1]);
            Assert.Equal(20000, s["two"]);
        }

        [Fact]
        public void SetColumn_Test()
        {
            var dict = new Dictionary<string, NDArray>
            {
                { "one", np.arange(1000) },
                { "two", np.arange(1001, 2001) }
            };
            var pd = new Pandas();
            var df1 = pd.DataFrame<string>(dict);
            df1["three"] = new Series(np.arange(2001, 3001));
            Assert.Equal(3000, df1.Size);
            df1.SingleColumn("four", 1);
            Assert.Equal(1, df1["four"][500]);
            Assert.Equal(4000, df1.Size);

            df1.SingleColumn(1, 1);
            Assert.Equal(1, df1["two"][500]);
            df1.Column("five", np.arange(3001, 4001));
            Assert.Equal(3001, df1["five"][0]);

            df1.SingleColumn(4, 1);
            Assert.Equal(1, df1["five"][0]);

            Assert.Equal(3000, df1["three"][999]);
            df1["three"] = new Series(np.arange(1000));
            Assert.Equal(999, df1["three"][999]);

        }

        [Fact]
        public void Read_iloc_Test()
        {
            var dict = new Dictionary<string, NDArray>
            {
                { "one", np.arange(1000) },
                { "two", np.arange(1001, 2001) }
            };
            var pd = new Pandas();
            IDataFrame df1 = new DataFrame<string>(dict);
            Assert.Equal(2, df1.iloc[2].Name);
            Assert.Equal(2, df1.iloc[2]["one"]);
            Assert.Equal(3, df1.iloc[3]["one"]);
        }



        [Fact]
        public void Slice_Row_Test()
        {
            var dict = new Dictionary<string, NDArray>
            {
                { "one", np.arange(100) },
                { "two", np.arange(101, 201) }
            };
            var pd = new Pandas();
            IDataFrame df1 = new DataFrame<string>(dict);
            var dfSl1 = df1[(Slice)"0:12:5"];
            var one = dfSl1["one"];
            Assert.Equal("one", (one as SeriesBase).Name);
            Assert.Equal(10, (one as SeriesBase)[2]);
            var dfSl2 = df1[(Slice)"0:10"];
            var two = dfSl2["two"];
            Assert.Equal("two", (two as SeriesBase).Name);
            Assert.Equal(110, (two as SeriesBase)[9]);
            var dfSl3 = df1[(Slice)"6:10:3"];
            var two2 = dfSl3["two"];
            Assert.Equal("two", (two2 as SeriesBase).Name);
            Assert.Equal(110, (two2 as SeriesBase)[1]);
            var dfSl4 = df1[(Slice)":"];
            var one2 = dfSl4["one"];
            Assert.Equal("one", (one2 as SeriesBase).Name);
            Assert.Equal(0, (one2 as SeriesBase)[0]);
        }

        [Fact]
        public void SliceLabel_Row_Test()
        {
            IDataFrame df1 = _dataFrame;
            var sobj2 = df1.loc["row4"];
            var sobj1 = df1.loc["row2"];
            var dfSl1 = df1[(SliceLabel)"row2:row5:2"];
            var obj1 = dfSl1.iloc[0];
            var obj2 = dfSl1.iloc[1];
            Assert.Equal(sobj1[0], obj1[0]);
            Assert.Equal(sobj1[1], obj1[1]);
            Assert.Equal(sobj2[0], obj2[0]);
            Assert.Equal(sobj2[1], obj2[1]);
        }

        [Fact]
        public void Slice_Row_Desc_Test()
        {
            IDataFrame df1 = _dataFrame;
            var sobj1 = df1.iloc[df1.Index.Size-1]; 
            var sobj2 = df1.iloc[df1.Index.Size - 2];
            var dfSl1 = df1[(Slice)"::-1"];
            var obj1 = dfSl1.iloc[0];
            var obj2 = dfSl1.iloc[1];
            Assert.Equal(sobj1[0], obj1[0]);
            Assert.Equal(sobj1[1], obj1[1]);
            Assert.Equal(sobj2[0], obj2[0]);
            Assert.Equal(sobj2[1], obj2[1]);
            var dfSl2 = df1[(Slice)"-3:1:1"];
            sobj1 = df1.iloc[df1.Index.Size-3];
            sobj2 = df1.iloc[df1.Index.Size - 2];
            obj1 = dfSl2.iloc[0];
            obj2 = dfSl2.iloc[1];
            Assert.Equal(sobj1[0], obj1[0]);
            Assert.Equal(sobj1[1], obj1[1]);
            Assert.Equal(sobj2[0], obj2[0]);
            Assert.Equal(sobj2[1], obj2[1]);
            var dfSl3 = df1[(Slice)"-3:1:-1"];
            sobj1 = df1.iloc[0];
            sobj2 = df1.iloc[df1.Index.Size - 1];
            obj1 = dfSl3.iloc[0];
            obj2 = dfSl3.iloc[1];
            Assert.Equal(sobj1[0], obj1[0]);
            Assert.Equal(sobj1[1], obj1[1]);
            Assert.Equal(sobj2[0], obj2[0]);
            Assert.Equal(sobj2[1], obj2[1]);
        }

        private void CreateRowAndCol(int row, int col, ref List<string> indexs, ref List<string> columns)
        {
            for(int r=0;r<row;r++)
            {
                indexs.Add($"row{r+1}");
            }
            for (int c = 0; c < col; c++)
            {
                columns.Add($"col{c+1}");
            }

        }

        [Fact]
        public void SliceLabel_Row_Desc_Test()
        {
            IDataFrame df1 = _dataFrame;
            var sobj1 = df1.loc["row2"];
            var sobj2 = df1.loc["row4"];
            var dfSl1 = df1[(SliceLabel)"row2:row5:2"];
            var obj1 = dfSl1.iloc[0];
            var obj2 = dfSl1.iloc[1];
            Assert.Equal(sobj1[0], obj1[0]);
            Assert.Equal(sobj1[1], obj1[1]);
            Assert.Equal(sobj2[0], obj2[0]);
            Assert.Equal(sobj2[1], obj2[1]);
            var dfSl2 = df1[(SliceLabel)"row2:row5:-2"];
            sobj1 = df1.loc["row4"];
            sobj2 = df1.loc["row2"];
            obj1 = dfSl2.iloc[0];
            obj2 = dfSl2.iloc[1];
            Assert.Equal(sobj1[0], obj1[0]);
            Assert.Equal(sobj1[1], obj1[1]);
            Assert.Equal(sobj2[0], obj2[0]);
            Assert.Equal(sobj2[1], obj2[1]);
        }

        [Fact]
        public void Slice_Doc_Row_1_Test()
        {
            IDataFrame df1 = _dataFrame;
            var result=df1[(Slice)":5"];
            Assert.Equal(df1.iloc[0][0],result.iloc[0][0]);
        }

        [Fact]
        public void Slice_Doc_Row_2_Test()
        {
            IDataFrame df1 = _dataFrame;
            var result = df1[(Slice)"::2"];
            Assert.Equal(df1.iloc[0][0], result.iloc[0][0]);
        }

        [Fact]
        public void Slice_Doc_Row_3_Test()
        {
            IDataFrame df1 = _dataFrame;
            var result = df1[(Slice)"::-1"];
            int index = df1.Index.Size - 1;
            Assert.Equal(df1.iloc[index][0], result.iloc[0][0]);
        }

        [Fact]
        public void SliceLabel_Doc_Row_1_Test()
        {
            IDataFrame df1 = _dataFrame;
            var result = df1[(SliceLabel)"row2:row4"];
            Assert.Equal(df1.iloc[1][0], result.iloc[0][0]);
        }

        [Fact]
        public void SliceLabel_Doc_Row_2_Test()
        {
            IDataFrame df1 = _dataFrame;
            var result = df1[(SliceLabel)"row2:"];
            Assert.Equal(df1.iloc[1][0], result.iloc[0][0]);
        }

        [Fact]
        public void SliceLabel_Doc_Row_3_Test()
        {
            IDataFrame df1 = _dataFrame;
            var result = df1[(SliceLabel)"row3:"];
            Assert.Equal(df1.iloc[2][0], result.iloc[0][0]);
        }

        [Fact]
        public void Sort_Test()
        {
            IDataFrame df = _dataFrame;
            int sortNum = 2;
            #region test
            //int[] arr1 = { 5, 3, 1, 4, 1 };
            //int[] arr2 = { 6, 2, 8, 7, 9 };
            //int[] arr3 = { 9, 8, 7, 2, 1 };
            //int[] arr4 = { 1, 2, 9, 8, 7 };
            //int[] arr5 = { 2, 3, 8, 1, 9 };
            //var dict = new Dictionary<string, NDArray>
            //{
            //   { "col1",np.array(arr1,typeof(object)) },
            //   { "col2",np.array(arr2,typeof(object))},
            //   { "col3",np.array(arr3,typeof(object)) },
            //   { "col4",np.array(arr4,typeof(object)) },
            //   { "col5",np.array(arr5,typeof(object)) },
            //};

            //var pd = new Pandas();
            //var df = pd.DataFrame<string>(dict, new string[] {
            //    "row1","row2","row3","row4","row5"
            //});
            #endregion
            List<string> columns = new List<string>();
            int r = new Random().Next(0,df.Columns.Size/2);
            int size = df.Columns.Size;
            for (int i=0;i< size; i+= (size/sortNum))
            {
                columns.Add(df[i].Name.ToString());
            }
            DateTime startTime = DateTime.Now;
            var result = df.sort_values(columns.ToArray());
            DateTime endTime = DateTime.Now;
            var time = (endTime - startTime).TotalMilliseconds;
            //string sourStr = ShowDataFrameValues(df);
            //string resStr = ShowDataFrameValues(result);
            string index = columns[0];
            var col=result[index];

            for(int i=0;i<result.Index.Size-1;i++)
            {
                Assert.True(Convert.ToDecimal(col[i]) <= Convert.ToDecimal(col[i + 1]));
            }
        }

        [Fact]
        public void Sort_Desc_Test()
        {
            IDataFrame df = _dataFrame;

            List<string> columns = new List<string>();
            int r = new Random().Next(0, df.Columns.Size / 2);
            for (int i = 0; i < df.Columns.Size; i += r)
            {
                columns.Add(df[i].Name.ToString());
            }
            var result = df.sort_values(columns.ToArray(),false);

            string index = columns[0];
            var col = result[index];

            for (int i = 0; i < result.Index.Size - 1; i++)
            {
                Assert.True(Convert.ToDecimal(col[i]) >= Convert.ToDecimal(col[i + 1]));
            }
        }

        [Fact]
        public void Sort_Index_Test()
        {
            IDataFrame df = _dataFrame;

            List<int> indexs = new List<int>();
            int r = new Random().Next(0, df.Columns.Size / 2);
            for (int i = 0; i < df.Columns.Size; i += r)
            {
                indexs.Add(i);
            }
            //var str = ShowDataFrameValues(_dataFrame);
            var result = df.sort_values(indexs.ToArray());
            //var str2 = ShowDataFrameValues(result);
            int index = indexs[0];
            var col = result[index];

            for (int i = 0; i < result.Index.Size - 1; i++)
            {
                Assert.True(Convert.ToDecimal(col[i]) <= Convert.ToDecimal(col[i + 1]));
            }
        }

        [Fact]
        public void Sort_Index_Desc_Test()
        {
            IDataFrame df = _dataFrame;

            List<int> indexs = new List<int>();
            int r = new Random().Next(0, df.Columns.Size / 2);
            for (int i = 0; i < df.Columns.Size; i += r)
            {
                indexs.Add(i);
            }
            var result = df.sort_values(indexs.ToArray(),false);

            int index = indexs[0];
            var col = result[index];

            for (int i = 0; i < result.Index.Size - 1; i++)
            {
                Assert.True(Convert.ToDecimal(col[i]) >= Convert.ToDecimal(col[i + 1]));
            }
        }



        private string ShowDataFrameValues(IDataFrame dataFrame)
        {
            int rowLength = dataFrame.Index.Size;
            int colLength = dataFrame.Columns.Size;
            StringBuilder sb = new StringBuilder();
            for(int r=0;r<rowLength;r++)
            {
                for(int c=0;c<colLength;c++ )
                {
                    sb.Append(dataFrame[c][r]+"\t");
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}

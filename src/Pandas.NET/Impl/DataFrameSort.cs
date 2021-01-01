using NumSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PandasNet.Impl
{
    public class DataFrameSort : ISort
    {
        private readonly IDataFrame _dataFrame;
        private readonly ISorter _sorter;
        private List<int> _indexs;
        public DataFrameSort(IDataFrame dataFrame, ISorter sorter)
        {
            _dataFrame = dataFrame;
            _sorter = sorter;
        }

        public List<int> SourceIndexs => _indexs;

        public IDataFrame ExcuteSort()
        {
            //总列数
            var colSize = _dataFrame.Columns.Size;
            //总行数
            var rowSize = _dataFrame.Index.Size;
            List<SeriesBase> _seriesBases = new List<SeriesBase>();
            if (_sorter.ColumnIndexs != null && _sorter.ColumnIndexs.Any())//根据列索引获取排序列
            {
                var cols = _sorter.ColumnIndexs;
                var colLength = cols.Count;

                for (int c = 0; c < colLength; c++)
                {
                    _seriesBases.Add(_dataFrame[cols[c]]);
                }
            }
            int seriesLength = _seriesBases.Count;
            int sortLength = seriesLength + 1;
            object[,] sortArray = null;
            sortArray = new object[rowSize, sortLength];//多一列，追加行下标
            for (int c = 0; c < sortLength; c++)//多一列，追加行下标
            {
                for (int r = 0; r < rowSize; r++)
                {
                    if (c < seriesLength)
                    {
                        sortArray[r, c] = _seriesBases[c][r];
                    }
                    else
                    {   //记录行下标
                        sortArray[r, c] = r;
                    }
                }
            }
            switch (_sorter.Kind)
            {
                case SortKind.quicksort:
                    QuickSort(sortArray, 0, rowSize - 1, _sorter.Type==SortType.ascending?true:false);
                    break;
                default:
                    throw new NotImplementedException("该种类排序方式尚未实现。");
            }
            int[] indexs = GetLastCol(sortArray);
            _indexs = indexs.ToList();
            IDataFrame result = CreateDataFrame(_dataFrame, _indexs);
            return result;
        }

        private static IDataFrame CreateDataFrame(IDataFrame df, List<int> indexs)
        {
            var value = df.Values.Array;
            int colSize = df.Columns.Size;
            int rowSize = df.Index.Size;
            var cols = df.Columns.Values.Array;
            var rows = df.Index.Values.Array;
            var rowArray = Array.CreateInstance(df.Index.DType, rows.Length);
            int index = 0;
            foreach (var ind in indexs)
            {
                rowArray.SetValue(rows.GetValue(ind), index);
                index++;
            }
            object[] objs = new object[df.Size];
            int ins = 0;
            for (int c = 0; c < colSize; c++)
            {
                for (int r = 0; r < rowSize; r++)
                {
                    objs[ins] = value.GetValue(indexs[r] + c *rowSize);
                    ins++;
                }
            }
            NDArray nDArray = new NDArray(df.DType, new Shape(rowSize, colSize));
            nDArray.SetData(objs);
            var pd = new Pandas();
            IDataFrame dataFrame = pd.DataFrame(nDArray, null, null, df.DType);
            dataFrame.Index.Values.SetData(rowArray);
            dataFrame.Columns.Values.SetData(cols);
            return dataFrame;
        }

        private int[] GetLastCol(object[,] sortArray)
        {
            int length = sortArray.GetLength(0);
            int colLength = sortArray.GetLength(1);
            int[] indexs = new int[length];
            for (int i = 0; i < length; i++)
            {
                indexs[i] = Convert.ToInt32(sortArray.GetValue(i, colLength - 1));
            }
            return indexs;
        }

        /// <summary>
        /// 获取二维数组中一行的数据
        /// </summary>
        /// <param name="values">二维数据</param>
        /// <param name="rowID">行ID</param>
        /// <returns>返回一行的数据</returns>
        private object[,] GetRowByID(object[,] values, int rowID)
        {
            int rowLength = values.GetLength(1);

            if (rowID > (values.GetLength(0) - 1))
                throw new Exception("rowID超出最大的行索引号!");

            object[,] row = new object[1, rowLength];
            for (int i = 0; i < rowLength; i++)
            {
                row[0, i] = values[rowID, i];
            }
            return row;

        }

        /// <summary>
        /// 快速排序
        /// </summary>
        /// <param name="arr">需要排序的二维数组</param>
        /// <param name="begin">起始指针位置</param>
        /// <param name="end">结束指针位置</param>
        /// <param name="ascending">升序/降序，默认true升序</param>
        private void QuickSort(object[,] arr, int begin, int end, bool ascending = true)
        {
            if (begin >= end) return;   //两个指针重合就返回，结束调用
            int pivotIndex = QuickSortOnce(arr, begin, end, ascending);  //会得到一个基准值下标
            QuickSort(arr, begin, pivotIndex - 1, ascending);  //对基准的左端进行排序  递归
            QuickSort(arr, pivotIndex + 1, end, ascending);   //对基准的右端进行排序  递归
        }

        private delegate bool TempComparer(int value);


        /// <summary>
        /// 快速排序首次确定基准
        /// </summary>
        /// <param name="arr">需要排序的二维数组</param>
        /// <param name="begin">起始指针位置</param>
        /// <param name="end">结束指针位置</param>
        /// <param name="ascending">升序/降序，默认true升序</param>
        /// <returns></returns>
        private int QuickSortOnce(object[,] arr, int begin, int end, bool ascending)
        {
            object[,] pivot = GetRowByID(arr, begin);   //将首行作为基准
            int pivotLength = pivot.GetLength(1);
            object pivotBaseValue = pivot.GetValue(0, 0);
            int arrLength = arr.GetLength(1);
            int i = begin;
            int j = end;
            int compareResult;
            object[,] objIs;
            object[,] objJs;
            TempComparer temp1 = null;
            TempComparer temp2 = null;

            if (ascending)
            {
                temp1 = new TempComparer(GreaterComparer);
                temp2 = new TempComparer(LessComparer);
            }
            else
            {
                temp1 = new TempComparer(LessComparer);
                temp2 = new TempComparer(GreaterComparer);
            }
            while (i < j)
            {
                objJs = GetRowByID(arr, j);

                compareResult = Comparer<object>.Default.Compare(objJs.GetValue(0, 0), pivotBaseValue);

                if (compareResult == 0)
                {
                    for (int c = 0; c < pivotLength; c++)
                    {
                        compareResult = Comparer<object>.Default.Compare(objJs.GetValue(0, c), pivot.GetValue(0, c));
                        if (compareResult != 0)
                            break;
                    }
                }
                if (temp1.Invoke(compareResult) && i < j)
                    j--;
                Array.Copy(arr, j * arrLength, arr, i * arrLength, arrLength);

                objIs = GetRowByID(arr, i);

                compareResult = Comparer<object>.Default.Compare(objIs.GetValue(0, 0), pivotBaseValue);

                if (compareResult == 0)
                {
                    for (int c = 0; c < pivotLength; c++)
                    {
                        compareResult = Comparer<object>.Default.Compare(objIs.GetValue(0, c), pivot.GetValue(0, c));
                        if (compareResult != 0)
                            break;
                    }
                }
                if (temp2.Invoke(compareResult) && i < j)
                    i++;
                Array.Copy(arr, i * arrLength, arr, j * arrLength, arrLength);
            }
            Array.Copy(pivot, 0, arr, i * arrLength, arrLength);
            return i;
        }

        /// <summary>
        /// 小于等于的比较
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool LessComparer(int value)
        {
            return value <= 0;
        }

        /// <summary>
        /// 大于等于的比较
        /// </summary>
        /// <param name="value">比较值</param>
        /// <returns></returns>
        private bool GreaterComparer(int value)
        {
            return value >= 0;
        }
    }
}

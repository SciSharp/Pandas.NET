using NumSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PandasNet.Impl
{
    public partial class DataFrame<TIndex>
    {
        private readonly IList<TIndex> _rawIndex;
        private readonly IList<string> _rawColumns;
        private readonly int _rowSize;
        private readonly Type _dtype;

        public DataFrame(NDArray data, IList<TIndex> index, IList<string> columns, Type dtype)
        {
            this._rawIndex = index;
            this._rowSize = data.shape[0];
            if (columns != null)
            {
                var cols = new List<string>() { };
                this._rawColumns = cols;
                cols.AddRange(columns);
            }

            this._dtype = dtype;

            this.Values = data;
            this.CreateRowIndex();
            this.CreateColumnIndex();
        }

        public DataFrame(IDictionary<string, NDArray> data, IList<TIndex> index = null)
        {
            _rawIndex = index;
            _rowSize = data.First().Value.shape[0];
            if (index != null && _rowSize != index.Count)
            {
                throw new ArgumentException("index的长度非法");
            }
            _rawColumns = data.Keys.ToList();
            var shape = new Shape(_rowSize, 1);
            var nds = data.Values.Select(x => x.reshape(shape)).ToArray();
            foreach (var nd in nds)
            {
                if (Values is null)
                {
                    Values = nd;
                }
                else
                {
                    Values = np.hstack<string>(Values, nd);
                }
            }
            CreateRowIndex();
            CreateColumnIndex();
        }

        protected virtual void AddColumnLabel(string columnName)
        {
            var cols = (Columns.Values.Array as string[]).Select(x => x).Concat(new string[] { columnName })
              .ToArray();
            Columns.Values.SetData(cols);
            _rawColumns.Add(columnName);
        }

        protected virtual void AddColumn(string columnName, NDArray value)
        {
            AddColumnLabel(columnName);
            AddColumnValue(value as NDArray);
        }

        protected virtual void AddColumn(string columnName, SeriesBase value)
        {
            AddColumnLabel(columnName);
            AddColumnValue(value);
        }

        protected virtual void AddColumnValue<T>(T value)
        {
            var insertValues = new NDArray(typeof(T), new Shape(_rowSize, 1));
            for (var i = 0; i < _rowSize; i++)
            {
                insertValues[i, 0].SetData(value);
            }
            AddColumnValue(insertValues);
        }

        protected virtual void AddColumnValue(NDArray value)
        {
            if (value.size != _rowSize)
            {
                throw new ArgumentException("输入的数组长度不等于dataframe行数");
            }
            var insertValues = value.reshape(new Shape(_rowSize, 1));
            Values = np.hstack<int>(Values, insertValues);
        }

        protected virtual void AddColumnValue(SeriesBase value)
        {
            var insertValues = value.Values.reshape(new Shape(_rowSize, 1));
            AddColumnValue(insertValues);
        }

        protected virtual void CreateRowIndex()
        {
            DataIndex index = null;
            if (_rawIndex == null)
            {
                index = new DataIndex(np.arange(_rowSize));
            }
            else
            {
                if (_rawIndex.Count != _rowSize)
                    throw new ArgumentException("传入的行标签与传入的行数不一致！");
                Type indexType = typeof(TIndex);
                switch (indexType.Name)
                {
                    case ("Int32"):
                        index = new DataIndex(_rawIndex.Select(x => Convert.ToInt32(x)).ToArray());
                        break;
                    case "String":
                        index = new DataIndex(_rawIndex.Select(x => x.ToString()).ToArray());
                        break;
                    case "Object":
                        index = new DataIndex(_rawIndex.Select(x => x).ToArray());
                        break;
                }
            }
            Index = index;
        }
        protected virtual void CreateColumnIndex()
        {
            DataIndex index = null;
            if (_rawColumns == null)
            {
                index = new DataIndex(np.arange(Values.shape[0]));
            }
            else
            {
                index = new DataIndex(_rawColumns.Select(x => x.ToString()).ToArray());
            }
            Columns = index;
        }

    }
}

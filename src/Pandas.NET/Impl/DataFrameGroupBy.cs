using NumSharp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PandasNet.Impl
{
    public class DataFrameGroupBy : IGroupBy
    {
        private IDataFrame _dataFrame;
        private IGrouper _grouper;
        private Dictionary<object, IDataIndex> _groups;
        private Dictionary<object, IDataIndex> _indices;
        private SeriesBase _keySeries;
        private Dictionary<object, List<int>> _groupIndices;

        public DataFrameGroupBy(IDataFrame dataFrame, IGrouper grouper)
        {
            _dataFrame = dataFrame;
            _grouper = grouper;
        }

        public Dictionary<object, IDataIndex> Groups
        {
            get
            {

                ExcuteGrouper();
                if (_groups == null)
                {
                    _groups = _groupIndices.ToDictionary(x => x.Key, y =>
                    {
                        var labels = y.Value.Select(z => _keySeries.Index.Values[z].ToString()).ToArray();
                        return (new DataIndex(labels)) as IDataIndex;
                    });
                }
                return _groups;
            }
        }

        public Dictionary<object, IDataIndex> Indices
        {
            get
            {
                ExcuteGrouper();
                if (_indices == null)
                {
                    _indices = _groupIndices.ToDictionary(x => x.Key, y => new DataIndex(y.Value.ToArray<int>()) as IDataIndex);
                }
                return _indices;
            }
        }

        public IDataFrame GetGroup(object name)
        {
            var indices = Indices[name].Values;
            var size = indices.size;
            var colSize = _dataFrame.Columns.Size;
            var array = new NDArray(typeof(object), new Shape(size, colSize));
            var labels = new object[size];
            for (var i = 0; i < size; i++)
            {
                var index = Convert.ToInt32(indices[i]);
                var row = _dataFrame.iloc[index];
                for (var j = 0; j < colSize; j++)
                {
                    array[i, j] = row[j];
                }

                labels[i] = _keySeries.Index.Values[index];
            }

            return new DataFrame<object>(array, labels, _dataFrame.Columns.Values.Storage.GetData<string>(), typeof(object));
        }

        protected virtual void ExcuteGrouper()
        {
            if (_groupIndices != null)
            {
                return;
            }
            _indices = new Dictionary<object, IDataIndex>();
            var rowSize = _dataFrame.Index.Size;

            //TODO:未完成列轴
            if (_grouper.Axis == 0)
            {
                _keySeries = _dataFrame[_grouper.Key];
            }

            _groupIndices = new Dictionary<object, List<int>>();
            for (var i = 0; i < rowSize; i++)
            {
                var currentKey = _keySeries[i];
                if (_groupIndices.ContainsKey(currentKey))
                {
                    _groupIndices[currentKey].Add(i);
                }
                else
                {
                    var list = new List<int>
                    {
                        i
                    };
                    _groupIndices.Add(currentKey, list);
                }
            }

        }
    }
}

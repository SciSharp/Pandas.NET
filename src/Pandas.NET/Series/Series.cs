using System;
using System.Collections.Generic;
using System.Text;

namespace PandasNet
{
    public partial class Series
    {
        Array _data;
        public Array data => _data;
        Series _index;
        public Series index => _index;
        Column _column;
        public Column column => _column;

        public string name => _column.Name;
        public int size => _data.Length;
        public Type dtype => _column.DType;
        private List<int> naIndex = new List<int>();

        public Series(Array data)
        {
            _data = data;
            _column = data switch
            {
                bool[] bool1 => new Column { Name = string.Empty, DType = typeof(bool) },
                int[] int32 => new Column { Name = string.Empty, DType = typeof(int) },
                float[] float32 => new Column { Name = string.Empty, DType = typeof(float) },
                double[] float64 => new Column { Name = string.Empty, DType = typeof(double) },
                DateTime[] strings => new Column { Name = string.Empty, DType = typeof(DateTime) },
                string[] strings => new Column { Name = string.Empty, DType = typeof(string) },
                _ => throw new NotImplementedException("")
            };
        }

        public Series(Column column)
        {
            _column = column;
        }

        public Series(Array data, Column column)
        {
            _data = data;
            _column = column;
        }

        public Series(Array data, Series index, Column column)
        {
            _data = data;
            _index = index;
            _column = column;
        }

        public void Allocate(int count)
        {
            if (_column.DType == typeof(int))
                _data = new int[count];
            else if (_column.DType == typeof(float))
                _data = new float[count];
            else if (_column.DType == typeof(double))
                _data = new double[count];
            else if (_column.DType == typeof(DateTime))
                _data = new DateTime[count];
            else if (_column.DType == typeof(string))
                _data = new string[count];
            else
                throw new NotImplementedException("");
        }

        public void SetIndex(Series index)
        {
            _index = index;
        }

        public object GetValue(int row)
        {
            return _data.GetValue(row);
        }

        public T GetValue<T>(int row)
        {
            return (T)_data.GetValue(row);
        }

        public void SetNull(int row)
        {
            naIndex.Add(row);
        }

        public bool IsNull(int row)
        {
            return naIndex.Contains(row);
        }

        public void SetValue<T>(T value, int row)
        {
            if (dtype == typeof(int) && value is string int32_string)
            {
                int.TryParse(int32_string, out var int32);
                _data.SetValue(int32, row);
            }
            else if (dtype == typeof(float) && value is string float32_string)
            {
                float.TryParse(float32_string, out var float32);
                _data.SetValue(float32, row);
            }
            else if (dtype == typeof(double) && value is string float64_string)
            {
                double.TryParse(float64_string, out var float64);
                _data.SetValue(float64, row);
            }
            else if (dtype == typeof(DateTime) && value is string dt_string)
            {
                DateTime.TryParse(dt_string, out var datetime);
                _data.SetValue(datetime, row);
            }
            else
            {
                _data.SetValue(value, row);
            }
        }

        public override string ToString()
        {
            return $"{name}, {size}, {dtype}";
        }
    }
}

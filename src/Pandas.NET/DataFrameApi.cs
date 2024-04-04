using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PandasNet
{
    public class DataFrameApi
    {
        public DataFrame from_dict(string data)
        {
            // data = {'col_1': [3, 2, 1, 0], 'col_2': ['a', 'b', 'c', 'd']}
            var json = (JObject)JsonConvert.DeserializeObject(data);

            var cols = new List<Series>();
            foreach (var col in json)
            {
                var column = new Column
                {
                    Name = col.Key
                };

                var type = col.Value.First().Type;
                if (type == JTokenType.Integer)
                {
                    var array = GetArray<int>(col.Value);
                    column.DType = typeof(int);
                    var series = new Series(array, column);
                    cols.Add(series);
                }
                else if (type == JTokenType.String)
                {
                    var array = GetArray<string>(col.Value);
                    column.DType = typeof(string);
                    var series = new Series(array, column);
                    cols.Add(series);
                };
            }
            var df = new DataFrame(cols);
            foreach (var s in df.data)
            {
                s.SetIndex(df.index);
            }
            return df;
        }

        T[] GetArray<T>(JToken values)
        {
            var array = new List<T>();
            foreach (var row in values)
                array.Add(row.Value<T>());
            return array.ToArray();
        }
    }
}

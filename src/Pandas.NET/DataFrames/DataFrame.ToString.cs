using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PandasNet
{
    public partial class DataFrame
    {
        public override string ToString()
        {
            var display = "  ";
            var header = string.Join(" ", _columns.Select(x => x.Name));
            display += header;

            for (int i = 0; i < _index.size; i++)
            {
                if (i > 4) break;
                var values = new List<string>();
                values.Add(_index.GetValue(i).ToString());
                for(int col = 0; col < _columns.Count; col++)
                {
                    var value = _data[col].GetValue(i);
                    if (value is double float64)
                        values.Add(Convert.ToSingle(float64).ToString());
                    else
                        values.Add(value.ToString());
                }
                display += "\r\n" + string.Join(" ", values);
            }

            return display;
        }
    }
}

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
            var display = string.Empty;
            var header = string.Join(" ", columns.Select(x => x.Name));
            display += header;

            for (int i = 0; i < index.Length; i++)
            {
                if (i > 4) break;
                var values = new List<string>();
                for(int col = 0; col < columns.Count; col++)
                {
                    values.Add(data[col].GetValue(i).ToString());
                }
                display += "\r\n" + string.Join(" ", values);
            }

            return display;
        }
    }
}

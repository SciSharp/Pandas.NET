using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PandasNet
{
    public partial class DataFrame
    {
        public Series pop(string columnName)
        {
            Series series = null;

            for (int i = 0; i< _columns.Count; i++)
            {
                if(_columns[i].Name == columnName)
                {
                    series = _data[i];
                    _columns.RemoveAt(i);
                    _data.RemoveAt(i);
                }
            }

            return series;
        }
    }
}

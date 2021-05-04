using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PandasNet
{
    public partial class DataFrame
    {
        public void pop(string columnName)
        {
            for(int i = 0; i< _columns.Count; i++)
            {
                if(_columns[i].Name == columnName)
                {
                    _columns.RemoveAt(i);
                    _data.RemoveAt(i);
                }
            }
        }
    }
}

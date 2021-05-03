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
            for(int i = 0; i< columns.Count; i++)
            {
                if(columns[i].Name == columnName)
                {
                    columns.RemoveAt(i);
                    data.RemoveAt(i);
                }
            }
        }
    }
}

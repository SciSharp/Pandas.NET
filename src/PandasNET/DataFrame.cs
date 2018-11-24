using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;
using NumSharp.Core;

namespace PandasNET
{
    /// <summary>
    /// Two-dimensional size-mutable, potentially heterogeneous tabular data structure with labeled axes (rows and columns).
    /// https://pandas.pydata.org/pandas-docs/stable/generated/pandas.DataFrame.html
    /// </summary>
    public class DataFrame<TIndex>
    {
        public DataFrame()
        {
            // _ColumnArrayMapping = new Dictionary<string, NDArray<TData>>();
            // Columns = new Index<string>();
            Columns.values = new NDArray(typeof(string));
        }

        /*public NDArray<TData> this[string column]
        {
            get 
            {
                return (_ColumnArrayMapping[column]);
            }
            set
            {
                if (!_ColumnArrayMapping.ContainsKey(column))
                {
                    if (Columns.Values.Data == null)
                    {
                        Columns.Values.Data = new string[]{ column};
                        Columns.Values.Shape = new Shape(1); 
                    }    
                    else
                    {
                        var puffer = Columns.Values.Data.ToList();
                        puffer.Add(column);
                        Columns.Values.Data = puffer.ToArray();
                        Columns.Values.Shape = new Shape( puffer.Count );
                    } 
                }
                else 
                {

                }
                    
                _ColumnArrayMapping[column] = value;
            }
        } 
        protected Dictionary<string,NDArray<TData>> _ColumnArrayMapping;*/
        public Index<TIndex> Index {get;set;}
        public Index<string> Columns {get;set;}
        public NDArray Values { get; set; }
    }
}

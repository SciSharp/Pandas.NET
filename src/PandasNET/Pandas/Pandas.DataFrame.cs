using NumSharp;
using NumSharp.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PandasNET
{
    public static partial class PandasExtensions
    {
        public static DataFrame<TInd> DataFrame<TInd,TValue>(this Pandas pd, NDArray[] data, IList<TInd> index = null, IList<string> columns = null)
        {
            var df = new DataFrame<TInd>();

            if (columns == null)
                columns = Enumerable.Range(0,(data.Length-1)).Select(x => x.ToString()).ToArray();

            Type indexType = typeof(TInd);

            if (index == null)
            {
                dynamic indexDyn = index;
                switch (indexType.Name)
                {
                    case ("Double"): indexDyn = Enumerable.Range(0,data[0].size).Select(x => (double) x).ToList()  ; break;
                    case ("Int32"): indexDyn = Enumerable.Range(0,data[0].size).ToList()  ; break;
                }
                index = (List<TInd>) indexDyn; 
            }
            else 
            {

            }

            /*for(int idx = 0; idx < columns.Count;idx++)
                df[columns[idx]] = data[idx];*/
        
            //df.Index = new Index<TInd>();
            df.Index.values = new NDArray(typeof(int));
            df.Index.values.reshape(new Shape(index.Count));

            return df;
        }
        public static DataFrame<TInd> DataFrame<TInd>(this Pandas pd, NDArray data, IList<TInd> index = null, IList<string> columns = null)
        {

            var vectors = new NDArray[data.shape[1]];
            
            for (int idx = 0;idx < data.shape[1];idx++)
            {
                vectors[idx] = new NDArray(typeof(int));
                for (int jdx = 0; jdx < data.shape[0];jdx++)
                {
                    vectors[idx][jdx] = data[jdx,idx];
                }
            }
            // return pd.DataFrame<TInd>(vectors,index,columns);

            return null;
        }
    }
}

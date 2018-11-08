using NumSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PandasNET.Extensions
{
    public static partial class PandasExtensions
    {
        public static DataFrame<TInd,TValue> DataFrame<TInd,TValue>(this Pandas pd, NDArray<TValue>[] data, IList<TInd> index = null, IList<string> columns = null)
        {
            var df = new DataFrame<TInd,TValue>();

            if (columns == null)
                columns = Enumerable.Range(0,(data.Length-1)).Select(x => x.ToString()).ToArray();

            Type indexType = typeof(TInd);

            if (index == null)
            {
                dynamic indexDyn = index;
                switch (indexType.Name)
                {
                    case ("Double"): indexDyn = Enumerable.Range(0,data[0].Data.Length).Select(x => (double) x).ToList()  ; break;
                    case ("Int32"): indexDyn = Enumerable.Range(0,data[0].Data.Length).ToList()  ; break;
                }
                index = (List<TInd>) indexDyn; 
            }
            else 
            {

            }

            /*for(int idx = 0; idx < columns.Count;idx++)
                df[columns[idx]] = data[idx];*/
        
            df.Index = new Index<TInd>();
            df.Index.Values = new NDArray<TInd>();
            df.Index.Values.Data = index.ToArray();
            df.Index.Values.Shape = new Shape(index.Count);

            return df;
        }
        public static DataFrame<TInd,TValue> DataFrame<TInd,TValue>(this Pandas pd, NDArray<TValue> data, IList<TInd> index = null, IList<string> columns = null)
        {

            var vectors = new NDArray<TValue>[data.Shape.Shapes[1]];
            
            for (int idx = 0;idx < data.Shape.Shapes[1];idx++)
            {
                vectors[idx] = new NDArray<TValue>();
                vectors[idx].Data = new TValue[data.Shape.Shapes[0]];
                for (int jdx = 0; jdx < data.Shape.Shapes[0];jdx++)
                {
                    vectors[idx].Data[jdx] = data[jdx,idx];
                }
            } 
            return pd.DataFrame<TInd,TValue>(vectors,index,columns);
        }
    }
}

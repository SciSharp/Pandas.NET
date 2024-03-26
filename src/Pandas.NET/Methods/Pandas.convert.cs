using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Globalization;
using OneOf.Types;

namespace PandasNet
{
    public partial class Pandas
    {
        public int int32(double value) => Convert.ToInt32(Math.Floor(value));

        public T[,] array<T>(DataFrame df)
        {
            var data = new T[df.index.size, df.columns.Count];
            var shape = df.shape;

            for (int col = 0; col < shape[1]; col++)
            {
                if (df.data[col].data is T[] buffer)
                {
                    for (int row = 0; row < shape[0]; row++)
                        data[row, col] = buffer[row];
                }
            }
            return data;
        }

        public Toutput[,] array<Tinput, Toutput>(DataFrame df)
        {
            var data = new Toutput[df.index.size, df.columns.Count];
            var shape = df.shape;

            for (int col = 0; col < shape[1]; col++)
            {
                if (df.data[col].data is Tinput[] buffer)
                {
                    for (int row = 0; row < shape[0]; row++)
                        data[row, col] = (Toutput)Convert.ChangeType(buffer[row], typeof(Toutput));
                }
            }
            return data;
        }
    }
}

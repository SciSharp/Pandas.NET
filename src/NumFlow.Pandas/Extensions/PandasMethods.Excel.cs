using System;
using System.Collections.Generic;
using System.Text;

namespace Pandas
{
    public static class PandasMethods
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pd"></param>
        /// <param name="filepath_or_buffer"></param>
        /// <param name="sep"></param>
        /// <param name="names"></param>
        /// <returns></returns>
        public static IDataFrame read_csv(this Pandas pd, string filepath, string sep = ",", string[] names = null)
        {
            throw new NotImplementedException();
        }
    }
}

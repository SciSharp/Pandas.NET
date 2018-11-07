using NumSharp;
using System;
using System.Collections.Generic;
using System.Text;
using PandasNET;

namespace PandasNET
{
    public class Index<T>
    {
        public Index()
        {
            
        }   
        public NDArray<T> Values { get; set; } 
    }
}

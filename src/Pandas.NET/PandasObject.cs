using NumSharp.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PandasNet
{
    public abstract class PandasObject : IPandasObject
    {
        public object Name { get; set; }

        public virtual NDArray Values { get; set; }

        /// <summary>
        /// NDArray的数据类型
        /// </summary>
        public  Type DType => Values.dtype;

        /// <summary>
        /// 维度
        /// </summary>
        public int NDIM => Values.ndim;

        /// <summary>
        /// 
        /// </summary>
        public virtual Shape Shape => Values.Storage.Shape;

        /// <summary>
        /// 元素总数
        /// </summary>
        public virtual int Size => Values.size;
    }
}

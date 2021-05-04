using System;
using System.Linq;

namespace PandasNet
{
    public partial class Series
    {
        public object this[Series mask]
        {
            get
            {
                if(mask.data is bool[])
                {
                    var ms = new MaskedSeries(_data, _index, _column);
                    ms.SetMask(mask);
                    return ms;
                }
                throw new NotImplementedException("");
            }

            set
            {
                if (mask.data is bool[] masks)
                {
                    for (int row = 0; row < masks.Length; row++)
                    {
                        if (masks[row])
                            _data.SetValue(value, row);
                    }
                    return;
                }
                throw new NotImplementedException("");
            }
        }
    }
}

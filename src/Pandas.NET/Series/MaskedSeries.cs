using System;
using System.Collections.Generic;
using System.Text;

namespace PandasNet
{
    public class MaskedSeries : Series
    {
        private Series _mask;
        public MaskedSeries(Array data, Series index, Column column) : 
            base(data, index, column)
        {

        }

        public void SetMask(Series mask)
        {
            _mask = mask;
        }
    }
}

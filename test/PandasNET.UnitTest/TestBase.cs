using NumSharp.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PandasNET.UnitTest
{
    public class TestBase
    {
        protected NumPy np;
        protected Pandas pd;

        public TestBase()
        {
            np = new NumPy();
            pd = new Pandas();
        }
    }
}

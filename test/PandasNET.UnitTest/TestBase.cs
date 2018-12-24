using NumSharp.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PandasNET.UnitTest
{
    public class TestBase
    {
        protected Pandas pd;

        public TestBase()
        {
            pd = new Pandas();
        }
    }
}

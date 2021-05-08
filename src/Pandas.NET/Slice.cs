using System;
using System.Collections.Generic;
using System.Text;

namespace PandasNet
{
    public class Slice
    {
        public int Start { get; set; }
        public int? Stop { get; set; }
        public int Step { get; set; }

        public Slice(int start, int? stop = null, int step = 1)
        {
            Start = start;
            Stop = stop;
            Step = step;
        }

        public override string ToString()
            => $"{Start}:{Stop}:{Step}";
    }
}

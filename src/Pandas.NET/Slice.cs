using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace PandasNet
{
    public class Slice 
    {
        public int Start { protected set; get; }

        public int End { protected set; get; }

        public int Step { protected set; get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sliceRule"></param>
        public static implicit operator Slice(string sliceRule)
        { 
            return new Slice(sliceRule);
        }

        Slice(string sliceRule)
        {
            if (string.IsNullOrEmpty(sliceRule))
            {
                throw new ArgumentNullException("the sliceRule is null or empty");
            }
            Regex regex = new Regex(@"(?<start>(-\d*)|(\d*)):(?<end>(-\d*)|(\d*)):?(?<step>(-\d*)|(\d*))");
            Match match = regex.Match(sliceRule);

            Start = 0;
            End = int.MaxValue;
            Step = 1;
            if (match.Success)
            {
                var start = match.Groups["start"].Value;
                var end = match.Groups["end"].Value;
                var step = match.Groups["step"].Value;

                Start = !string.IsNullOrEmpty(start) ? Convert.ToInt32(start) : Start;
                End = !string.IsNullOrEmpty(end) ? Convert.ToInt32(end) : End;
                Step = !string.IsNullOrEmpty(step) ? Convert.ToInt32(step) : Step;
            }
            else
            {
                throw new ArgumentException("the sliceRule is not correct format");
            }

        }
    }
}

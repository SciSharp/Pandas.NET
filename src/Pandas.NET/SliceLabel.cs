using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace PandasNet
{
    public class SliceLabel
    {
        public string StartLabel { protected set; get; }

        public string EndLabel { protected set; get; }

        public int Step { protected set; get; }


        SliceLabel(string sliceRule)
        {
            if (string.IsNullOrEmpty(sliceRule))
            {
                throw new ArgumentNullException("the sliceRule is null or empty");
            }
            Regex regex = new Regex(@"(?<start>\w*):(?<end>\w*):?(?<step>(-\d*)|(\d*))");
            Match match = regex.Match(sliceRule);

            StartLabel = string.Empty;
            EndLabel = string.Empty;
            Step = 1;
            if (match.Success)
            {
                var startLabel = match.Groups["start"].Value;
                var endLabel = match.Groups["end"].Value;
                var step = match.Groups["step"].Value;

                StartLabel = startLabel;
                EndLabel = endLabel;
                Step = !string.IsNullOrEmpty(step) ? Convert.ToInt32(step) : Step;
            }
            else
            {
                throw new ArgumentException("the sliceRule is not correct format");
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sliceRule"></param>
        public static implicit operator SliceLabel(string sliceRule)
        {
            return new SliceLabel(sliceRule);
        }

    }
}

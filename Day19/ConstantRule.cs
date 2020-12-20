using System;
using System.Collections.Generic;
using System.Linq;

namespace Day19
{
    public class ConstantRule : IRule
    {
        public string Index { get; set; }
        public string StringContent { get; set; }

        public Tuple<bool, string> RunCheck(string input, IEnumerable<IRule> rulesList)
        {
            if (input.First() == StringContent[2])
            {
                return new Tuple<bool, string>(true, input.Substring(1));
            }

            return new Tuple<bool, string>(false, input);
        }
    }
}
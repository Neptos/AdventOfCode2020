using System;
using System.Collections.Generic;
using System.Linq;

namespace Day19
{
    public class ConstantRule : IRule
    {
        public string Index { get; set; }
        public string StringContent { get; set; }

        public Tuple<bool, Tuple<string, string>> RunCheck(string input, IEnumerable<IRule> rulesList, List<string> rulesUsed)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return new Tuple<bool, Tuple<string, string>>(true, new Tuple<string, string>(input, string.Join(" ", rulesUsed)));
            }
            if (input.First() == StringContent[2])
            {
                return new Tuple<bool, Tuple<string, string>>(true, new Tuple<string, string>(input.Substring(1), string.Join(" ", rulesUsed)));
            }

            return new Tuple<bool, Tuple<string, string>>(false, new Tuple<string, string>(input, string.Join(" ", "")));
        }
    }
}
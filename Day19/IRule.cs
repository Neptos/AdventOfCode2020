using System;
using System.Collections.Generic;

namespace Day19
{
    public interface IRule
    {
        string Index { get; set; }
        string StringContent { get; set; }
        Tuple<bool, string> RunCheck(string input, IEnumerable<IRule> rulesList);
    }
}
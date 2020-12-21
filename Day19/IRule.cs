using System;
using System.Collections.Generic;

namespace Day19
{
    public interface IRule
    {
        string Index { get; set; }
        string StringContent { get; set; }
        Tuple<bool, Tuple<string, string>> RunCheck(string input, IEnumerable<IRule> rulesList, List<string> rulesUsed);
    }
}
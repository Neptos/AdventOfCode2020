using System;
using System.Collections.Generic;
using System.Linq;

namespace Day19
{
    public class PointerRule : IRule
    {
        public string Index { get; set; }
        public string StringContent { get; set; }

        public Tuple<bool, Tuple<string, string>> RunCheck(string input, IEnumerable<IRule> rulesList, List<string> rulesUsed)
        {
            var result = new Tuple<bool, Tuple<string, string>>(false, new Tuple<string, string>(input, ""));
            IEnumerable<string> andRules;
            if (StringContent.Contains("|"))
            {
                var orRules = StringContent.Split("|");
                var success = false;
                foreach (var orRule in orRules)
                {
                    andRules = orRule.Split(" ").Where(s => !string.IsNullOrWhiteSpace(s));
                    result = CheckAndRules(input, andRules.ToList(), rulesList, rulesUsed.ToList());
                    if (result.Item1)
                    {
                        success = true;
                        break;
                    }
                }

                if (!success)
                {
                    return new Tuple<bool, Tuple<string, string>>(false, new Tuple<string, string>(input, ""));
                }
                return result;
            }
            andRules = StringContent.Split(" ").Where(s => !string.IsNullOrWhiteSpace(s));
            return CheckAndRules(input, andRules.ToList(), rulesList, rulesUsed.ToList());
        }

        private Tuple<bool, Tuple<string, string>> CheckAndRules(string input, List<string> andRules, IEnumerable<IRule> rulesList, List<string> rulesUsed)
        {
            var newInput = input;
            var newRulesUsedList = rulesUsed;
            var result = new Tuple<bool, Tuple<string, string>>(false, new Tuple<string, string>(input, ""));

            foreach (var andRule in andRules.Where(r => !string.IsNullOrWhiteSpace(r)))
            {
                if (string.IsNullOrWhiteSpace(newInput))
                {
                    break;
                }
                newRulesUsedList.Add(andRule);
                result = rulesList.First(rl => rl.Index == andRule).RunCheck(newInput, rulesList, newRulesUsedList.ToList());
                if (!result.Item1)
                {
                    newRulesUsedList.Remove(andRule);
                    return new Tuple<bool, Tuple<string, string>>(false, new Tuple<string, string>(input, ""));
                }

                newInput = result.Item2.Item1;
                newRulesUsedList = result.Item2.Item2.Split(" ").ToList();
            }

            result = new Tuple<bool, Tuple<string, string>>(result.Item1,
                new Tuple<string, string>(result.Item2.Item1, string.Join(" ", newRulesUsedList)));
            return result;
        }
    }
}
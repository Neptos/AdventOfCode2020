using System;
using System.Collections.Generic;
using System.Linq;

namespace Day19
{
    public class PointerRule : IRule
    {
        public string Index { get; set; }
        public string StringContent { get; set; }

        public Tuple<bool, string> RunCheck(string input, IEnumerable<IRule> rulesList)
        {
            var result = new Tuple<bool, string>(false, input);
            IEnumerable<string> andRules;
            if (StringContent.Contains("|"))
            {
                var orRules = StringContent.Split("|");
                foreach (var orRule in orRules)
                {
                    andRules = orRule.Split(" ").Where(s => !string.IsNullOrWhiteSpace(s));
                    result = CheckAndRules(input, andRules.ToList(), rulesList);
                    if (result.Item1)
                    {
                        break;
                    }
                }
                return result;
            }
            andRules = StringContent.Split(" ").Where(s => !string.IsNullOrWhiteSpace(s));
            return CheckAndRules(input, andRules.ToList(), rulesList);
        }

        private Tuple<bool, string> CheckAndRules(string input, List<string> andRules, IEnumerable<IRule> rulesList)
        {
            var newInput = input;
            var result = new Tuple<bool, string>(false, input);

            foreach (var andRule in andRules.Where(r => !string.IsNullOrWhiteSpace(r)))
            {
                result = rulesList.First(rl => rl.Index == andRule).RunCheck(newInput, rulesList);
                if (!result.Item1)
                {
                    return new Tuple<bool, string>(false, input);
                }

                newInput = result.Item2;
            }

            return result;
        }
    }
}
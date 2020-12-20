﻿using System;
using System.Linq;
using Common;

namespace Day19
{
    internal static class Program
    {
        private static void Main()
        {
            var input = InputReader.ReadInput("input.txt", s => s);
            var rules = input
                .TakeWhile(s => !string.IsNullOrWhiteSpace(s))
                .Select(ConvertLineToRule);
            var messages = input
                .SkipWhile(s => !string.IsNullOrWhiteSpace(s))
                .Skip(1);

            var result = 0;
            var rule0 = rules.First(r => r.Index == "0");
            foreach (var message in messages)
            {
                var output = rule0.RunCheck(message, rules);
                if (output.Item1 && string.IsNullOrWhiteSpace(output.Item2))
                {
                    result++;
                }
            }
            
            Console.WriteLine($"Part1: {result}");
        }

        private static IRule ConvertLineToRule(string stringRule)
        {
            var stringRuleSplit = stringRule.Split(":");
            var ruleIndex = stringRuleSplit[0];
            var content = stringRuleSplit[1];
            if (content.Contains("\""))
            {
                return new ConstantRule
                {
                    Index = ruleIndex,
                    StringContent = content
                };
            }

            return new PointerRule
            {
                Index = ruleIndex,
                StringContent = content
            };
        }
    }
}
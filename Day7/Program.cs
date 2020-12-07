using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace Day7
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var rules = InputReader.ReadInput("input.txt", ConvertToRule);

            var result = GetNrOfBagsBagCanFitIn("shiny gold", rules.ToList());
            
            Console.WriteLine($"Part1: {result}");

            result = GetNrOfBagsInBag("shiny gold", rules.ToList());
            
            Console.WriteLine($"Part2: {result}");
        }

        private static int GetNrOfBagsBagCanFitIn(string bagName, List<Rule> rules)
        {
            rules.Remove(rules.First(r => r.Bag == bagName));
            var bags = rules
                .Where(rule => rule.Contain
                    .Any(bc => bc.Bag == bagName))
                .ToList();

            return bags.Count + bags
                .Where(bag => bag.Contain.Count != 0)
                .Sum(bag => GetNrOfBagsBagCanFitIn(bag.Bag, rules));
        }

        private static int GetNrOfBagsInBag(string bagName, List<Rule> rules)
        {
            var bagRule = rules.First(r => r.Bag == bagName);
            var sum = bagRule.Contain.Sum(b => b.Count);

            foreach (var bagColorCount in bagRule.Contain)
            {
                for (var nrOfBags = 0; nrOfBags < bagColorCount.Count; nrOfBags++)
                {
                    sum += GetNrOfBagsInBag(bagColorCount.Bag, rules);
                }
            }

            return sum;
        }

        private static Rule ConvertToRule(string s)
        {
            var words = s.Split(" ");
            var bag = string.Join(" ", words.Take(2));
            var contain = new List<BagColorCount>();

            foreach (var containBags in string.Join(" ", words.Skip(4)).Split(", "))
            {
                if (containBags.StartsWith("no"))
                {
                    continue;
                }
                var containBagsWords = containBags.Split(" ");
                var count = int.Parse(containBagsWords.First());
                var bagName = string.Join(" ", containBagsWords.Skip(1).Take(2));
                
                contain.Add(new BagColorCount
                {
                    Bag = bagName,
                    Count = count
                });
            }
            
            return new Rule
            {
                Bag = bag,
                Contain = contain
            };
        }
        
        private class Rule
        {
            public string Bag { get; set; }
            public List<BagColorCount> Contain { get; set; }
        }

        private class BagColorCount
        {
            public int Count { get; set; }
            public string Bag { get; set; }
        }
    }
}
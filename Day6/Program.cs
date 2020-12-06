using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Common;

namespace Day6
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var personAnswers = InputReader.ReadInput("input.txt", s => s);
            var groups = CreateGroups(personAnswers);

            var part1Result = groups
                    .Select(s => s.Answers
                        .Distinct()
                        .Count())
                    .Sum();
            
            Console.WriteLine($"Part1: {part1Result}");

            var part2Result = groups
                .Select(g => g.Answers
                    .Distinct()
                    .Count(distinctChar => g.Answers
                        .Count(c => c == distinctChar) == g.Size))
                .Sum();
            
            Console.WriteLine($"Part2: {part2Result}");
        }

        private static List<Group> CreateGroups(ImmutableList<string> personAnswers)
        {
            var groups = new List<Group>();

            var groupAnswersAggregator = "";
            var groupSizeAggregator = 0;
            foreach (var personAnswer in personAnswers)
            {
                if (personAnswer.Length == 0)
                {
                    groups.Add(new Group
                    {
                        Answers = groupAnswersAggregator,
                        Size = groupSizeAggregator
                    });
                    groupAnswersAggregator = "";
                    groupSizeAggregator = 0;
                    continue;
                }

                groupAnswersAggregator += personAnswer;
                groupSizeAggregator++;
            }

            // Since the input doesn't end in an empty line we need to add the last group here
            groups.Add(new Group
            {
                Answers = groupAnswersAggregator,
                Size = groupSizeAggregator
            });
            return groups;
        }

        private class Group
        {
            public string Answers { get; set; }
            public int Size { get; set; }
        }
    }
}
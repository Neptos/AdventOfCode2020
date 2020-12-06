using System;
using System.Linq;
using Common;

namespace Day6
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var groupAnswers = InputReader.ReadInput(
                                                "input.txt",
                                                s => s,
                                                true);

            var result = groupAnswers
                    .Select(s => s
                        .Distinct()
                        .Count())
                    .Sum();
            
            Console.WriteLine($"Part1: {result}");
        }
    }
}
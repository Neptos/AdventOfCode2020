using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using Common;

namespace Day9
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var input = InputReader.ReadInput("input.txt", long.Parse);

            var timer = new Stopwatch();
            
            timer.Start();
            var result = FindInvalidNumber(input);
            timer.Stop();
            
            Console.WriteLine($"Part1: {result}");
            Console.WriteLine($"Time: {timer.Elapsed}");
            
            timer.Restart();
            result = FindEncryptionWeakness(input, result);
            timer.Stop();
            
            Console.WriteLine($"Part2: {result}");
            Console.WriteLine($"Time: {timer.Elapsed}");
        }

        private static long FindEncryptionWeakness(ImmutableList<long> input, long invalidNumber)
        {
            for (var i = 0; i < input.Count; i++)
            {
                var smallestNumber = long.MaxValue;
                var largestNumber = long.MinValue;
                long weakness = 0;
                for (var j = i; weakness < invalidNumber; j++)
                {
                    var value = input[i + j];
                    largestNumber = value > largestNumber ? value : largestNumber;
                    smallestNumber = value < smallestNumber ? value : smallestNumber;
                    weakness += value;
                }

                if (weakness == invalidNumber)
                {
                    return largestNumber + smallestNumber;
                }
            }

            return 0;
        }

        private static long FindInvalidNumber(ImmutableList<long> input)
        {
            var index = 24;
            foreach (var value in input.Skip(25))
            {
                index++;
                var previousLines = input.Skip(index - 25).Take(25).ToList();
                var found = previousLines
                    .Any(a => previousLines
                        .Any(b => b != a && b + a == value));

                if (!found)
                {
                    return value;
                }
            }

            return 0;
        }
    }
}
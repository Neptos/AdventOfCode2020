using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using Common;

namespace Day10
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var adapters = InputReader.ReadInput("input.txt", int.Parse);
            var timer = new Stopwatch();
            
            timer.Start();
            var result = Part1(adapters);
            timer.Stop();
            
            Console.WriteLine($"Part1: {result}");
            Console.WriteLine($"Time: {timer.Elapsed}");

            timer.Restart();
            var resultPart2 = Part2(adapters, 0, new List<Tuple<int, long>>());
            timer.Stop();
            
            Console.WriteLine($"Part2: {resultPart2}");
            Console.WriteLine($"Time: {timer.Elapsed}");
        }

        private static long Part2(ImmutableList<int> adapters, int currentAdapter, List<Tuple<int, long>> cache)
        {
            var cachedAdapter = cache.FirstOrDefault(c => c.Item1 == currentAdapter);
            if (cachedAdapter != null)
            {
                return cachedAdapter.Item2;
            }
            var result = adapters
                .Where(a => a > currentAdapter && a <= currentAdapter + 3)
                .Sum(item => Part2(adapters, item, cache));

            result = result == 0 ? 1 : result;
            cache.Add(new Tuple<int, long>(currentAdapter, result));
            
            return result;
        }
        
        private static int Part1(ImmutableList<int> adapters)
        {
            var sortedAdapters = adapters.Sort();

            var nrOf1Diff = 0;
            var nrOf3Diff = 1;
            var previousAdapter = 0;
            foreach (var adapter in sortedAdapters)
            {
                switch (adapter - previousAdapter)
                {
                    case 1:
                        nrOf1Diff++;
                        break;
                    case 3:
                        nrOf3Diff++;
                        break;
                }

                previousAdapter = adapter;
            }

            return nrOf1Diff*nrOf3Diff;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Common;

namespace Day13
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var input = InputReader.ReadInput(s => s.Split(","));
            var earliestDepartTime = input
                .First()
                .Select(int.Parse)
                .First();
            var busTable = input
                .Last()
                .Select(SpecialParse)
                .Where(b => b.Item2 != 0)
                .ToList();
            var filteredBusTable = input
                .Last()
                .Where(b => b != "x")
                .Select(int.Parse)
                .ToList();
            var timer = new Stopwatch();

            timer.Start();
            var result = Part1(earliestDepartTime, filteredBusTable);
            timer.Stop();
            
            Console.WriteLine($"Part1: {result}");
            Console.WriteLine($"Time: {timer.Elapsed}");

            timer.Restart();
            long timestamp = busTable.Max(b => b.Item2);
            var step = busTable.Max(b => b.Item2);
            var stepIndex = busTable
                .First(b => b.Item2 == busTable
                    .Max(b2 => b2.Item2)).Item1;
            var counter = 0;
            var counterCounter = 0;
            while (!AllBussesDepartAtTheirIndex(busTable, timestamp, stepIndex))
            {
                if (counter > 100000000)
                {
                    counterCounter++;
                    Console.WriteLine($"Currently at: {timestamp} - Time per 100M: {timer.Elapsed/counterCounter}");
                    counter = 0;
                }
                counter++;
                timestamp += step;
            }
            timer.Stop();

            Console.WriteLine($"Part2: {timestamp-stepIndex}");
            Console.WriteLine($"Time: {timer.Elapsed}");
        }

        private static Tuple<int, int> SpecialParse(string arg, int index)
        {
            return arg == "x"
                ? new Tuple<int, int>(index, 0)
                : new Tuple<int, int>(index, int.Parse(arg));
        }

        private static bool AllBussesDepartAtTheirIndex(List<Tuple<int, int>> busTable, long timestamp, int maxIndex)
        {
            var checkTimestamp = timestamp - maxIndex;
            foreach (var (index, busId) in busTable)
            {
                if ((checkTimestamp + index) % busId != 0) return false;
            }

            return true;
        }

        private static int Part1(int earliestDepartTime, List<int> filteredBusTable)
        {
            var bestBus = 0;
            var waitTime = 0;
            var to = earliestDepartTime + filteredBusTable.Max();
            for (var departTime = earliestDepartTime; departTime < to; departTime++)
            {
                if (filteredBusTable.Any(b => departTime % b == 0))
                {
                    bestBus = filteredBusTable.First(b => departTime % b == 0);
                    waitTime = departTime - earliestDepartTime;
                    break;
                }
            }

            var result = waitTime * bestBus;
            return result;
        }
    }
}
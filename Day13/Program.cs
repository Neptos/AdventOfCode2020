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
            long checkTimestamp;
            bool cont;
            while (true)
            {
                if (counter > 1000000000)
                {
                    counterCounter++;
                    Console.WriteLine($"Currently at: {timestamp} - Time per 1B: {timer.Elapsed/counterCounter}");
                    counter = 0;
                }
                counter++;
                timestamp += step;
                checkTimestamp = timestamp - stepIndex;
                cont = false;
                for (var i = 0; i < busTable.Count; i++)
                {
                    if ((checkTimestamp + busTable[i].Item1) % busTable[i].Item2 == 0) continue;
                    cont = true;
                    break;
                }

                if (cont)
                {
                    continue;
                }

                break;
            }
            timer.Stop();

            Console.WriteLine($"Part2: {timestamp-stepIndex}");
            Console.WriteLine($"Time: {timer.Elapsed}");
        }

        private static ValueTuple<int, int> SpecialParse(string arg, int index)
        {
            return arg == "x"
                ? ValueTuple.Create(index, 0)
                : ValueTuple.Create(index, int.Parse(arg));
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
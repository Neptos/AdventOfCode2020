using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using Common;

namespace Day3
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var map = InputReader.ReadInput("input.txt", s => s);

            var timer = new Stopwatch();
            timer.Start();
            var nrOfTrees = TraverseSlope(map, 3, 1);
            timer.Stop();
            
            Console.WriteLine($"Part1: {nrOfTrees}");
            Console.WriteLine($"Time: {timer.Elapsed}");
            
            var traverses = new List<Tuple<int, int>>
            {
                new Tuple<int, int>(1,1),
                new Tuple<int, int>(3,1),
                new Tuple<int, int>(5,1),
                new Tuple<int, int>(7,1),
                new Tuple<int, int>(1,2)
            };

            timer.Restart();
            var result = traverses
                                .Select(t => TraverseSlope(map, t.Item1, t.Item2))
                                .Aggregate(1, (a, b) => a * b);
            timer.Stop();
            
            Console.WriteLine($"Part2: {result}");
            Console.WriteLine($"Time: {timer.Elapsed}");
        }

        private static int TraverseSlope(ImmutableList<string> map, int xTraverse, int yTraverse)
        {
            var xLength = map[0].Length;
            var currentXPos = 0;
            var nrOfTrees = 0;
            for (var currentYPos = yTraverse; currentYPos < map.Count; currentYPos += yTraverse)
            {
                currentXPos += xTraverse;

                if (currentXPos >= xLength)
                {
                    currentXPos -= xLength;
                }

                if (map[currentYPos][currentXPos] == '#')
                {
                    nrOfTrees++;
                }
            }

            return nrOfTrees;
        }
    }
}
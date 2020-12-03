using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Common;

namespace Day3
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var map = InputReader.ReadInput("input.txt", s => s);

            var nrOfTrees = TraverseSlope(map, 3, 1);
            
            Console.WriteLine($"Part1: {nrOfTrees}");
            
            var traverses = new List<Tuple<int, int>>
            {
                new Tuple<int, int>(1,1),
                new Tuple<int, int>(3,1),
                new Tuple<int, int>(5,1),
                new Tuple<int, int>(7,1),
                new Tuple<int, int>(1,2)
            };

            var treesPerTraverse = traverses.Select(t => TraverseSlope(map, t.Item1, t.Item2));
            var result = treesPerTraverse.Aggregate(1, (a, b) => a * b);
            
            Console.WriteLine($"Part2: {result}");
        }

        private static int TraverseSlope(ImmutableList<string> map, int xTraverse, int yTraverse)
        {
            var xLength = map.First().Length;
            var yLength = map.Count;
            var currentYPos = 0;
            var currentXPos = 0;
            var nrOfTrees = 0;
            while (currentYPos < yLength - 1)
            {
                currentXPos += xTraverse;
                currentYPos += yTraverse;

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
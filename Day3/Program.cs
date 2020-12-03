using System;
using System.Collections.Generic;
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
            var xLength = map[0].Length;
            var flattenedMap = string.Join("", map);
            var traverseLength = xLength + 3;
            
            var timer = new Stopwatch();
            timer.Start();
            var nrOfTrees = TraverseSlope(flattenedMap, traverseLength, xLength);
            timer.Stop();
            
            Console.WriteLine($"Part1: {nrOfTrees}");
            Console.WriteLine($"Time: {timer.Elapsed}");
            
            var traverses = new List<int>
            {
                xLength+1,
                xLength+3,
                xLength+5,
                xLength+7,
                xLength*2+1
            };

            timer.Restart();
            var result = traverses
                                .Select(t => TraverseSlope(flattenedMap, t, xLength))
                                .Aggregate(1, (a, b) => a * b);
            timer.Stop();
            
            Console.WriteLine($"Part2: {result}");
            Console.WriteLine($"Time: {timer.Elapsed}");
        }

        private static int TraverseSlope(string flattenedMap, int traverseLength, int xLength)
        {
            var yCount = traverseLength > 60 ? 2 : 1;
            var nrOfTrees = 0;
            for (var currentPos = traverseLength; currentPos < flattenedMap.Length; currentPos += traverseLength)
            {
                if (currentPos % xLength < traverseLength - xLength*yCount)
                {
                    currentPos -= xLength;
                }
                if (flattenedMap[currentPos] == '#')
                {
                    nrOfTrees++;
                }
            }

            return nrOfTrees;
        }
    }
}
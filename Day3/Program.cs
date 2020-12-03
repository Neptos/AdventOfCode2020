using System;
using System.Linq;
using Common;

namespace Day3
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var map = InputReader.ReadInput("input.txt", s => s);

            var xLength = map.First().Length;
            var yLength = map.Count;
            var currentYPos = 0;
            var currentXPos = 0;
            var nrOfTrees = 0;
            
            while (currentYPos < yLength-1)
            {
                currentXPos += 3;
                currentYPos += 1;

                if (currentXPos >= xLength)
                {
                    currentXPos -= xLength;
                }

                if (map[currentYPos][currentXPos] == '#')
                {
                    nrOfTrees++;
                }
            }
            
            Console.WriteLine($"Result: {nrOfTrees}");
        }
    }
}
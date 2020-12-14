using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace Day14
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var input = InputReader.ReadInput(s => s);
            var andMask = "";
            var orMask = "";
            var memory = new List<Tuple<int, long>>();

            foreach (var line in input)
            {
                if (line.StartsWith("mask"))
                {
                    var mask = line.Split(" ").Last();
                    andMask = mask.Replace('X', '1');
                    orMask = mask.Replace('X', '0');
                }
                else
                {
                    var intAndMask = Convert.ToInt64(andMask, 2);
                    var intOrMask = Convert.ToInt64(orMask, 2);
                    var value = long.Parse(line.Split(" ").Last());
                    var address = int.Parse(line.Split("[")[1].Split("]")[0]);
                    value &= intAndMask;
                    value |= intOrMask;
                    if (memory.Any(m => m.Item1 == address))
                    {
                        memory.Remove(memory.First(m => m.Item1 == address));
                    }
                    memory.Add(new Tuple<int, long>(address, value));
                }
            }
            
            Console.Write($"Part1: {memory.Sum(m => m.Item2)}");
        }
    }
}
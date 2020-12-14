using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using Common;

namespace Day14
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var input = InputReader.ReadInput(s => s);
            var timer = new Stopwatch();
            
            timer.Start();
            var result = Part1(input);
            timer.Stop();
            
            Console.WriteLine($"Part1: {result}");
            Console.WriteLine($"Time: {timer.Elapsed}");
            
            timer.Restart();
            result = Part2(input);
            timer.Stop();
            
            Console.WriteLine($"Part2: {result}");
            Console.WriteLine($"Time: {timer.Elapsed}");
        }

        private static long Part2(ImmutableList<string> input)
        {
            var memory = new Dictionary<long, long>();
            var orMask = "";
            var mask = "";
            
            foreach (var line in input)
            {
                if (line.StartsWith("mask"))
                {
                    mask = line.Split(" ").Last();
                    orMask = mask.Replace('X', '0');
                }
                else
                {
                    var intOrMask = Convert.ToInt64(orMask, 2);
                    var value = long.Parse(line.Split(" ").Last());
                    var address = long.Parse(line.Split("[")[1].Split("]")[0]);
                    address |= intOrMask;
                    
                    var xPermutations = GetPermutationsWithRepetition(
                        new List<char>{'0', '1'},
                        mask.Count(c => c == 'X'));

                    foreach (var xPermutation in xPermutations)
                    {
                        var newAddress = address;
                        var xIndex = -1;
                        foreach (var character in xPermutation)
                        {
                            xIndex = mask.IndexOf('X', xIndex+1);
                            newAddress = ReplaceBinaryChar(newAddress, mask.Length - xIndex - 1, character);
                        }

                        memory[newAddress] = value;
                    }
                }
            }

            return memory.Sum(pair => pair.Value);
        }
        
        private static List<List<T>> GetPermutationsWithRepetition<T>(IReadOnlyCollection<T> list, int length)
        {
            if (length == 1) return list.Select(t => new List<T> { t }).ToList();
            return GetPermutationsWithRepetition(list, length - 1)
                .SelectMany(t => list, 
                    (t1, t2) => t1.Concat(new[] { t2 }).ToList()).ToList();
        }

        private static long ReplaceBinaryChar(long address, int index, char character)
        {
            var mask = CreateMask(index, character);
            if (character == '0')
            {
                address &= mask;
            }
            else
            {
                address |= mask;
            }

            return address;
        }

        private static long CreateMask(int index, char character)
        {
            var mask = (long)Math.Pow(2, index);
            if (character == '0')
            {
                return long.MaxValue - mask;
            }

            return mask;
        }

        private static long Part1(ImmutableList<string> input)
        {
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

            return memory.Sum(m => m.Item2);
        }
    }
}
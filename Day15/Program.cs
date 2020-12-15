using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Day15
{
    internal static class Program
    {
        private static void Main()
        {
            var startingNumbers = new List<int> { 0, 5, 4, 1, 10, 14, 7 };
            var timer = new Stopwatch();
            
            timer.Start();
            var result = GetNumberSpokenFor(startingNumbers, 2020);
            timer. Stop();
            
            Console.WriteLine($"Part1: {result}");
            Console.WriteLine($"Time: {timer.Elapsed}");
            
            startingNumbers = new List<int>(30000000) { 0, 5, 4, 1, 10, 14, 7 };
            
            timer.Restart();
            result = GetNumberSpokenFor(startingNumbers, 30000000);
            timer. Stop();
            
            Console.WriteLine($"Part2: {result}");
            Console.WriteLine($"Time: {timer.Elapsed}");
        }

        internal static int GetNumberSpokenFor(List<int> spokenNumbers, int thNumber)
        {
            var cache = new Dictionary<int, List<int>>();
            for (var i = 0; i < spokenNumbers.Count; i++)
            {
                cache[spokenNumbers[i]] = new List<int>{ i };
            }
            var currentNumber = 0;
            for (var i = spokenNumbers.Count; i < thNumber; i++)
            {
                var lastSpokenNumber = spokenNumbers.Last();
                currentNumber = 0;

                if (cache.ContainsKey(lastSpokenNumber) && cache[lastSpokenNumber].Count == 2)
                {
                    currentNumber = spokenNumbers.Count - (cache[lastSpokenNumber][0] + 1);
                    spokenNumbers.Add(currentNumber);
                    if (!cache.ContainsKey(currentNumber))
                    {
                        cache[currentNumber] = new List<int> { i };
                        continue;
                    }

                    if (cache[currentNumber].Count == 2)
                    {
                        cache[currentNumber][0] = cache[currentNumber][1];
                        cache[currentNumber][1] = i;
                        continue;
                    }
                    cache[currentNumber].Add(i);
                    continue;
                }

                if (!cache.ContainsKey(currentNumber))
                {
                    for (var j = spokenNumbers.Count-2; j >= 0; j--)
                    {
                        if (spokenNumbers[j] != lastSpokenNumber)
                        {
                            continue;
                        }
                        currentNumber = spokenNumbers.Count - (j + 1);
                        break;
                    }
                    cache[currentNumber] = new List<int> { i };
                    spokenNumbers.Add(currentNumber);
                    continue;
                }
                if (cache[currentNumber].Count == 2)
                {
                    cache[currentNumber].RemoveAt(0);
                }
                cache[currentNumber].Add(i);
                spokenNumbers.Add(currentNumber);
            }

            return currentNumber;
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using Common;

namespace Day18
{
    internal static class Program
    {
        private static void Main()
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

        private static long Part1(ImmutableList<string> input)
        {
            long sum = 0;
            foreach (var line in input)
            {
                sum += CalculatePart1(line);
            }

            return sum;
        }

        private static long Part2(ImmutableList<string> input)
        {
            long sum = 0;
            foreach (var line in input)
            {
                sum += CalculatePart2(line);
            }

            return sum;
        }

        internal static long CalculatePart2(string problem)
        {
            var tokens = BuildTokens(problem);
            var stack = new Stack<string>();

            foreach (var token in tokens)
            {
                if (token == ")")
                {
                    var parenthesisContent = "";
                    while (stack.Peek() != "(")
                    {
                        parenthesisContent = $"{stack.Pop()} {parenthesisContent}";
                    }

                    // Remove ( from stack
                    stack.Pop();
                    stack.Push(CalculatePart2(parenthesisContent).ToString());
                }
                else
                {
                    stack.Push(token);
                }
            }

            var list = stack.ToList();
            list.Reverse();

            var newList = new List<string>();
            newList.Add(list[0]);
            for (var i = 1; i < list.Count; i += 2)
            {
                if (list[i] == "*")
                {
                    newList.Add(list[i]);
                    newList.Add(list[i+1]);
                }
                else
                {
                    var lastNewList = newList.Last();
                    newList.RemoveAt(newList.Count-1);
                    var sum = long.Parse(lastNewList) + long.Parse(list[i + 1]);
                    newList.Add(sum.ToString());
                }
            }

            var total = long.Parse(newList.First());
            for (var i = 1; i < newList.Count; i += 2)
            {
                total *= long.Parse(newList[i+1]);
            }
            
            return total;
        }

        internal static long CalculatePart1(string problem)
        {
            var tokens = BuildTokens(problem);
            var stack = new Stack<string>();

            foreach (var token in tokens)
            {
                if (token == ")")
                {
                    var parenthesisContent = "";
                    while (stack.Peek() != "(")
                    {
                        parenthesisContent = $"{stack.Pop()} {parenthesisContent}";
                    }

                    // Remove ( from stack
                    stack.Pop();
                    stack.Push(CalculatePart1(parenthesisContent).ToString());
                }
                else
                {
                    stack.Push(token);
                }
            }

            var list = stack.ToList();
            list.Reverse();

            var total = long.Parse(list.First());
            for (var i = 1; i < list.Count; i += 2)
            {
                if (list[i] == "*")
                {
                    total *= long.Parse(list[i+1]);
                }
                else
                {
                    total += long.Parse(list[i+1]);
                }
            }
            
            return total;
        }

        private static IEnumerable<string> BuildTokens(string problem)
        {
            var splitProblem = problem.Split(" ");
            var result = new List<string>();
            foreach (var potentialToken in splitProblem)
            {
                if (potentialToken.Length == 1)
                {
                    result.Add(potentialToken);
                }
                else
                {
                    var number = "";
                    foreach (var character in potentialToken)
                    {
                        if (character == '(' || character == ')')
                        {
                            if (number.Length > 0)
                            {
                                result.Add(number);
                                number = "";
                            }
                            result.Add(character.ToString());
                        }
                        else
                        {
                            number += character;
                        }
                    }

                    if (number.Length > 0)
                    {
                        result.Add(number);
                    }
                }
            }

            return result;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Common;

namespace Day8
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var input = InputReader.ReadInput("input.txt", line => 
                CodeLine.CreateCodeLine(
                line.Split(" ").First(),
                int.Parse(line.Split(" ").Last())
                )
            );

            var timer = new Stopwatch();
            
            timer.Start();
            var accumulator = RunCode(input).Item1;
            timer.Stop();
            
            Console.WriteLine($"Part1: {accumulator}");
            Console.WriteLine($"Time: {timer.Elapsed}");

            timer.Restart();
            var potentiallyCorruptLines = input
                .Where(line => line.GetType() != typeof(AccumulatorCodeLine))
                .ToList();
            var infiniteLoop = true;

            while (infiniteLoop)
            {
                var editedInput = input.ToList();
                var testLine = potentiallyCorruptLines.FirstOrDefault();
                if (testLine == null)
                {
                    Console.WriteLine("Failed to find corrupt line");
                    break;
                }
                editedInput[editedInput.IndexOf(testLine)] = CodeLine.CreateCodeLine(
                        testLine.GetType() == typeof(JumpCodeLine) ? "nop" : "jmp",
                        testLine.Argument);
                potentiallyCorruptLines.Remove(testLine);
                (accumulator, infiniteLoop) = RunCode(editedInput);
            }
            timer.Stop();
            
            Console.WriteLine($"Part2: {accumulator}");
            Console.WriteLine($"Time: {timer.Elapsed}");
        }

        private static (int, bool) RunCode(IReadOnlyList<CodeLine> input)
        {
            foreach (var codeLine in input)
            {
                codeLine.Visited = false;
            }
            var accumulator = 0;
            var currentLine = input[0];
            var currentLineNumber = 0;
            var infiniteLoop = true;
            while (!currentLine.Visited)
            {
                currentLineNumber = currentLine.ExecuteCode(currentLineNumber, ref accumulator);
                if (currentLineNumber >= input.Count)
                {
                    infiniteLoop = false;
                    break;
                }
                currentLine = input[currentLineNumber];
            }

            return (accumulator, infiniteLoop);
        }

        private abstract class CodeLine
        {
            public int Argument { get; protected set; }
            public bool Visited { get; set; }

            public static CodeLine CreateCodeLine(string operation, int argument)
            {
                return operation switch
                {
                    "acc" => new AccumulatorCodeLine(argument),
                    "nop" => new NoOperationCodeLine(),
                    _ => new JumpCodeLine(argument)
                };
            }

            public abstract int ExecuteCode(int currentLine, ref int accumulator);
        }
        
        private class AccumulatorCodeLine : CodeLine
        {
            public AccumulatorCodeLine(int argument)
            {
                Argument = argument;
            }

            public override int ExecuteCode(int currentLine, ref int accumulator)
            {
                Visited = true;
                accumulator += Argument;
                return currentLine + 1;
            }
        }
        
        private class NoOperationCodeLine : CodeLine
        {
            public override int ExecuteCode(int currentLine, ref int accumulator)
            {
                Visited = true;
                return currentLine + 1;
            }
        }
        
        private class JumpCodeLine : CodeLine
        {
            public JumpCodeLine(int argument)
            {
                Argument = argument;
            }

            public override int ExecuteCode(int currentLine, ref int accumulator)
            {
                Visited = true;
                return currentLine + Argument;
            }
        }
    }
}
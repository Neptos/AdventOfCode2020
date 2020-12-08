using System;
using System.Collections.Generic;
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

            var accumulator = RunCode(input);

            Console.WriteLine($"Part1: {accumulator}");

            var allJumpLines = input.Where(line => line.GetType() == typeof(JumpCodeLine)).ToList();
            var allNoOperationLines = input.Where(line => line.GetType() == typeof(NoOperationCodeLine)).ToList();
            var foundCorrect = false;

            while (!foundCorrect)
            {
                try
                {
                    var editedInput = input.ToList();
                    editedInput[editedInput.IndexOf(allJumpLines.First())] = CodeLine.CreateCodeLine("nop", 0);
                    allJumpLines.Remove(allJumpLines.First());
                    accumulator = RunCodePart2(editedInput);
                    foundCorrect = true;
                }
                catch (InvalidOperationException)
                {
                    // Tried all jump operations
                    try
                    {
                        var editedInput = input.ToList();
                        editedInput[editedInput.IndexOf(allNoOperationLines.First())] = CodeLine.CreateCodeLine("jmp", allNoOperationLines.First().Argument);
                        allNoOperationLines.Remove(allNoOperationLines.First());
                        accumulator = RunCodePart2(editedInput);
                        foundCorrect = true;
                    }
                    catch (InvalidOperationException)
                    {
                        // Tried all nop operations
                        break;
                    }
                    catch (Exception)
                    {
                        // try again
                    }
                }
                catch (Exception)
                {
                    // try again
                }
            }
            
            Console.WriteLine($"Success? {foundCorrect}");
            Console.WriteLine($"Part2: {accumulator}");
        }

        private static int RunCodePart2(IReadOnlyList<CodeLine> input)
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

            if (infiniteLoop)
            {
                throw new Exception("Still looping");
            }

            return accumulator;
        }

        private static int RunCode(IReadOnlyList<CodeLine> input)
        {
            var accumulator = 0;
            var currentLine = input[0];
            var currentLineNumber = 0;
            while (!currentLine.Visited)
            {
                currentLineNumber = currentLine.ExecuteCode(currentLineNumber, ref accumulator);
                currentLine = input[currentLineNumber];
            }

            return accumulator;
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
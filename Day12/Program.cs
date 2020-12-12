using System;
using System.Diagnostics;
using System.Linq;
using Common;

namespace Day12
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var instructions = InputReader.ReadInput(
                "input.txt",
                s => new Instruction(s.First(), int.Parse(s.Substring(1))));

            var timer = new Stopwatch();
            
            timer.Start();
            var ship = new Ship();
            foreach (var instruction in instructions)
            {
                ship.Execute(instruction);
            }
            timer.Stop();
            
            Console.WriteLine($"Part1: {ship.ManhattanDistance}");
            Console.WriteLine($"Timer: {timer.Elapsed}");

            timer.Restart();
            var shipPart2 = new ShipPart2();
            var waypoint = new Waypoint(shipPart2);
            foreach (var instruction in instructions)
            {
                ExecutePart2Instruction(instruction, waypoint, shipPart2);
            }

            timer.Stop();
            
            Console.WriteLine($"Part2: {shipPart2.ManhattanDistance}");
            Console.WriteLine($"Timer: {timer.Elapsed}");
        }

        private static void ExecutePart2Instruction(Instruction instruction, Waypoint waypoint, ShipPart2 shipPart2)
        {
            switch (instruction.Operation)
            {
                case 'N':
                    waypoint.NorthPosition += instruction.Amount;
                    break;
                case 'E':
                    waypoint.EastPosition += instruction.Amount;
                    break;
                case 'S':
                    waypoint.NorthPosition -= instruction.Amount;
                    break;
                case 'W':
                    waypoint.EastPosition -= instruction.Amount;
                    break;
                case 'L':
                case 'R':
                    waypoint.Rotate(instruction.Operation, instruction.Amount);
                    break;
                case 'F':
                    shipPart2.EastPosition += waypoint.EastPosition * instruction.Amount;
                    shipPart2.NorthPosition += waypoint.NorthPosition * instruction.Amount;
                    break;
            }
        }
    }
}
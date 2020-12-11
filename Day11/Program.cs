using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Common;

namespace Day11
{
    internal class Program
    {
        public const char Occupied = '#';
        public const char Free = 'L';
        public const char Floor = '.';
        
        public static void Main(string[] args)
        {
            var rows = InputReader.ReadInput("input.txt", s => s).ToList();
            var timer = new Stopwatch();
            
            timer.Start();
            var occupiedSeats = Part1(rows);
            timer.Stop();
            
            Console.WriteLine($"Part1: {occupiedSeats}");
            Console.WriteLine($"Timer: {timer.Elapsed}");
            
            rows = InputReader.ReadInput("input.txt", s => s).ToList();

            timer.Restart();
            occupiedSeats = Part2(rows);
            timer.Stop();
            
            Console.WriteLine($"Part2: {occupiedSeats}");
            Console.WriteLine($"Timer: {timer.Elapsed}");
        }

        private static int Part2(List<string> rows)
        {
            var rowsCopy = rows.ToList();
            var occupiedSeats = -1;
            var lastRoundOccupiedSeats = -2;
            while (occupiedSeats != lastRoundOccupiedSeats)
            {
                lastRoundOccupiedSeats = occupiedSeats;
                for (var y = 0; y < rows.Count; y++)
                {
                    for (var x = 0; x < rows[y].Length; x++)
                    {
                        OccupyOrFreePart2(rows, rowsCopy, x, y);
                    }
                }

                occupiedSeats = CountOccupiedSeats(rows);
                rowsCopy = rows.ToList();
            }

            return occupiedSeats;
        }

        private static void OccupyOrFreePart2(List<string> operatingRows,
            List<string> checkingRows,
            int x,
            int y)
        {
            switch (checkingRows[y][x])
            {
                case Floor:
                    break;
                case Free when CountVisibleOccupiedSeats(checkingRows, x, y) == 0:
                {
                    var row = operatingRows[y].ToCharArray();
                    row[x] = Occupied;
                    operatingRows[y] = string.Join("", row);
                    break;
                }
                case Occupied when CountVisibleOccupiedSeats(checkingRows, x, y) >= 5:
                {
                    var row = operatingRows[y].ToCharArray();
                    row[x] = Free;
                    operatingRows[y] = string.Join("", row);
                    break;
                }
            }
        }

        private static int Traverse(List<string> checkingRows, int x, int y, int dx, int dy)
        {
            var currentX = x;
            var currentY = y;
            while (currentX + dx >= 0
                   && currentX + dx < checkingRows[0].Length
                   && currentY + dy >= 0
                   && currentY + dy < checkingRows.Count)
            {
                currentX += dx;
                currentY += dy;

                switch (checkingRows[currentY][currentX])
                {
                    case Occupied:
                        return 1;
                    case Free:
                        return 0;
                }
            }

            return 0;
        }

        private static int CountVisibleOccupiedSeats(List<string> checkingRows, int x, int y)
        {
            var count = 0;
            if (x == 0)
            {
                if (y == 0) // 1
                {
                    count += Traverse(checkingRows, x, y, 1, 0);
                    count += Traverse(checkingRows, x, y, 1, 1);
                    count += Traverse(checkingRows, x, y, 0, 1);
                }
                else if (y == checkingRows.Count - 1) // 6
                {
                    count += Traverse(checkingRows, x, y, 0, -1);
                    count += Traverse(checkingRows, x, y, 1, -1);
                    count += Traverse(checkingRows, x, y, 1, 0);
                }
                else // 4
                {
                    count += Traverse(checkingRows, x, y, 1, -1);
                    count += Traverse(checkingRows, x, y, 1, 0);
                    count += Traverse(checkingRows, x, y, 1, 1);
                    count += Traverse(checkingRows, x, y, 0, 1);
                    count += Traverse(checkingRows, x, y, 0, -1);
                }
            }
            else if (x == checkingRows[0].Length - 1)
            {
                if (y == 0) // 3
                {
                    count += Traverse(checkingRows, x, y, 0, 1);
                    count += Traverse(checkingRows, x, y, -1, 1);
                    count += Traverse(checkingRows, x, y, -1, 0);
                }
                else if (y == checkingRows.Count - 1) // 8
                {
                    count += Traverse(checkingRows, x, y, -1, 0);
                    count += Traverse(checkingRows, x, y, -1, -1);
                    count += Traverse(checkingRows, x, y, 0, -1);
                }
                else // 5
                {
                    count += Traverse(checkingRows, x, y, -1, -1);
                    count += Traverse(checkingRows, x, y, -1, 0);
                    count += Traverse(checkingRows, x, y, -1, 1);
                    count += Traverse(checkingRows, x, y, 0, 1);
                    count += Traverse(checkingRows, x, y, 0, -1);
                }
            }
            else if (y == 0) // 2
            {
                count += Traverse(checkingRows, x, y, 1, 1);
                count += Traverse(checkingRows, x, y, 0, 1);
                count += Traverse(checkingRows, x, y, -1, 1);
                count += Traverse(checkingRows, x, y, 1, 0);
                count += Traverse(checkingRows, x, y, -1, 0);
            }
            else if (y == checkingRows.Count - 1) // 7
            {
                count += Traverse(checkingRows, x, y, 1, -1);
                count += Traverse(checkingRows, x, y, 0, -1);
                count += Traverse(checkingRows, x, y, -1, -1);
                count += Traverse(checkingRows, x, y, 1, 0);
                count += Traverse(checkingRows, x, y, -1, 0);
            }
            else // 9
            {
                count += Traverse(checkingRows, x, y, 1, 1);
                count += Traverse(checkingRows, x, y, 1, 0);
                count += Traverse(checkingRows, x, y, 1, -1);
                count += Traverse(checkingRows, x, y, 0, 1);
                count += Traverse(checkingRows, x, y, 0, -1);
                count += Traverse(checkingRows, x, y, -1, 1);
                count += Traverse(checkingRows, x, y, -1, 0);
                count += Traverse(checkingRows, x, y, -1, -1);
            }

            return count;
        }

        private static int Part1(List<string> rows)
        {
            var rowsCopy = rows.ToList();
            var occupiedSeats = -1;
            var lastRoundOccupiedSeats = -2;
            while (occupiedSeats != lastRoundOccupiedSeats)
            {
                lastRoundOccupiedSeats = occupiedSeats;
                for (var y = 0; y < rows.Count; y++)
                {
                    for (var x = 0; x < rows[y].Length; x++)
                    {
                        OccupyOrFreePart1(rows, rowsCopy, x, y);
                    }
                }

                occupiedSeats = CountOccupiedSeats(rows);
                rowsCopy = rows.ToList();
            }

            return occupiedSeats;
        }

        private static int CountOccupiedSeats(List<string> rows)
        {
            return rows.SelectMany(row => row).Count(character => character == Occupied);
        }

        public static void OccupyOrFreePart1(
            List<string> operatingRows,
            List<string> checkingRows,
            int x,
            int y)
        {
            switch (checkingRows[y][x])
            {
                case Floor:
                    break;
                case Free when CountAdjacentOccupiedSeats(checkingRows, x, y) == 0:
                {
                    var row = operatingRows[y].ToCharArray();
                    row[x] = Occupied;
                    operatingRows[y] = string.Join("", row);
                    break;
                }
                case Occupied when CountAdjacentOccupiedSeats(checkingRows, x, y) >= 4:
                {
                    var row = operatingRows[y].ToCharArray();
                    row[x] = Free;
                    operatingRows[y] = string.Join("", row);
                    break;
                }
            }
        }

        private static int CountAdjacentOccupiedSeats(List<string> checkingRows, int x, int y)
        {
            var count = 0;
            if (x == 0)
            {
                if (y == 0) // 1
                {
                    count += checkingRows[0][1] == Occupied ? 1 : 0;
                    count += checkingRows[1][0] == Occupied ? 1 : 0;
                    count += checkingRows[1][1] == Occupied ? 1 : 0;
                }
                else if (y == checkingRows.Count - 1) // 6
                {
                    count += checkingRows[y][1] == Occupied ? 1 : 0;
                    count += checkingRows[y-1][0] == Occupied ? 1 : 0;
                    count += checkingRows[y-1][1] == Occupied ? 1 : 0;
                }
                else // 4
                {
                    count += checkingRows[y][1] == Occupied ? 1 : 0;
                    count += checkingRows[y+1][0] == Occupied ? 1 : 0;
                    count += checkingRows[y-1][0] == Occupied ? 1 : 0;
                    count += checkingRows[y+1][1] == Occupied ? 1 : 0;
                    count += checkingRows[y-1][1] == Occupied ? 1 : 0;
                }
            }
            else if (x == checkingRows[0].Length - 1)
            {
                if (y == 0) // 3
                {
                    count += checkingRows[0][x-1] == Occupied ? 1 : 0;
                    count += checkingRows[1][x] == Occupied ? 1 : 0;
                    count += checkingRows[1][x-1] == Occupied ? 1 : 0;
                }
                else if (y == checkingRows.Count - 1) // 8
                {
                    count += checkingRows[y][x-1] == Occupied ? 1 : 0;
                    count += checkingRows[y-1][x] == Occupied ? 1 : 0;
                    count += checkingRows[y-1][x-1] == Occupied ? 1 : 0;
                }
                else // 5
                {
                    count += checkingRows[y][x-1] == Occupied ? 1 : 0;
                    count += checkingRows[y+1][x] == Occupied ? 1 : 0;
                    count += checkingRows[y-1][x] == Occupied ? 1 : 0;
                    count += checkingRows[y+1][x-1] == Occupied ? 1 : 0;
                    count += checkingRows[y-1][x-1] == Occupied ? 1 : 0;
                }
            }
            else if (y == 0) // 2
            {
                count += checkingRows[0][x-1] == Occupied ? 1 : 0;
                count += checkingRows[1][x] == Occupied ? 1 : 0;
                count += checkingRows[0][x+1] == Occupied ? 1 : 0;
                count += checkingRows[1][x-1] == Occupied ? 1 : 0;
                count += checkingRows[1][x+1] == Occupied ? 1 : 0;
            }
            else if (y == checkingRows.Count - 1) // 7
            {
                count += checkingRows[y][x-1] == Occupied ? 1 : 0;
                count += checkingRows[y-1][x] == Occupied ? 1 : 0;
                count += checkingRows[y][x+1] == Occupied ? 1 : 0;
                count += checkingRows[y-1][x-1] == Occupied ? 1 : 0;
                count += checkingRows[y-1][x+1] == Occupied ? 1 : 0;
            }
            else // 9
            {
                count += checkingRows[y][x-1] == Occupied ? 1 : 0;
                count += checkingRows[y-1][x] == Occupied ? 1 : 0;
                count += checkingRows[y][x+1] == Occupied ? 1 : 0;
                count += checkingRows[y-1][x-1] == Occupied ? 1 : 0;
                count += checkingRows[y-1][x+1] == Occupied ? 1 : 0;
                count += checkingRows[y+1][x-1] == Occupied ? 1 : 0;
                count += checkingRows[y+1][x] == Occupied ? 1 : 0;
                count += checkingRows[y+1][x+1] == Occupied ? 1 : 0;
            }

            return count;
        }
    }
}
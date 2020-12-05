using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Common;

namespace Day5
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var inputs = InputReader.ReadInput("input.txt", s => s);
            var seats = inputs.Select(GetSeatInfo).ToList();

            var timer = new Stopwatch();
            
            timer.Start();
            var maxSeatId = seats.Max(s => s.SeatId);
            timer.Stop();
            
            Console.WriteLine($"Max seatId: {maxSeatId}");
            Console.WriteLine($"Time: {timer.Elapsed}");

            timer.Restart();
            var minSeatId = seats.Min(s => s.SeatId);
            var mySeatId = FindMySeat(seats, minSeatId, maxSeatId);
            timer.Stop();
            
            Console.WriteLine($"My seatId: {mySeatId}");
            Console.WriteLine($"Time: {timer.Elapsed}");
        }

        private static int FindMySeat(List<SeatInfo> seats, int minSeatId, int maxSeatId)
        {
            for (var currentSeatId = minSeatId; currentSeatId < maxSeatId; currentSeatId++)
            {
                if (seats.Exists(s => s.SeatId == currentSeatId))
                {
                    continue;
                }
                if (seats.Exists(s => s.SeatId == currentSeatId - 1)
                    && seats.Exists(s => s.SeatId == currentSeatId + 1))
                {
                    return currentSeatId;
                }
            }

            return 0;
        }

        public static SeatInfo GetSeatInfo(string boardingPass)
        {
            var rowInfo = boardingPass.Take(7);
            var columnInfo = boardingPass.Skip(7);
            return new SeatInfo(GetRow(rowInfo), GetColumn(columnInfo));
        }

        private static int GetColumn(IEnumerable<char> columnInfo)
        {
            return BinarySearchRange(columnInfo, 7, 'L');
        }

        private static int GetRow(IEnumerable<char> rowInfo)
        {
            return BinarySearchRange(rowInfo, 127, 'F');
        }

        private static int BinarySearchRange(IEnumerable<char> charsInfo, int upperLimit, char lowerChar)
        {
            var lower = 0;
            var upper = upperLimit;
            foreach (var charInfo in charsInfo)
            {
                if (charInfo == lowerChar)
                {
                    upper = (int)Math.Floor((lower + upper) / 2.0);
                }
                else
                {
                    lower = (int)(Math.Floor((lower + upper) / 2.0)+1);
                }
            }

            return lower;
        }
    }
}
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
            var mySeatId = FindMySeat(seats);
            timer.Stop();
            
            Console.WriteLine($"My seatId: {mySeatId}");
            Console.WriteLine($"Time: {timer.Elapsed}");
        }

        private static int FindMySeat(List<SeatInfo> seats)
        {
            for (var row = 0; row < 128; row++)
            {
                for (var column = 0; column < 8; column++)
                {
                    if (seats.Exists(s => s.Row == row && s.Column == column))
                    {
                        continue;
                    }

                    var seatId = SeatInfo.GetSeatId(row, column);
                    if (seats.Exists(s => s.SeatId == seatId - 1)
                        && seats.Exists(s => s.SeatId == seatId + 1))
                    {
                        return seatId;
                    }
                }
            }

            return 0;
        }

        public static SeatInfo GetSeatInfo(string boardingPass)
        {
            var rowInfo = boardingPass.Take(7);
            var columnInfo = boardingPass.Skip(7);
            return new SeatInfo
            {
                Row = GetRow(rowInfo),
                Column = GetColumn(columnInfo)
            };
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
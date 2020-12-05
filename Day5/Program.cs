using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace Day5
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var inputs = InputReader.ReadInput("input.txt", s => s);

            var seats = new List<SeatInfo>();
            foreach (var input in inputs)
            {
                seats.Add(GetSeatInfo(input));
            }

            Console.WriteLine($"Max seatId: {seats.Max(s => s.SeatId)}");

            for (int row = 0; row < 128; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    if (seats.Exists(s => s.Row == row && s.Column == column))
                    {
                        continue;
                    }
                    var seatId = SeatInfo.GetSeatId(row, column);
                    if (seats.Exists(s => s.SeatId == seatId - 1)
                        && seats.Exists(s => s.SeatId == seatId + 1))
                    {
                        Console.WriteLine($"My seatId: {seatId}");
                    }
                }
            }
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
            var (lower, upper) = new ValueTuple<int, int>(0, upperLimit);
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

    internal class SeatInfo
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public int SeatId => GetSeatId(Row, Column);

        public static int GetSeatId(int row, int column)
        {
            return row * 8 + column;
        }
    }
}
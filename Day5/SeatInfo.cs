namespace Day5
{
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
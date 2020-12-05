namespace Day5
{
    internal class SeatInfo
    {
        public int Row { get; }
        public int Column { get; }
        public int SeatId { get; }

        internal SeatInfo(int row, int column)
        {
            SeatId = GetSeatId(row, column);
            Row = row;
            Column = column;
        }
        
        public static int GetSeatId(int row, int column)
        {
            return row * 8 + column;
        }
    }
}
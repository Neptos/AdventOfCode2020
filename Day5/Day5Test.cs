using Xunit;

namespace Day5
{
    public class Day5Test
    {
        [Theory]
        [InlineData("FBFBBFFRLR", 44, 5, 357)]
        [InlineData("BFFFBBFRRR", 70, 7, 567)]
        [InlineData("FFFBBBFRRR", 14, 7, 119)]
        [InlineData("BBFFBBFRLL", 102, 4, 820)]
        public void GetSeatTest(string boardingPass, int expectedRow, int expectedColumn, int expectedSeatId)
        {
            var result = Program.GetSeatInfo(boardingPass);
            Assert.Equal(result.Row, expectedRow);
            Assert.Equal(result.Column, expectedColumn);
            Assert.Equal(result.SeatId, expectedSeatId);
        }
    }
}
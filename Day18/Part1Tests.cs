using Xunit;

namespace Day18
{
    public class Part1Tests
    {
        [Theory]
        [InlineData("2 * 3 + (4 * 5)", 26)]
        [InlineData("1 + (2 * 3) + (4 * (5 + 6))", 51)]
        [InlineData("1 + 2 * 3 + 4 * 5 + 6", 71)]
        [InlineData("5 + (8 * 3 + 9 + 3 * 4 * 3)", 437)]
        [InlineData("5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))", 12240)]
        [InlineData("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2", 13632)]
        public void TestCalculate(string problem, int expectedSolution)
        {
            var result = Program.CalculatePart1(problem);
            Assert.Equal(expectedSolution, result);
        }
    }
}
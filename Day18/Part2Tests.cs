using Xunit;

namespace Day18
{
    public class Part2Tests
    {
        [Theory]
        [InlineData("2 * 3 + (4 * 5)", 46)]
        [InlineData("1 + (2 * 3) + (4 * (5 + 6))", 51)]
        [InlineData("1 + 2 * 3 + 4 * 5 + 6", 231)]
        [InlineData("5 + (8 * 3 + 9 + 3 * 4 * 3)", 1445)]
        [InlineData("5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))", 669060)]
        [InlineData("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2", 23340)]
        public void TestCalculate(string problem, int expectedSolution)
        {
            var result = Program.CalculatePart2(problem);
            Assert.Equal(expectedSolution, result);
        }
    }
}
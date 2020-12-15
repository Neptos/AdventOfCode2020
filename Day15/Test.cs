using System.Collections.Generic;
using Xunit;

namespace Day15
{
    public class Test
    {
        [Theory]
        [MemberData(nameof(Part1Data))]
        public void TestPart1(List<int> startingNumber, int thNumber, int expectedResult)
        {
            var result = Program.GetNumberSpokenFor(startingNumber, thNumber);
            Assert.Equal(expectedResult, result);
        }
        
        [Theory]
        [MemberData(nameof(Part2Data))]
        public void TestPart2(List<int> startingNumber, int thNumber, int expectedResult)
        {
            var result = Program.GetNumberSpokenFor(startingNumber, thNumber);
            Assert.Equal(expectedResult, result);
        }

        public static IEnumerable<object[]> Part1Data =>
            new List<object[]>
            {
                new object[] {new List<int> {0,3,6}, 2020, 436},
                new object[] {new List<int> {1,3,2}, 2020, 1},
                new object[] {new List<int> {2,1,3}, 2020, 10},
                new object[] {new List<int> {1,2,3}, 2020, 27},
                new object[] {new List<int> {2,3,1}, 2020, 78},
                new object[] {new List<int> {3,2,1}, 2020, 438},
                new object[] {new List<int> {3,1,2}, 2020, 1836}
            };
        
        public static IEnumerable<object[]> Part2Data =>
            new List<object[]>
            {
                new object[] {new List<int> {0,3,6}, 30000000, 175594},
                new object[] {new List<int> {1,3,2}, 30000000, 2578},
                new object[] {new List<int> {2,1,3}, 30000000, 3544142},
                new object[] {new List<int> {1,2,3}, 30000000, 261214},
                new object[] {new List<int> {2,3,1}, 30000000, 6895259},
                new object[] {new List<int> {3,2,1}, 30000000, 18},
                new object[] {new List<int> {3,1,2}, 30000000, 362}
            };
    }
}
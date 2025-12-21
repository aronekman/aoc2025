namespace AdventOfCode.Tests;

public static class ExampleCases
{
    public static TheoryData<int, string> Part1 => new()
    {
        { 1, "3" },
        { 2, "1227775554" },
        { 3, "357" },
        { 4, "13" },
        { 5, "3" }
    };


    public static TheoryData<int, string> Part2 => new()
    {
        { 1, "6" },
        { 2, "4174379265" },
        { 3, "3121910778619" },
        { 4, "43" },
        { 5, "14" }
    };


    public class Tests
    {
        [Theory]
        [MemberData(nameof(Part1), MemberType = typeof(ExampleCases))]
        public void Part1_Examples(int day, string expected)
        {
            var input = Helpers.LoadExampleInput(day);
            var solver = Helpers.CreateDay(day);

            var result = solver.Part1(input);

            Assert.Equal(expected, result);
        }

        [Theory]
        [MemberData(nameof(Part2), MemberType = typeof(ExampleCases))]
        public void Part2_Examples(int day, string expected)
        {
            var input = Helpers.LoadExampleInput(day);
            var solver = Helpers.CreateDay(day);

            var result = solver.Part2(input);

            Assert.Equal(expected, result);
        }
    }
}

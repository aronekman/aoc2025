namespace AdventOfCode.Tests;

public static class ExampleCases
{
    public static TheoryData<int, string> Part1 => new()
    {
        { 1, "3" },
    };

    public static TheoryData<int, string> Part2 => new()
    {
        { 1, "6" },
    };
}

public class Tests
{
    [Theory]
    [MemberData(nameof(ExampleCases.Part1), MemberType = typeof(ExampleCases))]
    public void Part1_Examples(int day, string expected)
    {
        var input = Helpers.LoadExampleInput(day);
        var solver = Helpers.CreateDay(day);

        var result = solver.Part1(input);

        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(ExampleCases.Part2), MemberType = typeof(ExampleCases))]
    public void Part2_Examples(int day, string expected)
    {
        var input = Helpers.LoadExampleInput(day);
        var solver = Helpers.CreateDay(day);

        var result = solver.Part2(input);

        Assert.Equal(expected, result);
    }
}

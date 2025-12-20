namespace AdventOfCode.Extensions;

public static class InputExtensions
{
    public static string[] SplitAtNewLines(this string input)
    {
        return input.Trim().Split(Environment.NewLine);
    }
}

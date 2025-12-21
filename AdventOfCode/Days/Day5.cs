using AdventOfCode.Extensions;

namespace AdventOfCode.Days;

public class Day5 : IDay
{
    public int DayNumber => 5;

    public string Part1(string input)
    {
        var ranges = input.SplitAtNewLines().TakeWhile(x => !string.IsNullOrWhiteSpace(x)).Select(row =>
        {
            var parts = row.Split("-");
            return (low: long.Parse(parts[0]), high: long.Parse(parts[1]));
        });
        var ingredients = input.SplitAtNewLines().SkipWhile(x => !string.IsNullOrWhiteSpace(x)).Skip(1)
            .Select(long.Parse);
        return ingredients.Count(x => ranges.Any(range => x >= range.low && x <= range.high)).ToString();
    }

    public string Part2(string input)
    {
        var ranges = input.SplitAtNewLines().TakeWhile(x => !string.IsNullOrWhiteSpace(x)).Select(row =>
        {
            var parts = row.Split("-");
            return (low: long.Parse(parts[0]), high: long.Parse(parts[1]));
        }).OrderBy(r => r.low).ToList();

        long result = 0;
        var currentLow = ranges[0].low;
        var currentHigh = ranges[0].high;

        foreach (var (low, high) in ranges.Skip(1))
        {
            if (low > currentHigh + 1)
            {
                result += currentHigh - currentLow + 1;
                currentLow = low;
                currentHigh = high;
                continue;
            }

            if (high > currentHigh) currentHigh = high;
        }

        result += currentHigh - currentLow + 1;
        return result.ToString();
    }
}

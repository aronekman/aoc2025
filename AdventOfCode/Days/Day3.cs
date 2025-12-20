
using AdventOfCode.Extensions;

namespace AdventOfCode.Days;

public class Day3 : IDay
{
    public int DayNumber => 3;

    public string Part1(string input)
    {
        var rows = input.SplitAtNewLines();

        var result = 0;
        foreach (var row in rows)
        {
            var currentBest = 0;
            for (var i = 0; i < row.Length - 1; i++)
            {
                for (var j = i + 1; j < row.Length; j++)
                {
                    currentBest = Math.Max(int.Parse($"{row[i]}{row[j]}"), currentBest);
                }
            }

            result += currentBest;
        }

        return result.ToString();
    }

    public string Part2(string input)
    {
        var rows = input.SplitAtNewLines();
        long result = 0;

        foreach (var row in rows)
        {
            var numbers = row.Select(x => int.Parse(x.ToString())).ToList();
            List<int> joltage = [];
            while (joltage.Count < 12)
            {
                var max = numbers[..(numbers.Count - 11 + joltage.Count)].Max();
                numbers = numbers[(numbers.FindIndex(x => x == max) + 1)..];
                joltage.Add(max);
            }

            result += long.Parse(string.Join("", joltage));
        }

        return result.ToString();
    }
}

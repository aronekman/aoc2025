using AdventOfCode.Extensions;

namespace AdventOfCode.Days;

public class Day6 : IDay
{
    public int DayNumber => 6;

    public string Part1(string input)
    {
        var grid = input.SplitAtNewLines().Select(row => row.Split(" ", StringSplitOptions.RemoveEmptyEntries))
            .ToList();
        var problems = Enumerable.Range(0, grid.First().Length)
            .Select(col =>
            {
                var column = grid.Select(row => row[col]).ToList();
                return (numbers: column.SkipLast(1).Select(long.Parse), operation: column.Last());
            })
            .ToList();
        return problems.Sum(p => p.numbers.Aggregate((a, b) => p.operation == "+" ? a + b : a * b)).ToString();
    }

    public string Part2(string input)
    {
        var grid = input.SplitAtNewLines();
        var x = 0;
        var w = grid.Max(x => x.Length);
        List<(IEnumerable<long>, char)> problems = [];
        while (x < w)
        {
            var operation = grid.Last()[x];
            List<string> numbers = [];
            do
            {
                numbers.Add(string.Join("",
                    grid.SkipLast(1).Select(row => x < row.Length ? row[x] : ' ').Where(c => c != ' ')));
                x += 1;
            } while ((x >= grid.Last().Length || grid.Last()[x] == ' ') && x < w);

            problems.Add((numbers.Where(s => !string.IsNullOrWhiteSpace(s)).Select(long.Parse), operation));
        }

        return problems.Sum(p => p.Item1.Aggregate((a, b) => p.Item2 == '+' ? a + b : a * b)).ToString();
    }
}

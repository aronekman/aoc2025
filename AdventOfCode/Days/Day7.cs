using AdventOfCode.Extensions;

namespace AdventOfCode.Days;

public class Day7 : IDay
{
    public int DayNumber => 7;

    public string Part1(string input)
    {
        var grid = input.SplitAtNewLines().Select(row => row.Select(x => x).ToList()).ToList();
        var start = grid.First().FindIndex(c => c == 'S');
        grid[1][start] = '|';
        var result = 0;
        foreach (var (row, y) in grid.Skip(2).Select((row, y) => (row, y + 2)))
        {
            foreach (var (cell, x) in row.Select((c, x) => (c, x)).ToList())
            {
                if (grid[y - 1][x] != '|') continue;
                if (cell == '^')
                {
                    row[x - 1] = '|';
                    row[x + 1] = '|';
                    result++;
                }
                else
                {
                    grid[y][x] = '|';
                }
            }
        }

        return result.ToString();
    }


    public string Part2(string input)
    {
        var grid = input.SplitAtNewLines().Skip(1).Select(row => row.Select(x => x).ToList())
            .Where(row => row.Any(c => c != '.')).ToList();
        var start = grid.First().FindIndex(c => c == '^');
        var leafsCountCache = new Dictionary<(int x, int y), long>();
        return CountLeafs(start, 0).ToString();


        long CountLeafs(int x, int y)
        {
            while (true)
            {
                if (grid.Count <= y)
                {
                    return 1;
                }

                if (leafsCountCache.TryGetValue((x, y), out var leafs)) return leafs;

                if (grid[y][x] == '^')
                {
                    var leafCount = CountLeafs(x - 1, y + 1) + CountLeafs(x + 1, y + 1);
                    leafsCountCache.Add((x, y), leafCount);
                    return leafCount;
                }

                y++;
            }
        }
    }
}

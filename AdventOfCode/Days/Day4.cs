using AdventOfCode.Extensions;

namespace AdventOfCode.Days;

public class Day4 : IDay
{
    public int DayNumber => 4;

    public string Part1(string input)
    {
        var grid = input.SplitAtNewLines().Select(row => row.Select(c => c == '@' ? Cell.Paper : Cell.Empty).ToList())
            .ToList();
        var height = grid.Count;
        var width = grid[0].Count;
        var result = 0;
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                if (grid[y][x] != Cell.Paper) continue;
                var neighbours = GetNeighbours(grid, y, x, height, width);
                if (neighbours.Count(n => n == Cell.Paper) < 4) result++;
            }
        }

        return result.ToString();
    }

    public string Part2(string input)
    {
        var grid = input.SplitAtNewLines().Select(row => row.Select(c => c == '@' ? Cell.Paper : Cell.Empty).ToList())
            .ToList();
        var height = grid.Count;
        var width = grid[0].Count;
        var result = 0;
        var shouldRetry = true;
        while (shouldRetry)
        {
            shouldRetry = false;
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    if (grid[y][x] != Cell.Paper) continue;
                    var neighbours = GetNeighbours(grid, y, x, height, width);
                    if (neighbours.Count(n => n == Cell.Paper) < 4)
                    {
                        result++;
                        grid[y][x] = Cell.Empty;
                        shouldRetry = true;
                    }
                }
            }
        }

        return result.ToString();
    }

    private enum Cell
    {
        Paper,
        Empty
    }

    private static List<Cell> GetNeighbours(List<List<Cell>> grid, int y, int x, int height, int width)
    {
        List<Cell> neighbours = [];
        if (x > 0) neighbours.Add(grid[y][x - 1]);
        if (x < width - 1) neighbours.Add(grid[y][x + 1]);
        if (y > 0) neighbours.Add(grid[y - 1][x]);
        if (y < height - 1) neighbours.Add(grid[y + 1][x]);
        if (x > 0 && y > 0) neighbours.Add(grid[y - 1][x - 1]);
        if (x < width - 1 && y > 0) neighbours.Add(grid[y - 1][x + 1]);
        if (x > 0 && y < height - 1) neighbours.Add(grid[y + 1][x - 1]);
        if (x < width - 1 && y < height - 1) neighbours.Add(grid[y + 1][x + 1]);
        return neighbours;
    }
}

using AdventOfCode.Extensions;

namespace AdventOfCode.Days;

public class Day1 : IDay
{
    public int DayNumber => 1;

    public string Part1(string input)
    {
        var lines = input.SplitAtNewLines();
        var commands = lines.Select(row => (row[..1], int.Parse(row[1..])));
        var dial = 50;
        var password = 0;
        foreach (var (dir, amount) in commands)
        {
            dial = dir == "R" ? dial + amount : dial - amount;
            if (dial < 0) dial += 100 * (-dial / 100 + 1);
            if (dial > 99) dial -= 100 * (dial / 100);
            if (dial == 0) password++;
        }

        return password.ToString();
    }

    public string Part2(string input)
    {
        var lines = input.SplitAtNewLines();
        var commands = lines.Select(row => (row[..1], int.Parse(row[1..])));
        var dial = 50;
        var password = 0;
        foreach (var (dir, amount) in commands)
        {
            var leftToMove = amount;
            var counter = dir == "R" ? 1 : -1;
            while (leftToMove > 0)
            {
                dial += counter;
                leftToMove--;
                if (dial > 99) dial = 0;
                if (dial < 0) dial = 99;
                if (dial == 0) password++;
            }
        }

        return password.ToString();
    }
}

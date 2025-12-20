namespace AdventOfCode;

public interface IDay
{
    int DayNumber { get; }
    string Part1(string input);
    string Part2(string input);
}

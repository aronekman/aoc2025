namespace AdventOfCode.Tests;

public static class Helpers
{
    public static string LoadExampleInput(int day)
    {
        var path = Path.Combine("ExampleInputs", $"day{day:00}.txt");

        return !File.Exists(path)
            ? throw new FileNotFoundException($"Example input file not found: {path}")
            : File.ReadAllText(path);
    }

    public static IDay CreateDay(int day)
    {
        var assembly = typeof(IDay).Assembly;

        var dayType = assembly
            .GetTypes()
            .Where(t => typeof(IDay).IsAssignableFrom(t) && t is { IsInterface: false, IsAbstract: false })
            .FirstOrDefault(t =>
            {
                var instance = (IDay)Activator.CreateInstance(t)!;
                return instance.DayNumber == day;
            }) ?? throw new InvalidOperationException($"Day {day:D2} not implemented.");
        return (IDay)Activator.CreateInstance(dayType)!;
    }
}

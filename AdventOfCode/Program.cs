using System.Diagnostics;
using System.Reflection;
using AdventOfCode;
using Microsoft.Extensions.Configuration;


var config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddCommandLine(args)
    .Build();

var sessionCookie = config["Session"] ?? throw new InvalidOperationException("Session cookie not set.");
var inputFetcher = new InputFetcher(sessionCookie);


var dayInput = config["day"];

if (dayInput == null)
{
    await RunAllDays();
    return;
}

if (!int.TryParse(dayInput, out int dayNumber)) throw new ArgumentException("Invalid day value.");
var dayImplementation = GetAllDays().FirstOrDefault(d => d.DayNumber == dayNumber);
if (dayImplementation is null)
{
    Console.WriteLine($"Day {dayImplementation:D2} not implemented.");
    return;
}

await RunSingleDay(dayImplementation);
return;

async Task RunSingleDay(IDay day)
{
    var input = await inputFetcher.GetInputAsync(day.DayNumber);
    Console.WriteLine($"\n=== Day {day.DayNumber:00} ===");
    var (part1, t1) = Measure(day.Part1, input);
    Console.WriteLine($"Part 1: {part1} ({GetExecutiontime(t1)})");
    var (part2, t2) = Measure(day.Part2, input);
    Console.WriteLine($"Part 2: {part2} ({GetExecutiontime(t2)})");
}


async Task RunAllDays()
{
    var startTime = Stopwatch.GetTimestamp();
    foreach (var puzzleDay in GetAllDays().OrderBy(d => d.DayNumber))
    {
        await RunSingleDay(puzzleDay);
    }

    var delta = Stopwatch.GetElapsedTime(startTime);
    Console.WriteLine($"\nTotal Time: {GetExecutiontime(delta)}");
}

IEnumerable<IDay> GetAllDays()
{
    return Assembly.GetExecutingAssembly()
        .GetTypes()
        .Where(t => typeof(IDay).IsAssignableFrom(t) && t is { IsInterface: false, IsAbstract: false })
        .Select(t => (IDay)Activator.CreateInstance(t)!);
}

static (T Result, TimeSpan Elapsed) Measure<T>(Func<string, T> action, string input)
{
    var startTime = Stopwatch.GetTimestamp();
    var result = action(input);
    return (result, Stopwatch.GetElapsedTime(startTime));
}


string GetExecutiontime(TimeSpan delta)
{
    if (delta.TotalSeconds < 1) return $"{delta.TotalMilliseconds:F3}ms";
    if (delta.TotalMinutes < 1) return $"{delta.TotalSeconds:F3}s";
    return $"{delta.TotalMinutes:F3}m";
}


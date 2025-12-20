

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
    Console.WriteLine($"Part 1: {day.Part1(input)}");
    Console.WriteLine($"Part 2: {day.Part2(input)}");
}

async Task RunAllDays()
{
    foreach (var day in GetAllDays().OrderBy(d => d.DayNumber))
    {
        await RunSingleDay(day);
    }
}

IEnumerable<IDay> GetAllDays()
{
    return Assembly.GetExecutingAssembly()
        .GetTypes()
        .Where(t => typeof(IDay).IsAssignableFrom(t) && t is { IsInterface: false, IsAbstract: false })
        .Select(t => (IDay)Activator.CreateInstance(t)!);
}

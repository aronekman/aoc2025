using AdventOfCode.Extensions;

namespace AdventOfCode.Days;

public class Day8 : IDay
{
    public int DayNumber => 8;

    public string Part1(string input)
    {
        var junctionBoxes = input.SplitAtNewLines().Select(row =>
        {
            var split = row.Split(",");
            return new Box(int.Parse(split[0]), int.Parse(split[1]), int.Parse(split[2]));
        }).ToList();
        var pairs = junctionBoxes.SelectMany((box1, i) => junctionBoxes.Skip(i + 1).Select(box2 => (box1, box2,
            distance: Math.Sqrt(Math.Pow(box2.X - box1.X, 2) + Math.Pow(box2.Y - box1.Y, 2) +
                                Math.Pow(box2.Z - box1.Z, 2))))).OrderBy(x => x.distance);

        List<List<Box>> circuits = [];

        foreach (var (box1, box2, _) in pairs.Take(Helpers.IsTest ? 10 : 1000))
        {
            Connect(circuits, box1, box2);
        }

        var result = circuits.Select(c => c.Count).OrderDescending().Take(3);

        return result.Aggregate((a, b) => a * b).ToString();
    }

    public string Part2(string input)
    {
        var junctionBoxes = input.SplitAtNewLines().Select(row =>
        {
            var split = row.Split(",");
            return new Box(int.Parse(split[0]), int.Parse(split[1]), int.Parse(split[2]));
        }).ToList();
        var pairs = junctionBoxes.SelectMany((box1, i) => junctionBoxes.Skip(i + 1).Select(box2 => (box1, box2,
            distance: Math.Sqrt(Math.Pow(box2.X - box1.X, 2) + Math.Pow(box2.Y - box1.Y, 2) +
                                Math.Pow(box2.Z - box1.Z, 2))))).OrderBy(x => x.distance).ToList();

        List<List<Box>> circuits = [];
        foreach (var (box1, box2, _) in pairs)
        {
            Connect(circuits, box1, box2);
            if (circuits.Max(x => x.Count) >= junctionBoxes.Count) return ((long)box1.X * box2.X).ToString();
        }

        throw new InvalidOperationException("No fully connected circuit was found after processing all box pairs.");
    }

    private static void Connect(List<List<Box>> circuits, Box box1, Box box2)
    {
        var circuit1 = circuits.Find(c => c.Contains(box1));
        var circuit2 = circuits.Find(c => c.Contains(box2));

        if (circuit1 != null && circuit2 != null && ReferenceEquals(circuit1, circuit2)) return;
        if (circuit1 != null && circuit2 != null)
        {
            circuit1.AddRange(circuit2);
            circuits.Remove(circuit2);
        }
        else if (circuit1 != null) circuit1.Add(box2);
        else if (circuit2 != null) circuit2.Add(box1);
        else circuits.Add([box1, box2]);
    }

    private record Box(int X, int Y, int Z);
}

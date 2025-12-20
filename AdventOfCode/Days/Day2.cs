namespace AdventOfCode.Days;

public class Day2 : IDay
{
    public int DayNumber => 2;

    public string Part1(string input)
    {
        var ranges = input.Split(",").Select(x =>
        {
            var s = x.Trim().Split("-");
            return (long.Parse(s[0]), long.Parse(s[1]));
        });
        List<long> incorrectIds = [];
        foreach (var (start, end) in ranges)
        {
            var cur = start;
            while (cur <= end)
            {
                var text = cur.ToString();
                var first = text[..(text.Length / 2)];
                var second = text[(text.Length / 2)..];
                if (first == second) incorrectIds.Add(cur);
                cur++;
            }
        }

        return incorrectIds.Sum().ToString();
    }

    public string Part2(string input)
    {
        var ranges = input.Split(",").Select(x =>
        {
            var s = x.Trim().Split("-");
            return (long.Parse(s[0]), long.Parse(s[1]));
        });
        List<long> incorrectIds = [];
        foreach (var (start, end) in ranges)
        {
            var cur = start;
            while (cur <= end)
            {
                var text = cur.ToString();
                for (var chunkSize = 1; chunkSize <= text.Length / 2; chunkSize++)
                {
                    if (text.Length % chunkSize != 0) continue;
                    var size = chunkSize;
                    var parts = Enumerable.Range(0, text.Length / chunkSize)
                        .Select(i => text.Substring(i * size, size));
                    if (parts.Distinct().Count() != 1) continue;
                    incorrectIds.Add(cur);
                }

                cur++;
            }
        }

        return incorrectIds.Sum().ToString();
    }
}

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
                    var first = text[..chunkSize].AsSpan();
                    var incorrect = true;
                    for (var i = 0; i < text.Length; i += chunkSize)
                    {
                        if (text.AsSpan(i, chunkSize).SequenceEqual(first)) continue;
                        incorrect = false;
                        break;
                    }

                    if (!incorrect) continue;
                    incorrectIds.Add(cur);
                    break;
                }

                cur++;
            }
        }

        return incorrectIds.Sum().ToString();
    }
}

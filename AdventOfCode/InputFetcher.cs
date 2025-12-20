namespace AdventOfCode;

public class InputFetcher
{
    private readonly HttpClient _client;

    public InputFetcher(string sessionCookie)
    {
        _client = new HttpClient()
        {
            BaseAddress = new Uri("https://adventofcode.com")
        };
        _client.DefaultRequestHeaders.Add("Cookie", $"session={sessionCookie}");
    }

    public async Task<string> GetInputAsync(int day)
    {
        var response = await _client.GetAsync($"/2025/day/{day}/input");

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }
}

namespace AdventOfCode;

public class Helpers
{
    public static bool IsTest =>
        AppDomain.CurrentDomain.GetAssemblies().Any(a => a.GetName().Name == "AdventOfCode.Tests");
}

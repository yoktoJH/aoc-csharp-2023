namespace _2023_cs;

public static class Util
{
    public static List<string> ParseFile(string filename)
    {
        StreamReader input = new StreamReader(filename);
        string? line = input.ReadLine();
        List<string> lines = new List<string>();
        while (line != null)
        {
            lines.Add(line);
            line = input.ReadLine();
        }

        return lines;
    }

    public static void PrintResult(long part1, long part2,int day)
    {
        Console.WriteLine($"Day {day} solutions:");
        Console.WriteLine($"part1 {part1}");
        Console.WriteLine($"part2 {part2}");
    }
    
    
}
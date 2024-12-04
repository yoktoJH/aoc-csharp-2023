namespace _2023_cs;

public class Day9
{


    private static int[] convert(string line)
    {
        string[] splitLine = line.Split(' ');
        int[] startingLine = new int[splitLine.Length];
        for (int i = 0; i < splitLine.Length; i++)
        {
            startingLine[i] = Convert.ToInt32(splitLine[i]);
        }

        return startingLine;
    }
    private static List<int[]> extrapolate(int[] startingLine)
    {
        List<int[]> extrapolations = new List<int[]>();
        extrapolations.Add(startingLine);
        int size = startingLine.Length-1;
        bool nonzero = true;
        int[] previousLine = startingLine;
        while (size> 0 && nonzero)
        {
            int[] nextLine = new int[size];
            for (int i = 0; i < previousLine.Length-1; i++)
            {
                nextLine[i] = previousLine[i + 1] - previousLine[i];
            }
            extrapolations.Add(nextLine);
            previousLine = nextLine;
            size--;
        }

        return extrapolations;
    }

    private static int part1(List<int[]> extrapolations)
    {
        int result = 0;
        foreach (var line in extrapolations)
        {
            result += line[^1];
        }

        return result;
    }

    private static int part2(List<int[]> extrapolations)
    {
        int result = 0;
        extrapolations.Reverse();
        foreach (var line in extrapolations)
        {
            result = line[0] - result;
        }

        return result;
    }
    
    
    public static void solve(string filename)
    {
        StreamReader input = new StreamReader(filename);
        string? line = input.ReadLine();
        int sum1 = 0;
        int sum2 = 0;
        while (line != null)
        {
            List<int[]> extrapolations = extrapolate(convert(line));
            sum1 += part1(extrapolations);
            sum2 += part2(extrapolations);
            line = input.ReadLine();
        }
        
        Console.WriteLine("Day 9 part 1");
        Console.WriteLine(sum1);
        Console.WriteLine("Day 9 part 2");
        Console.WriteLine(sum2);
    }
}
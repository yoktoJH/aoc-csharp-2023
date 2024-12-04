using System.Diagnostics;

namespace _2023_cs;

public class Day6
{

    private  static List<int> to_ints(string[] numbers)
    {
        List<int> result = new List<int>();
        foreach (var num in numbers)
        {
            if (num.Length !=0 && char.IsNumber(num[0]))
            {
                result.Add(Convert.ToInt32(num));
            }
        }

        return result;
    }

    public static int part2_read(string line)
    {
        int result = 0;
        foreach (var c in line)
        {
            if (char.IsNumber(c))
            {
                result *= 10;
                result += c - '0';
            }
        }

        return result;
    }
    
    public static void solve(string filename)
    {
        StreamReader input = new StreamReader(filename);
        string? line = input.ReadLine();
        line = line.TrimEnd();
        long p2_time = 44899691;
        string[] strtimes = line.Split(' ');
        line = input.ReadLine();
        line = line.TrimEnd();
        long p2_dist = 277113618901768;
        string[] strdistances = line.Split(' ');
        List<int> times = to_ints(strtimes);
        List<int> distances = to_ints(strdistances);

        int sum = 1;
        for (int i = 0; i < times.Count; i++)
        {
            int beaten = 0;
            for (int timeHeld = 0; timeHeld <= times[i]; timeHeld++)
            {
                
                if (timeHeld*(times[i]-timeHeld)> distances[i])
                {
                    beaten++;
                }
            }

            sum *= beaten;
        }
        
        
        
        Console.WriteLine("Day 6");
        Console.WriteLine(sum);
        
        int beaten2 = 0;
        for (int timeHeld = 0; timeHeld <= p2_time; timeHeld++)
        {
                
            if (timeHeld*(p2_time-timeHeld)> p2_dist)
            {
                beaten2++;
            }
        }
        Console.WriteLine(beaten2);
    }   
}
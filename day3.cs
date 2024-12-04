using System.Collections;
using System.Diagnostics;
using System.Numerics;

namespace _2023_cs;

public class Day3
{
    private static int find_start(string line, int index)
    {
        while (index > 0 && index < line.Length && char.IsNumber(line[index - 1]))
        {
            index--;
        }

        return index;
    }

    public static int to_int(string line, int index)
    {
        int number = 0;
        for (int i = index; i < line.Length; i++)
        {
            if (!char.IsDigit(line[i]))
            {
                break;
            }

            number *= 10;
            number += line[i] - '0';
        }

        return number;
    }

    private static bool in_range(IReadOnlyList<string> lines, int i, int j)
    {
        return !(0 > i || i > lines.Count || 0 > j || j > lines[i].Length);
    }

    private static int checkAndConvert(IReadOnlyList<string> lines, int newi, int newj,
        HashSet<Tuple<int, int>> counted)
    {
        if (in_range(lines, newi, newj) && char.IsDigit(lines[newi][newj]))
        {
            counted.Add(new Tuple<int, int>(newi, newj));
            return to_int(lines[newi], find_start(lines[newi], newj));
        }

        return 0;
    }

    private static Tuple<int, int> CheckSurroundings(IReadOnlyList<string> lines, int i, int j,
        HashSet<Tuple<int, int>> counted)
    {
        int sum = 0;
        int sum2 = 0;
        List<int> numbers = new List<int>();

        //check left
        int newi = i;
        int newj = j - 1;
        numbers.Add(checkAndConvert(lines, newi, newj, counted));

        // check right 
        newj = j + 1;
        numbers.Add(checkAndConvert(lines, newi, newj, counted));

        //check up 
        newi = i - 1;
        newj = j;
        if (in_range(lines, newi, newj) && char.IsDigit(lines[newi][newj]))
        {
            numbers.Add(checkAndConvert(lines, newi, newj, counted));
        }
        else
        {
            newj = j - 1;
            numbers.Add(checkAndConvert(lines, newi, newj, counted));

            newj = j + 1;
            numbers.Add(checkAndConvert(lines, newi, newj, counted));
        }

        //check down
        newi = i + 1;
        newj = j;
        if (in_range(lines, newi, newj) && char.IsDigit(lines[newi][newj]))
        {
            numbers.Add(checkAndConvert(lines, newi, newj, counted));
        }
        else
        {
            newj = j - 1;
            numbers.Add(checkAndConvert(lines, newi, newj, counted));

            newj = j + 1;
            numbers.Add(checkAndConvert(lines, newi, newj, counted));
        }
        List<int> numbers2 = new List<int>();
        foreach (var num in numbers)
        {
            if (num != 0)
            {
                numbers2.Add(num);
            }
            
        }

        numbers = numbers2;
        sum = numbers.Sum();
        if (lines[i][j] == '*' && numbers.Count ==2)
        {
            sum2 = numbers[0] * numbers[1];
        }
        return new Tuple<int, int>(sum, sum2);
    }

    public static Tuple<int,int> Part1(string filename)
    {
        StreamReader input = new StreamReader(filename);
        List<string> lines = new List<string>();
        string? line = input.ReadLine();
        while (line != null)
        {
            lines.Add(line);
            line = input.ReadLine();
        }

        HashSet<Tuple<int, int>> counted = new HashSet<Tuple<int, int>>();
        int sum = 0;
        int sum2 = 0;
        for (int i = 0; i < lines.Count; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                if (lines[i][j] == '.' || Char.IsNumber(lines[i][j]))
                {
                    continue;
                }

                Tuple<int, int> x = CheckSurroundings(lines, i, j, counted);
                sum += x.Item1;
                sum2 += x.Item2;
            }
        }

        
        return new Tuple<int,int>(sum,sum2);
    }
}
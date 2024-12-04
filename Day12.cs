using System.Runtime.CompilerServices;

namespace _2023_cs;

public class Day12
{
    private static Dictionary<Tuple<int, int>, long> _memoized;
    
    private static bool fitsTemplate(char a, char b)
    {
        return b == '?' || a == b;
    }

    private static int CalcEnd(int springsLen, int[] template, int templateIndex)
    {
        int sum = 0;
        for (int i = templateIndex; i < template.Length; i++)
        {
            sum += template[i] + 1;
        }

        sum++;
        return springsLen - sum;
    }

    private static char[] Tochararr(List<char> list)
    {
        char[] result = new char[list.Count];
        for (int i = 0; i < list.Count; i++)
        {
            result[i] = list[i];
        }

        return result;
    }

    private static long RecSolveLine(List<char> springs, int[] template, int templateIndex, int start, char[] debug)
    {
        var memoIndex = new Tuple<int, int>(templateIndex, start);
        if (_memoized.TryGetValue(memoIndex, out var line))
        {
            return line;
        }
        if (templateIndex >= template.Length)
        {
            for (int i = start; i < springs.Count; i++)
            {
                if (springs[i]=='#')
                {
                    return 0;
                }
            }
            //Console.WriteLine(debug);
            return 1;
        }

        long sum = 0;
        int end = CalcEnd(springs.Count, template, templateIndex);
        bool onlyDots = true;
        for (int i = start; i <= end; i++)
        {
            onlyDots = onlyDots && fitsTemplate('.', springs[i]);
            if (!onlyDots)
            {
                break;
            }
            bool fits = fitsTemplate('.', springs[i]);
            //debug[i] = '.';
            for (int j = 1; j <= template[templateIndex]; j++)
            {
                fits = fits && fitsTemplate('#', springs[i + j]);
            }

            fits = fits && fitsTemplate('.', springs[i + 1 + template[templateIndex]]);
            if (onlyDots && fits)
            {
                /*for (int j = 1; j <= template[templateIndex]; j++)
                {
                    debug[i + j] = '#';
                }*/
                sum += RecSolveLine(springs, template, templateIndex + 1, i + 1 + template[templateIndex], debug);
            }
        }

        _memoized[memoIndex] = sum;
        return sum;
    }

    private static long solveLine(string line)
    {
        string[] firstSplit = line.Split(' ');
        string[] strNumbers = firstSplit[1].Split(',');
        int[] numbers = new int[strNumbers.Length];
        for (int i = 0; i < strNumbers.Length; i++)
        {
            numbers[i] = Int32.Parse(strNumbers[i]);
        }

        List<char> charSprings = new List<char>(firstSplit[0].ToCharArray());
        charSprings.Insert(0, '.');
        charSprings.Add('.');
        return RecSolveLine(charSprings, numbers, 0, 0, new char[charSprings.Count]);
    }
    
    
    private static long solveLine2(string line)
    {
        string[] firstSplit = line.Split(' ');
        string[] strNumbers = firstSplit[1].Split(',');
        int[] numbers = new int[strNumbers.Length*5];
        for (int j = 0; j < 5; j++)
        {
            for (int i = 0; i < strNumbers.Length; i++)
            {
                numbers[strNumbers.Length*j+i] = Int32.Parse(strNumbers[i]);
            }
        }
        

        List<char> charSprings = new List<char>(firstSplit[0].ToCharArray());
        charSprings.Insert(0, '.');
        for (int i = 0; i < 4; i++)
        {
            charSprings.Add('?');
            charSprings.AddRange(firstSplit[0].ToCharArray());
        }
        charSprings.Add('.');
        
        return RecSolveLine(charSprings, numbers, 0, 0, new char[charSprings.Count]);
    }

    public static void solve(string filename)
    {
        StreamReader input = new StreamReader(filename);
        string? line = input.ReadLine();
        List<string> lines = new List<string>();
        while (line != null)
        {
            lines.Add(line);
            line = input.ReadLine();
        }

        long sum = 0;
        long sum2 = 0;
        foreach (var line1 in lines)
        {
            _memoized = new Dictionary<Tuple<int, int>, long>();
            sum += solveLine(line1);
            _memoized = new Dictionary<Tuple<int, int>, long>();
            sum2 += solveLine2(line1);
            
        }
        
        
        
        //8871 too high
        //
        Console.WriteLine("Day 12 part 1");
        Console.WriteLine(sum);
        
        Console.WriteLine("Day 12 part 2");
        Console.WriteLine(sum2);
    }
}
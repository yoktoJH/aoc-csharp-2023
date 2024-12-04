
using System.Text;

namespace _2023_cs;

public static class Day13
{
    private static List<string> InverseLines(List<string> lines)
    {
        List<string> result = new List<string>();
        
        for (int i = 0; i < lines[0].Length; i++)
        {
            StringBuilder newLine = new StringBuilder();
            foreach (var t in lines)
            {
                newLine.Append(t[i]);
            }
            result.Add(newLine.ToString());
        }
        return result;
    }
    
    
    /**
     * Checks if the pattern is horizontally symmetric.
     * axis is the index of the first line above the line of symmetry
     */
    private static bool IsSymmetric(List<string> pattern, int axis)
    {
        int top = axis;
        int bottom = axis + 1;
        while (top >=0 && bottom < pattern.Count)
        {
            if (pattern[top] != pattern[bottom])
            {
                return false;
            }
            
            top--;
            bottom++;
        }

        return true;
    }

    private static int CountDifferences(string str1, string str2)
    {
        int count = 0;
        for (int i = 0; i < str1.Length; i++)
        {
            if (str1[i] != str2[i])
            {
                count++;
            }
        }

        return count;
    }
    
    private static bool IsSymmetricWithSmudge(List<string> pattern, int axis)
    {
        int top = axis;
        int bottom = axis + 1;
        int differences = 0;
        while (top >=0 && bottom < pattern.Count)
        {
            differences += CountDifferences(pattern[top], pattern[bottom]);
            if (differences >1)
            {
                return false;
            }
            
            top--;
            bottom++;
        }

        return differences == 1;
    }
    
    private static long SummarizePattern(List<string> pattern,Func<List<string>,int,bool> symmetryFunc)
    {
        long score = 0;
        List<string> inversePattern = InverseLines(pattern);
        for (int i = 0; i < pattern.Count-1; i++)
        {
            
            if (symmetryFunc(pattern,i))
            {
                score += (i+1) * 100;
            }
        }
        for (int i = 0; i < inversePattern.Count-1; i++)
        {
            if (symmetryFunc(inversePattern,i))
            {
                score += i+1;
            }
        }
        
        return score;
    }
    
    
    
    
    public static void Solve(string filename)
    {
        List<string> lines = Util.ParseFile(filename);
        List<string> pattern = new List<string>();
        long sum1 = 0;
        long sum2 = 0;
        foreach (var line in lines)
        {
            if (line == "")
            {
                sum1 += SummarizePattern(pattern,IsSymmetric);
                sum2 += SummarizePattern(pattern, IsSymmetricWithSmudge);
                pattern = new List<string>();
            }
            else
            {
                pattern.Add(line);  
            }

            
            
            
        }
        sum1 += SummarizePattern(pattern,IsSymmetric);
        sum2 += SummarizePattern(pattern, IsSymmetricWithSmudge);

        Util.PrintResult(sum1,sum2,13);
    }
}
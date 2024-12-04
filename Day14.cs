namespace _2023_cs;

public class Day14
{
    private const long Repetitions = 1000000000;

    private static List<char[]> copyState(List<char[]> state)
    {
        List<char[]> copy = new List<char[]>();
        foreach (var line in state)
        {
            char[] lineCopy = new char[line.Length];
            line.CopyTo(lineCopy, 0);
            copy.Add(lineCopy);
        }

        return copy;
    }


    private static void MoveUpOrDown(List<char[]> lines, int start, int end, int direction)
    {
        foreach (var _ in lines)
        {
            for (int j = start; j < end; j++)
            {
                for (int k = 0; k < lines[j].Length; k++)
                {
                    if (lines[j][k] != 'O' || lines[j + direction][k] != '.') continue;
                    lines[j][k] = '.';
                    lines[j + direction][k] = 'O';
                }
            }
        }
    }

    private static void MoveLeftOrRight(List<char[]> lines, int start, int end, int direction)
    {
        for (int i = 0; i < lines[0].Length; i++)
        {
            for (int j = start; j < end; j++)
            {
                for (int k = 0; k < lines.Count; k++)
                {
                    if (lines[k][j] != 'O' || lines[k][j + direction] != '.') continue;
                    lines[k][j] = '.';
                    lines[k][j + direction] = 'O';
                }
            }
        }
    }

    private static bool AlreadyHappened(List<List<char[]>> pastStates, List<char[]> state,out int index)
    {
        index = -1;
        if (pastStates.Count == 0)
        {
            return false;
        }
        foreach (var oldState in pastStates)
        {
            var correct = true;
            for (int i = 0; i < state.Count; i++)
            {
                if (!oldState[i].SequenceEqual(state[i]))
                {
                    correct = false;
                    break;
                }
                
            }

            if (correct)
            {
                index = pastStates.IndexOf(oldState);
                return true;     
            }
           
        }

        return false;
    }

    private static void Cycle(List<char[]> lines)
    {
        // move north
        MoveUpOrDown(lines, 1, lines.Count, -1);

        // move west
        MoveLeftOrRight(lines, 1, lines[0].Length, -1);

        // move south
        MoveUpOrDown(lines, 0, lines.Count - 1, +1);
        // move east
        MoveLeftOrRight(lines, 0, lines[0].Length-1, +1);
    }

    private static long part2(List<char[]> lines)
    {
        List<List<char[]>> pastStates = new List<List<char[]>>();
        for (int i = 0; i < Repetitions; i++)
        {
            Cycle(lines);


            if (AlreadyHappened(pastStates, lines,out var previousIndex))
            {

                long index = Repetitions - previousIndex-1;
                index %= (i - previousIndex);
                index += previousIndex; 
     
                return CountWeight(pastStates[(int)index]);
            }
            pastStates.Add(copyState(lines));
        }
        return CountWeight(lines);
    }

    private static long CountWeight(List<char[]> lines)
    {
        long weight = 0;
        for (int i = 0; i < lines.Count; i++)
        {
            foreach (var c in lines[i])
            {
                if (c =='O')
                {
                    weight += lines.Count - i;
                }
            }
            
        }

        return weight;
    }

private static long part1(List<char[]> lines)
    {
        foreach (var _ in lines)
        {
            for (int j = 1; j < lines.Count; j++)
            {
                for (int k = 0; k < lines[j].Length; k++)
                {
                    if (lines[j][k] != 'O' || lines[j - 1][k] != '.') continue;
                    lines[j][k] = '.';
                    lines[j - 1][k] = 'O';
                }
            }
        }

        return CountWeight(lines);
    }
    public static void solve(string filename)
    {
        List<string> lines = Util.ParseFile(filename);
        List<char[]> charLines = new List<char[]>();
        lines.ForEach(s =>charLines.Add(s.ToCharArray()));
            
        Util.PrintResult(part1(charLines),part2(charLines),14);
        
    }
}
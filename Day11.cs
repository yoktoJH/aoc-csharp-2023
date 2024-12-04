using System.Globalization;

namespace _2023_cs;

public class Day11
{
    private static int emptyBetween(List<int> empty, int min, int max)
    {
        int i = 0;
        int count = 0;
        while (i < empty.Count && empty[i] < max)
        {
            if (min < empty[i] && empty[i] < max)
            {
                count++;
            }

            i++;
        }

        return count;
    }

    public static void solve(string filename)
    {
        StreamReader input = new StreamReader(filename);
        List<string> universe = new List<string>();
        string? line = input.ReadLine();
        while (line != null)
        {
            universe.Add(line);
            line = input.ReadLine();
        }

        List<Tuple<int, int>> galaxies = new List<Tuple<int, int>>();
        List<int> empyRows = new List<int>();
        List<int> emptyCols = new List<int>();
        for (int i = 0; i < universe.Count; i++)
        {
            bool isEmptyRow = true;
            bool isEmptyCol = true;
            for (int j = 0; j < universe[i].Length; j++)
            {
                if (universe[i][j] == '#')
                {
                    isEmptyRow = false;
                    galaxies.Add(new Tuple<int, int>(i, j));
                }

                isEmptyCol = isEmptyCol && universe[j][i] == '.';
            }

            if (isEmptyRow)
            {
                empyRows.Add(i);
            }

            if (isEmptyCol)
            {
                emptyCols.Add(i);
            }
        }

        emptyCols.Sort();
        empyRows.Sort();
        int distancesSum = 0;
        long distancesSum2 = 0;
        for (int i = 0; i < galaxies.Count; i++)
        {
            for (int j = 0; j < galaxies.Count; j++)
            {
                if (i == j)
                {
                    continue;
                }

                int rowExpansion = emptyBetween(empyRows, int.Min(galaxies[i].Item1, galaxies[j].Item1),
                    int.Max(galaxies[i].Item1, galaxies[j].Item1));
                int colExpansion = emptyBetween(emptyCols, int.Min(galaxies[i].Item2, galaxies[j].Item2),
                    int.Max(galaxies[i].Item2, galaxies[j].Item2));

                /*Console.Write("Distance from " + i + " to " + j+" :");
                Console.WriteLine("row ->"+int.Abs(galaxies[i].Item1 - galaxies[j].Item1)+", " + rowExpansion +
                                   " col ->"+int.Abs(galaxies[i].Item2 - galaxies[j].Item2)+", " + colExpansion);*/
                distancesSum += int.Abs(galaxies[i].Item1 - galaxies[j].Item1) + rowExpansion;
                distancesSum += int.Abs(galaxies[i].Item2 - galaxies[j].Item2) + colExpansion;
                distancesSum2 += long.Abs(galaxies[i].Item1 - galaxies[j].Item1) + rowExpansion*999999;
                distancesSum2+= long.Abs(galaxies[i].Item2 - galaxies[j].Item2) + colExpansion*999999;
            }
        }

        Console.WriteLine("Day 11 part 1");
        Console.WriteLine(distancesSum / 2);
        
        Console.WriteLine("Day 11 part 2");
        Console.WriteLine(distancesSum2 /2);
    }
}
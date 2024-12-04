using System.Globalization;

namespace _2023_cs;

public class Day4
{
    private static int solve_card(string line)
    {
        int winnings = 0;
        HashSet<int> winning = new HashSet<int>();
        int index = line.IndexOf(':')+2;
        while (line[index] != '|')
        {
            int number = 0;
            while (char.IsDigit(line[index]))
            {
                number *= 10;
                number += line[index];
                index++;
            }

            if (number != 0)
            {
                winning.Add(number);
            }
            
            index++;
        }

        while (index < line.Length)
        {
            int number = 0;
            while (index < line.Length && char.IsDigit(line[index]) )
            {
                number *= 10;
                number += line[index];
                index++;
            }

            if (number != 0 && winning.Contains(number))
            {
                winnings += 1;

            }
            
            index++;
        }

        return winnings;
    }
    public static void solve(string filename)
    {
        int sum = 0,sum2 = 0;
        StreamReader input = new StreamReader(filename);
        string? line = input.ReadLine();
        int[] copies = new int[202];
        int cardIndex = 0;
        while (line != null)
        {
            copies[cardIndex] += 1;
            int points = solve_card(line);
            if (points !=0)
            {
                sum += (int) Math.Pow(2,points-1);
                for (int i = cardIndex+1; i < cardIndex+1+points; i++)
                {
                    copies[i] += copies[cardIndex];
                }
                
            }

            cardIndex++;
            line = input.ReadLine();
        }

        sum2 += copies.Sum();
        Console.WriteLine(sum);
        Console.WriteLine(sum2);
    }
}
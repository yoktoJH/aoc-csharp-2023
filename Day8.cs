using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Xsl;

namespace _2023_cs;

public class Day8
{
    private struct nextNode
    {
        public nextNode(string name, int distance)
        {
            this.Name = name;
            this.Distance = distance;
        }

        public string Name { get; set; }
        public int Distance { get; set; }
    }

    private class Node
    {
        public Node(string name)
        {
            this.name = name;
            travelled = 0;
        }

        public string name { get; set; }
        public int travelled { get; set; }
    }

    public static void part2(Dictionary<string, Tuple<string, string>> map, string directions)
    {
        // vždy predpokladáme, že z *A sa prejde len do jedného *Z a to dokonca vždy v rovnakom stave 
        // smerových inštrukcií. Teda cesta z *A to *Z je rovnako dlhá ako cesta z toho *Z zase to doho istého *Z.
        // preto vypočítam iba najmenší spoločný násobok dĺžky ciest a to by mal byť výsledok. Problém je že táto infomácia
        // je tak trochu "vymyslená" a zo zadania priamo nevyplýva. Pre všeobecný prípad by bol výpočet značne náročnejší,
        // keďže by bolo terba nájsť nejaký cyklus a zároveň kontrolovať všetky medzizastávky v tomto cykle.
        
        List<string> startingNodes = new List<string>();

        foreach (var node in map)
        {
            if (node.Key.Last() == 'Z')
            {
                startingNodes.Add(node.Key);
            }
        }

        static int gcd(int a, int b)
        {
            if (a == 0)
            {
                return b;
            }

            if (b==0)
            {
                return a;
            }

            return gcd(b, a % b);
        }

        List<int> distances = new List<int>();
        foreach (var startingNode in startingNodes)
        {
            string currentNOde = startingNode;
            int i = 0;
            bool first = true;
            while (first || currentNOde[2] != 'Z')
            {
                first = false;
                if (directions[i % directions.Length] == 'R')
                {
                    currentNOde = map[currentNOde].Item2;
                }
                else
                {
                    currentNOde = map[currentNOde].Item1;
                }

                i++;
            }
            distances.Add(i);
        }

        int divisor = distances[0];
        for (int i = 1; i < distances.Count; i++)
        {
            divisor = gcd(divisor, distances[i]);
        }

        long multiple = divisor;
        foreach (var distance in distances)
        {
            multiple *= distance / divisor;
        }
        
        Console.WriteLine("day 8 part 2");
        Console.WriteLine(multiple);
    }

    public static void solve(string filename)
    {
        Dictionary<string, Tuple<string, string>> nodes = new Dictionary<string, Tuple<string, string>>();
        string instructions;
        StreamReader input = new StreamReader(filename);

        string? line = input.ReadLine();

        instructions = line;
        line = input.ReadLine();
        line = input.ReadLine();


        while (line != null)
        {
            nodes.Add(line.Substring(0, 3),
                new Tuple<string, string>(line.Substring(7, 3), line.Substring(12, 3)));

            line = input.ReadLine();
        }

        input.Close();
        part2(nodes, instructions);
        string currentNOde = "AAA";
        int i = 0;
        while (currentNOde != "ZZZ")
        {
            if (instructions[i % instructions.Length] == 'R')
            {
                currentNOde = nodes[currentNOde].Item2;
            }
            else
            {
                currentNOde = nodes[currentNOde].Item1;
            }

            i++;
        }

        Console.WriteLine("day 8 part 1");
        Console.WriteLine(i);
    }
}
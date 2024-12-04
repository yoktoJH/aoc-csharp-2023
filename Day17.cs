using System.ComponentModel;
using System.Xml.Schema;
using static System.Int32;

namespace _2023_cs;

public class Day17
{
    enum DirectionEnum
    {
        UP = 0,
        RIGHT = 1,
        DOWN = 2,
        LEFT = 3,
        NONE = 4
    }

    private static readonly int[,] Shifts = { { 0, -1 }, { 1, 0 }, { 0, 1 }, { -1, 0 } };


    private class Tile
    {
        public Tile(int heatLoss)
        {
            HeatLoss = heatLoss;
        }
        

        public int[][] Distance { get; set; } =
        {
            new[]
            {
                MaxValue, MaxValue, MaxValue, MaxValue, MaxValue, MaxValue, MaxValue, MaxValue, MaxValue, MaxValue
            },
            new[]
            {
                MaxValue, MaxValue, MaxValue, MaxValue, MaxValue, MaxValue, MaxValue, MaxValue, MaxValue, MaxValue
            },
            new[]
            {
                MaxValue, MaxValue, MaxValue, MaxValue, MaxValue, MaxValue, MaxValue, MaxValue, MaxValue, MaxValue
            },
            new[]
            {
                MaxValue, MaxValue, MaxValue, MaxValue, MaxValue, MaxValue, MaxValue, MaxValue, MaxValue, MaxValue
            },
        };

        public DirectionEnum Direction { get; set; }

        public int HeatLoss { get; }

        public int X { get; set; }

        public int Y { get; set; }

        public int MinInDirection(int direction, int index, int indexStart, int indexEnd)
        {
            int minimum = MaxValue;
            if (index > 0)
            {
                return Distance[direction][index - 1];
            }

            for (int i = 0; i < 4; i++)
            {
                

                for (int j = indexStart; j < indexEnd; j++)
                {
                    if (i == direction || (i + 2) % 4 == direction)
                    {
                        continue;
                    }
                    minimum = Min(minimum, Distance[i][j]);
                }
                
            }

            return minimum;
        }
    }


    private static void Loosen(Tile[,] tiles, int i, int j, int minHops, int maxHops)
    {
        var tile = tiles[i, j];

        for (int direction = 0; direction < 4; direction++)
        {
            var ii = i + Shifts[direction, 1];
            var jj = j + Shifts[direction, 0];
            if (0 > ii || ii >= height || 0 > jj || jj >= width)
            {
                continue;
            }

            var neighbour = tiles[ii, jj];
            for (int index = 0; index < maxHops; index++)
            {
                var min = tile.MinInDirection(direction, index,minHops-1,maxHops);
                if (min == MaxValue)
                {
                    continue;
                }
                
                neighbour.Distance[direction][index] = Min(min + neighbour.HeatLoss, neighbour.Distance[direction][index]);
              /*  if (min + neighbour.HeatLoss < neighbour.Distance[direction][index])
                {
                    neighbour.Distance[direction][index] = min + neighbour.HeatLoss;
                }*/
            }
        }
    }

    private static int Part1(List<string> lines)
    {
        var tiles = new Tile[lines.Count, lines[0].Length];

        for (int i = 0; i < lines.Count; i++)
        {
            for (int j = 0; j < lines[0].Length; j++)
            {
                tiles[i, j] = new Tile(lines[i][j] - '0');
            }
        }

        tiles[0, 1].Distance[1][0] = tiles[0, 1].HeatLoss;
        tiles[1, 0].Distance[2][0] = tiles[1, 0].HeatLoss;
        for (int n = 0; n < width + height; n++)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Loosen(tiles, i, j,1,3);
                }
            }
            //  Console.WriteLine(tiles[9,12].Distance[0][0]);
        }


        int min = MaxValue;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                min = Min(min, tiles[height - 1, width - 1].Distance[i][j]);
            }
        }

        return min;
    }

    private static int Part2(List<string> lines)
    {
        var tiles = new Tile[lines.Count, lines[0].Length];

        for (int i = 0; i < lines.Count; i++)
        {
            for (int j = 0; j < lines[0].Length; j++)
            {
                tiles[i, j] = new Tile(lines[i][j] - '0');
            }
        }

        tiles[0, 1].Distance[1][0] = tiles[0, 1].HeatLoss;
        tiles[1, 0].Distance[2][0] = tiles[1, 0].HeatLoss;
        for (int n = 0; n < width + height; n++)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Loosen(tiles, i, j,4,10);
                }
            }
            //  Console.WriteLine(tiles[9,12].Distance[0][0]);
        }


        int min = MaxValue;
        for (int i = 0; i < 4; i++)
        {
            // 3 here because you need to ignore invalid results
            for (int j = 3; j < 10; j++)
            {
                min = Min(min, tiles[height - 1, width - 1].Distance[i][j]);
            }
        }

        return min;
    }


    private static int width;
    private static int height;

    public static void solve(string filename)
    {
        var lines = Util.ParseFile(filename);
        width = lines[0].Length;
        height = lines.Count;
        var sdfa = Part1(lines);
        Util.PrintResult(sdfa, Part2(lines), 17);
    }
}
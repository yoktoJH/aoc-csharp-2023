namespace _2023_cs;

public class Day10
{
    private static Tuple<int, int>? SCoord(List<string> map)
    {
        for (int i = 0; i < map.Count; i++)
        {
            for (int j = 0; j < map[i].Length; j++)
            {
                if (map[i][j] == 'S')
                {
                    return new Tuple<int, int>(i, j);
                }
            }
        }

        return null;
    }


    private static bool in_range(List<string> map, int x, int y)
    {
        return 0 <= x && x < map.Count && 0 <= y && y < map[0].Length;
    }
    
    private static bool in_range(List<char[]> map, int x, int y)
    {
        return 0 <= x && x < map.Count && 0 <= y && y < map[0].Length;
    }

    private static List<Tuple<int, int, Direction>> whereToAfterStart(List<string> map, Tuple<int, int> start)
    {
        int x = start.Item1;
        int y = start.Item2;
        List<Tuple<int, int, Direction>> result =
            new List<Tuple<int, int, Direction>>();
        char[] toNorth = { '|', '7', 'F' };
        char[] toSouth = { '|', 'L', 'J' };
        char[] toWest = { 'L', '-', 'F' };
        char[] toEast = { '-', '7', 'J' };


        if (in_range(map, x - 1, y) && toNorth.Contains(map[x - 1][y]))
        {
            result.Add(new Tuple<int, int, Direction>(x - 1, y, Direction.South));
        }

        if (in_range(map, x + 1, y) && toSouth.Contains(map[x + 1][y]))
        {
            result.Add(new Tuple<int, int, Direction>(x + 1, y, Direction.North));
        }

        if (in_range(map, x, y - 1) && toWest.Contains(map[x][y - 1]))
        {
            result.Add(new Tuple<int, int, Direction>(x, y - 1, Direction.East));
        }

        if (in_range(map, x, y + 1) && toEast.Contains(map[x][y + 1]))
        {
            result.Add(new Tuple<int, int, Direction>(x, y + 1, Direction.West));
        }

        return result;
    }


    private class Position
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Direction CameFrom { get; set; }
        public Direction Inside { get; set; }

        public Position(int x, int y, Direction direction)
        {
            X = x;
            Y = y;
            CameFrom = direction;
        }
    }

    private static void nextStep(char square, Position position)
    {
        Direction[] directions = { Direction.North, Direction.East, Direction.South, Direction.West };
        switch (square)
        {
            case '|':
                if (position.CameFrom == Direction.North)
                {
                    position.X++;
                }
                else
                {
                    position.X--;
                }

                break;
            case '-':
                if (position.CameFrom == Direction.West)
                {
                    position.Y++;
                }
                else
                {
                    position.Y--;
                }

                break;
            case 'L':
                if (position.CameFrom == Direction.North)
                {
                    position.Y++;
                    position.CameFrom = Direction.West;
                    position.Inside = directions[((int)position.Inside + 3) % 4];
                }
                else
                {
                    position.X--;
                    position.CameFrom = Direction.South;
                    position.Inside = directions[((int)position.Inside + 1) % 4];
                }

                break;
            case 'J':
                if (position.CameFrom == Direction.North)
                {
                    position.Y--;
                    position.CameFrom = Direction.East;
                    position.Inside = directions[((int)position.Inside + 1) % 4];
                }
                else
                {
                    position.X--;
                    position.CameFrom = Direction.South;
                    position.Inside = directions[((int)position.Inside + 3) % 4];
                }


                break;
            case '7':
                if (position.CameFrom == Direction.South)
                {
                    position.Y--;
                    position.CameFrom = Direction.East;
                    position.Inside = directions[((int)position.Inside + 3) % 4];
                }
                else
                {
                    position.X++;
                    position.CameFrom = Direction.North;
                    position.Inside = directions[((int)position.Inside + 1) % 4];
                }

                break;
            case 'F':
                if (position.CameFrom == Direction.South)
                {
                    position.Y++;
                    position.CameFrom = Direction.West;
                    position.Inside = directions[((int)position.Inside + 1) % 4];
                }
                else
                {
                    position.X++;
                    position.CameFrom = Direction.North;

                    position.Inside = directions[((int)position.Inside + 3) % 4];
                }

                break;
        }
    }

    private static long furthestTile(List<string> map, List<Tuple<int, int, Direction>> nextSteps,
        List<char[]> modifiableMap)
    {
        long steps = 1;
        List<Position> positions = new List<Position>();
        foreach (var tuple in nextSteps)
        {
            positions.Add(new Position(tuple.Item1, tuple.Item2, tuple.Item3));
        }

        while (positions[0].X != positions[1].X || positions[0].Y != positions[1].Y)
        {
            foreach (var position in positions)
            {
                char c = map[position.X][position.Y];
                modifiableMap[position.X][position.Y] = 'X';
                nextStep(c, position);
            }

            steps++;
        }

        modifiableMap[positions[0].X][positions[0].Y] = 'X';
        return steps;
    }

    private enum Direction
    {
        North,
        East,
        South,
        West
    }

    private static List<char[]> toCharsList(List<string> map)
    {
        return map.Select(line => line.ToCharArray()).ToList();
    }

    private static bool isConnected(char left, char right)
    {
        char[] lefts = { '-', 'L', 'F', 'S' };
        char[] rightts = { '-', 'J', '7', 'S' };
        return lefts.Contains(left) && rightts.Contains(right);
    }


    private static void fill(List<char[]> map, Position position,ref char insideChar)
    {
        Tuple<int, int>[] shifts =
        {
            new(-1, 0), new(0, +1), new(+1, 0),
            new(0, -1)
        };

        int x = position.X + shifts[(int)position.Inside].Item1;
        int y = position.Y + shifts[(int)position.Inside].Item2;
        

        while (in_range(map,x,y)&&map[x][y] != 'X')
        {
            map[x][y] = 'I';
            x += shifts[(int)position.Inside].Item1;
            y += shifts[(int)position.Inside].Item2;
            
        }

        if (insideChar == 'X' && !in_range(map,x,y))
        {
            insideChar = ' ';
        }

        x = position.X + shifts[((int)position.Inside + 2) % 4].Item1;
        y = position.Y + shifts[((int)position.Inside + 2) % 4].Item2;

        while (in_range(map,x,y)&& map[x][y] != 'X')
        {
            map[x][y] = ' ';
            x += shifts[((int)position.Inside + 2) % 4].Item1;
            y += shifts[((int)position.Inside + 2) % 4].Item2;
           
        }
        if (insideChar == 'X' && !in_range(map,x,y))
        {
            insideChar = 'I';
        }
    }

    private static int inCount(List<char[]> map, char c)
    {
        int counter = 0;
        foreach (var line in map)
        {
            foreach (var character in line)
            {
                if (character ==c)
                {
                    counter++;
                }
            }
        }

        return counter;
    }
    
    private static long countInside(List<char[]> map, List<string> oldMap, Position start)
    {
        char inside = 'X';
        Position old = new Position(0, 0, Direction.North);
        while (oldMap[start.X][start.Y] != 'S')
        {
                old.X = start.X;
                old.Y = start.Y;
            fill(map,start,ref inside);
            nextStep(oldMap[start.X][start.Y], start);
            if (oldMap[old.X][old.Y] !='-' && oldMap[old.X][old.Y] !='|')
            {
                old.Inside = start.Inside;
                fill(map, old, ref inside);
            }
        }
        fill(map,start, ref inside);
        // check edges to figure out what character fills out the inside
        //Int32.Min(inCount(map,'I'),inCount(map,' '));
        return inCount(map, inside);
    }


    public static void solve(string filename)
    {
        StreamReader input = new StreamReader(filename);
        List<string> map = new List<string>();
        string? line = input.ReadLine();
        while (line != null)
        {
            map.Add(line);
            line = input.ReadLine();
        }

        Tuple<int, int>? start = SCoord(map);
        if (start == null)
        {
            Console.WriteLine("S not found");
            return;
        }

        List<Tuple<int, int, Direction>> nextSteps = whereToAfterStart(map, start);
        List<char[]> charMap = toCharsList(map);
        charMap[start.Item1][start.Item2] = 'X';
        Console.WriteLine("DAY 10 part 1");
        Console.WriteLine(furthestTile(map, nextSteps, charMap));

        
        Position fillStart = new Position(nextSteps[0].Item1, nextSteps[0].Item2, nextSteps[0].Item3);
        if (start.Item1 == nextSteps[0].Item1)
        {
            fillStart.Inside = Direction.South;
        }
        else
        {
            fillStart.Inside = Direction.East;
        }

        Console.WriteLine("DAY 10 part 2");
        Console.WriteLine(countInside(charMap, map, fillStart));
        StreamWriter output = new StreamWriter("C:\\JOZIK\\aoc\\2023_cs\\output.txt");
        foreach (var l in charMap)
        {
            output.WriteLine(l);
        }
        output.Close();
    }
}
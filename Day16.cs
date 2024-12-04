using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace _2023_cs;

public class Day16
{
    enum DirectionEnum
    {
        UP = 0,
        RIGHT = 1,
        DOWN = 2,
        LEFT = 3
    }

    private static readonly int[,] Shifts =  { { 0, -1 }, { 1, 0 }, { 0, 1 }, { -1, 0 } };

    private class Ray
    {
        public int y { get; set; }
        public int x { get; set; }
        public DirectionEnum Direction { get; set; }


        public bool Move(bool[,,] visited, List<string> lines, ref Ray? another)
        {
            if (!( x < 0 || x >= lines[0].Length || y < 0 || y >= lines.Count))
            {
                if (visited[y, x, (int)Direction])
                {
                    return false;
                }
                visited[y, x, (int)Direction] = true;
            }
            

           
            x += Shifts[(int)Direction, 0];
            y += Shifts[(int)Direction, 1];
            if (x < 0 || x >= lines[0].Length || y < 0 || y >= lines.Count)
            {
                return false;
            }

            var c = lines[y][x];
            if (c == '/')
            {
                Direction = Direction switch
                {
                    DirectionEnum.RIGHT => DirectionEnum.UP,
                    DirectionEnum.UP => DirectionEnum.RIGHT,
                    DirectionEnum.DOWN => DirectionEnum.LEFT,
                    DirectionEnum.LEFT => DirectionEnum.DOWN,
                    _ => Direction
                };
            }
            else if (c == '\\')
            {
                Direction = Direction switch
                {
                    DirectionEnum.RIGHT => DirectionEnum.DOWN,
                    DirectionEnum.UP => DirectionEnum.LEFT,
                    DirectionEnum.DOWN => DirectionEnum.RIGHT,
                    DirectionEnum.LEFT => DirectionEnum.UP,
                    _ => Direction
                };
            }
            else if (c == '-')
            {
                switch (Direction)
                {
                    case DirectionEnum.UP:
                        Direction = DirectionEnum.LEFT;
                        another = new Ray { x = this.x, y = this.y, Direction = DirectionEnum.RIGHT };
                        break;
                    case DirectionEnum.DOWN:
                        Direction = DirectionEnum.LEFT;
                        another = new Ray { x = this.x, y = this.y, Direction = DirectionEnum.RIGHT };
                        break;
                }
            }
            else if (c == '|')
            {
                switch (Direction)
                {
                    case DirectionEnum.LEFT:
                        Direction = DirectionEnum.UP;
                        another = new Ray { x = this.x, y = this.y, Direction = DirectionEnum.DOWN };
                        break;
                    case DirectionEnum.RIGHT:
                        Direction = DirectionEnum.UP;
                        another = new Ray { x = this.x, y = this.y, Direction = DirectionEnum.DOWN };
                        break;
                }
            }

            return true;
        }
    }

    private static int Part1(List<string> lines, Queue<Ray> rays)
    {
        
        var visted = new bool[lines.Count, lines[0].Length, 4];
        
        while (rays.Count >0)
        {
            var ray = rays.Dequeue();
            Ray? another = null;
            if (ray.Move(visted, lines, ref another))
            {
                rays.Enqueue(ray);
                if (another != null)
                {
                    rays.Enqueue(another);
                }
            }
        }
        int result = 0;
        for (int i = 0; i < lines.Count; i++)
        {
            for (int j = 0; j < lines[0].Length; j++)
            {
                for (int k = 0; k < 4; k++)
                {
                    if (!visted[i, j, k]) continue;
                    result++;

                    break;
                }
            }
        }

        return result;
    }

    private static int Part2(List<string> lines)
    {
        var rays = new Queue<Ray>();
        int maximum = 0;
        for (int i = 0; i < lines[0].Length; i++)
        {
            rays.Enqueue(new Ray{x = i , y = -1, Direction = DirectionEnum.DOWN});
            maximum = int.Max(maximum, Part1(lines, rays));
        }

        for (int i = 0; i < lines[0].Length; i++)
        {
            rays.Enqueue(new Ray{x = i , y = lines.Count, Direction = DirectionEnum.UP});
            maximum = int.Max(maximum, Part1(lines, rays));
        }

        for (int i = 0; i < lines.Count; i++)
        {
            rays.Enqueue(new Ray{x = -1 , y = i, Direction = DirectionEnum.RIGHT});
            maximum = int.Max(maximum, Part1(lines, rays));
        }

        for (int i = 0; i < lines.Count; i++)
        {
            rays.Enqueue(new Ray{x = lines[0].Length , y = i, Direction = DirectionEnum.LEFT});
            maximum = int.Max(maximum, Part1(lines, rays));
        }
        return maximum;
    }
    public static void solve(string filename)
    {
        var lines = Util.ParseFile(filename);
        var rays = new Queue<Ray>();
        rays.Enqueue(new Ray{x=-1,y=0,Direction = DirectionEnum.RIGHT});
        Util.PrintResult(Part1(lines,rays),Part2(lines),16);
    }
}
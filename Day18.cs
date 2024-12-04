using System.Globalization;

namespace _2023_cs;

public class Day18
{
    public class Edge
    {
        public long[][] Sides = { new long[] { 0, 0 }, new long[] { 0, 0 } };
        public long x;
        public long y;

        public Edge(long x, long y)
        {
            this.x = x;
            this.y = y;
        }
    }

    private static long GAP = 1000000;
    private static long _innerSide = 0;
    private static readonly long[][] _currentSides = { new long[] { -1, 0 }, new long[] { 1, 0 } };
    private static char _currentDirection = 'U';
    private static Dictionary<char, long[]> directions = new Dictionary<char, long[]>();
    private static Dictionary<char, char> translationTable = new Dictionary<char, char>();


    private static void TurnLeft()
    {
        _innerSide--;
        (_currentSides[0][0], _currentSides[0][1]) = (-_currentSides[0][1], _currentSides[0][0]);
        (_currentSides[1][0], _currentSides[1][1]) = (-_currentSides[1][1], _currentSides[1][0]);
    }

    private static void TurnRight()
    {
        _innerSide++;
        (_currentSides[0][0], _currentSides[0][1]) = (_currentSides[0][1], -_currentSides[0][0]);
        (_currentSides[1][0], _currentSides[1][1]) = (_currentSides[1][1], -_currentSides[1][0]);
    }

    private static void TurnAround()
    {
        _currentSides[0][0] *= -1;
        _currentSides[0][1] *= -1;
        _currentSides[1][0] *= -1;
        _currentSides[1][1] *= -1;
    }

    public static void Turn(char newDirection)
    {
        switch (_currentDirection)
        {
            case 'U':
                switch (newDirection)
                {
                    case 'D':
                        TurnAround();
                        break;
                    case 'R':
                        TurnRight();
                        break;
                    case 'L':
                        TurnLeft();
                        break;
                }

                break;
            case 'R':
                switch (newDirection)
                {
                    case 'L':
                        TurnAround();
                        break;
                    case 'D':
                        TurnRight();
                        break;
                    case 'U':
                        TurnLeft();
                        break;
                }

                break;
            case 'D':
                switch (newDirection)
                {
                    case 'U':
                        TurnAround();
                        break;
                    case 'L':
                        TurnRight();
                        break;
                    case 'R':
                        TurnLeft();
                        break;
                }

                break;
            case 'L':
                switch (newDirection)
                {
                    case 'R':
                        TurnAround();
                        break;
                    case 'U':
                        TurnRight();
                        break;
                    case 'D':
                        TurnLeft();
                        break;
                }

                break;
        }

        _currentDirection = newDirection;
    }

    private static long maxy = long.MinValue;
    private static long maxx = long.MinValue;
    private static long minx = long.MaxValue;
    private static long miny = long.MaxValue;


    public class Interval
    {
        public long start;
        public long end;

        public Interval(long start, long end)
        {
            this.start = start;
            this.end = end;
        }
    }

    private static LinkedList<Interval> _intervals = new LinkedList<Interval>();


    private static void AddInterval(Interval newInterval)
    {
        bool combined = true;
        while (combined)
        {
            combined = false;
            var node = _intervals.First;
            while (node != null)
            {
                if (newInterval.start >= node.Value.start && newInterval.start <= node.Value.end &&
                    newInterval.end >= node.Value.start && newInterval.end <= node.Value.end)
                {
                    return;
                }

                if (newInterval.end == node.Value.start)
                {
                    newInterval.end = node.Value.end;
                    _intervals.Remove(node);
                    combined = true;
                    break;
                }

                if (newInterval.start == node.Value.end)
                {
                    newInterval.start = node.Value.start;
                    _intervals.Remove(node);
                    combined = true;
                    break;
                }

                node = node.Next;
            }
        }

        _intervals.AddFirst(newInterval);
        return;
    }

    public static bool RemoveInterval(Interval interval)
    {
        var node = _intervals.First;
        while (node != null)
        {
            if (interval.start >= node.Value.start && interval.start <= node.Value.end &&
                interval.end >= node.Value.start && interval.end <= node.Value.end)
            {
                if (interval.start - 1 > node.Value.start)
                {
                    _intervals.AddFirst(new Interval(node.Value.start, interval.start));
                }

                bool wasReused = false;
                if (interval.end + 1 < node.Value.end)
                {
                    node.Value.start = interval.end;
                    wasReused = true;
                }

                if (!wasReused)
                {
                    _intervals.Remove(node);
                }

                return true;
            }

            node = node.Next;
        }

        return false;
    }

    private static long SumIntervals()
    {
        return _intervals.Sum(interval => interval.end - interval.start - 1);
    }

    public static void solve(string filename)
    {
        directions['U'] = new long[] { 0, 1 };
        directions['D'] = new long[] { 0, -1 };
        directions['R'] = new long[] { 1, 0 };
        directions['L'] = new long[] { -1, 0 };
        
        long edgeCount = 0;
        Dictionary<string, Edge> edges = new Dictionary<string, Edge>();
        var lines = Util.ParseFile(filename);
        long x = 0;
        long y = 0;
        
        foreach (var line in lines)
        {
            
            /*////dekete
            var distance = long.Parse(values[1]);
            Turn(values[0][0]);

            distanceTravelled += distance;
            ////dekete*/
            var values = line.Split(' ');
            Turn(values[0][0]);
            var edge = new Edge(x, y);
            edge.Sides[0][0] = _currentSides[0][0];
            edge.Sides[0][1] = _currentSides[0][1];
            edge.Sides[1][1] = _currentSides[1][1];
            edge.Sides[1][0] = _currentSides[1][0];
            edges[x + "," + y] = edge;
            edgeCount += long.Parse(values[1]);
            minx = long.Min(x, minx);
            miny = long.Min(y, miny);
            maxx = long.Max(x, maxx);
            maxy = long.Max(y, maxy);
            for (long i = 0; i < long.Parse(values[1]); i++)
            {
                x += directions[_currentDirection][0];
                y += directions[_currentDirection][1];

                edge = new Edge(x, y);
                edge.Sides[0][0] = _currentSides[0][0];
                edge.Sides[0][1] = _currentSides[0][1];
                edge.Sides[1][1] = _currentSides[1][1];
                edge.Sides[1][0] = _currentSides[1][0];
                edges[x + "," + y] = edge;
                edges[x + "," + y + _currentDirection] = edge;
            }
        }


        //part two
        // 0 means R, 1 means D, 2 means L, and 3 means U.
        translationTable['0'] = 'R';
        translationTable['1'] = 'D';
        translationTable['2'] = 'L';
        translationTable['3'] = 'U';
        
        x = 0;
        y = 0;
        var edgeStorage = new Dictionary<long, List<long>>();
        long distanceTravelled = 0;
        foreach (var line in lines)
        {
            var values = line.Split(' ');
            long distance = long.Parse(values[2].Substring(2, 5), NumberStyles.HexNumber);

            distanceTravelled += distance;
            Console.WriteLine(distanceTravelled);

            Turn(translationTable[values[2][7]]);
            
            if (!edgeStorage.ContainsKey(x))
            {
                edgeStorage[x] = new List<long>();
            }

            edgeStorage[x].Add(y);

            for (long i = 0; i < distance; i++)
            {
                x += directions[_currentDirection][0];
                y += directions[_currentDirection][1];
                if (!edgeStorage.ContainsKey(x))
                {
                    edgeStorage[x] = new List<long>();
                }

                edgeStorage[x].Add(y);
            }
        }

        //part one stuff
        HashSet<string> otherTiles = new HashSet<string>();
        long innerSide = 0;
        if (_innerSide > 0)
        {
            innerSide = 1;
        }

        foreach (var edge in edges)
        {
            x = edge.Value.x;
            y = edge.Value.y;
            otherTiles.Add(x + "," + y);
            x += edge.Value.Sides[innerSide][0];
            y += edge.Value.Sides[innerSide][1];
            while (!edges.ContainsKey(x + "," + y))
            {
                otherTiles.Add(x + "," + y);
                x += edge.Value.Sides[innerSide][0];
                y += edge.Value.Sides[innerSide][1];
            }
        }


        var surfacePartTwo = CalculateSurface(distanceTravelled, edgeStorage);

        Util.PrintResult(otherTiles.Count, surfacePartTwo, 18);
        
// works only with old way of solving this
        //PrintShape("MyTest.txt", otherTiles, edges);
    }

    private static long CalculateSurface(long distanceTravelled, Dictionary<long, List<long>> edgeStorage)
    {
        long surface = distanceTravelled;
        List<Interval> toAddIntervals = new List<Interval>();

        long maximum = edgeStorage.Keys.Max();
        for (long i = edgeStorage.Keys.Min(); i <= maximum; i++)
        {
            var line = edgeStorage[i];
            
            //Console.WriteLine(i);
            var points = line.Order().Distinct().ToArray();
            line.Clear();
            int index = 1;
            while (index < points.Length)
            {
                int startIndex = index - 1;
                while (index < points.Length && points[index - 1] == points[index] - 1)
                {
                    index++;
                }

                if (points[startIndex] == points[index - 1])
                {
                    index++;
                    continue;
                }

                var interval = new Interval(points[startIndex], points[index - 1]);
                if (!RemoveInterval(interval))
                {
                    toAddIntervals.Add(interval);
                }

                index++;
            }
            
            surface += SumIntervals();

            foreach (var rInterval in toAddIntervals)
            {
                AddInterval(rInterval);
            }
            toAddIntervals.Clear();
            edgeStorage.Remove(i);
        }

        return surface;
    }

    private static void PrintShape(string path, HashSet<string> otherTiles, Dictionary<string, Edge> edges)
    {
        using (StreamWriter sw = new StreamWriter(path, false))
        {
            for (long i = maxy; i >= miny; i--)
            {
                for (long j = minx; j <= maxx; j++)
                {
                    if (otherTiles.Contains(j + "," + i))
                    {
                        if (edges.ContainsKey(j + "," + i))
                        {
                            sw.Write("#");
                        }
                        else
                        {
                            sw.Write("+");
                        }
                    }
                    else
                    {
                        sw.Write(".");
                    }
                }

                sw.WriteLine();
            }
        }
    }
}
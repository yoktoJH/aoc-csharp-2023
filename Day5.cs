using System.Collections;

namespace _2023_cs;

public class Day5
{
    private struct MapLine
    {
        public long SourceStart;
        public long DestStart;
        public long Range;

        public MapLine(long sourceStart, long destStart, long range)
        {
            this.SourceStart = sourceStart;
            this.DestStart = destStart;
            this.Range = range;
        }
    }

    
    private static bool correctSeed ( List<long> seeds, long seed)
    {
        for (int i = 0; i < seeds.Count-1; i+=2)
        {
            if (seeds[i] < seed && seeds[i] + seeds[i + 1] > seed)
                return true;
        }

        return false;
    }
    private class Map
    {
        private readonly List<MapLine> mappings = new List<MapLine>();

        public Map(StreamReader input)
        {
            string? line = input.ReadLine();
            while (!string.IsNullOrEmpty(line))
            {
                if (line.Contains(':'))
                {
                    line = input.ReadLine();
                    continue;
                }

                line = line.TrimEnd();
                string[] strValues = line.Split(' ');
                mappings.Add(new MapLine(Int64.Parse(strValues[1]),
                    Int64.Parse(strValues[0]), Int64.Parse(strValues[2])));

                line = input.ReadLine();
            }
        }

        public long translate(long source)
        {
            foreach (var mapping in mappings)
            {
                if (mapping.SourceStart <= source && mapping.SourceStart + mapping.Range > source)
                {
                    return source - mapping.SourceStart + mapping.DestStart;
                }
            }

            return source;
        }

        public long reverseTranslate(long dest)
        {
            foreach (var mapping in mappings)
            {
                if (mapping.DestStart <= dest && mapping.DestStart + mapping.Range > dest)
                {
                    return dest - mapping.DestStart + mapping.SourceStart;
                }
            }

            return dest;
        }

        public MapLine lowestSourceDest()
        {
            MapLine lowestMapping = new MapLine(0, Int64.MaxValue, 0);
            foreach (var maping in mappings)
            {
                if (maping.DestStart < lowestMapping.DestStart)
                {
                    lowestMapping = maping;
                }
            }

            return lowestMapping;
        }
    }

    public static void solve(string filename)
    {
        StreamReader input = new StreamReader(filename);
        string? line = input.ReadLine();
        line = line.TrimEnd();
        string[] strseeds = line.Split(' ');
        List<long> seeds = new List<long>();
        for (int i = 1; i < strseeds.Length; i++)
        {
            seeds.Add(Convert.ToInt64(strseeds[i]));
        }
        line = input.ReadLine();
        List<Map> mapChain = new List<Map>();
        for (int i = 0; i < 7; i++)
        {
            mapChain.Add(new Map(input));
        }

        long minimum = Int64.MaxValue;
        for (int i = 1; i < seeds.Count; i++)
        {
            long tmp = seeds[i];
            foreach (var map in mapChain)
            {
                tmp = map.translate(tmp);
            }

            minimum = Int64.Min(minimum, tmp);
        }

        Console.WriteLine("Day 5");
        Console.WriteLine(minimum);
        //part 2
        long minimum2 = Int64.MaxValue;
        MapLine lowest = mapChain[mapChain.Count - 1].lowestSourceDest();
        mapChain.Reverse();
        long j = 0;
        while (true)
        {
            if (j%10000000==0)
            {
                Console.WriteLine("10mil done");
            }
            long tmp = j;
            foreach (var map in mapChain)
            {
                tmp = map.reverseTranslate(tmp);
            }

            if (correctSeed(seeds, tmp))
            {
                minimum2 = j;
                break;
            }
            j++;
        }


        Console.WriteLine(minimum2);
    }
}
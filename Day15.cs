namespace _2023_cs;

public class Day15
{
    private static int hash(string sequence)
    {
        int hash = 0;
        foreach (var c in sequence)
        {
            hash += c;
            hash *= 17;
            hash %= 256;
        }

        return hash;
    }

    class Lens
    {
        public string Label { get; set; }
        public int FocalLength { get; set; }

        public Lens(string label, int focalLength)
        {
            Label = label;
            FocalLength = focalLength;
        }
    }

    private static void removeLens(List<Lens> box, string label)
    {
        foreach (var t in box)
        {
            if (t.Label == label)
            {
                box.Remove(t);
                return;
            }
            
        }
    }

    private static void replaceLens(List<Lens> box, string label, int focalLength)
    {
        foreach (var t in box)
        {
            if (t.Label == label)
            {
                t.FocalLength = focalLength;
                return;
            }
            
        }

        box.Add(new Lens(label, focalLength));
    }

    public static void solve(string filename)
    {
        int sum1 = 0;
        int sum2 = 0;
        List<Lens>[] boxes = new List<Lens>[256];
        for (int i = 0; i < 256; i++)
        {
            boxes[i] = new List<Lens>();
        }


        List<string> lines = Util.ParseFile(filename);
        foreach (var line in lines)
        {
            string[] sequences = line.Split(',');
            foreach (var sequence in sequences)
            {
                sum1 += hash(sequence);
                int actionIndex = sequence.IndexOf('=');
                if (actionIndex == -1)
                {
                    actionIndex = sequence.IndexOf('-');
                }

                string label = sequence[..actionIndex];
                int boxid = hash(label);
                if (sequence[actionIndex] == '=')
                {
                    replaceLens(boxes[boxid], label, sequence[actionIndex+1]-'0');
                }
                else
                {
                    removeLens(boxes[boxid],label);
                }
            }
        }

        for (int i = 0; i < boxes.Length; i++)
        {
            for (int j = 0; j < boxes[i].Count; j++)
            {
                
                sum2 += (i + 1) * (j+1) * boxes[i][j].FocalLength;
            }
        }

        Util.PrintResult(sum1, sum2, 15);
    }
}
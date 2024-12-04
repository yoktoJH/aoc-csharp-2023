namespace _2023_cs;

public class Day7
{
    private enum HandTypes
    {
        Highcard,
        Pair,
        Twopair,
        Threekind,
        Fullhouse,
        Fourkind,
        Fivekind
    }

    private class Hand : IComparable<Hand>
    {
        public readonly string Cards;
        public  HandTypes Type;
        public readonly long Bid;
        protected char[] janeviemcostry;

        public Hand(string cards, long bid)
        {
            janeviemcostry = new[] { '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A' };
            this.Bid = bid;
            this.Cards = cards;
            this.Type = FindType(cards);
        }

        protected static HandTypes chooseType(Dictionary<char, int> counter)
        {
            Dictionary<int, int> counts = new Dictionary<int, int>();
            foreach (var pairs in counter)
            {
                if (counts.ContainsKey(pairs.Value))
                {
                    counts[pairs.Value] += 1;
                }
                else
                {
                    counts[pairs.Value] = 1;
                }
            }

            int x;
            if (counts.TryGetValue(5, out x))
            {
                return HandTypes.Fivekind;
            }

            if (counts.TryGetValue(4, out x))
            {
                return HandTypes.Fourkind;
            }

            if (counts.TryGetValue(3, out x) && counts.TryGetValue(2, out x))
            {
                return HandTypes.Fullhouse;
            }

            if (counts.TryGetValue(3, out x))
            {
                return HandTypes.Threekind;
            }


            if (!counts.TryGetValue(2, out x)) return HandTypes.Highcard;
            if (x == 2)
            {
                return HandTypes.Twopair;
            }

            return HandTypes.Pair;
        }

        public static HandTypes FindType(string cards)
        {
            Dictionary<char, int> counter = new Dictionary<char, int>();

            foreach (var c in cards)
            {
                if ((counter.ContainsKey(c)))
                {
                    counter[c] += 1;
                }
                else
                {
                    counter[c] = 1;
                }
            }

            return chooseType(counter);
        }

        public int CompareTo(Hand? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            int typeComparison = Type - other.Type;
            if (typeComparison != 0)
            {
                return typeComparison;
            }


            List<char> cardValues = new List<char>(janeviemcostry);
            for (int i = 0; i < 5; i++)
            {
                int x = cardValues.IndexOf(Cards[i]) - cardValues.IndexOf(other.Cards[i]);
                if (x != 0)
                {
                    return x;
                }
            }

            return 0;
        }
    }


    private class Hand2 : Hand
    {
        public Hand2(string cards, long bid) : base(cards, bid)
        {
            janeviemcostry = new[] { 'J', '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'Q', 'K', 'A' };
            this.Type = FindType(cards);
        }

        public new static HandTypes FindType(string cards)
        {
            Dictionary<char, int> counter = new Dictionary<char, int>(); 
            int maximum = 0;
            char maxkey = 'x';
            foreach (var c in cards)
            {
                if (counter.ContainsKey(c))
                {
                    counter[c] += 1;
                }
                else
                {
                    counter[c] = 1;
                }

                if (c !='J'&& counter[c]> maximum)
                {
                    maximum = counter[c];
                    maxkey = c;
                }
            }

            int jokers;
            bool hasJokers  = counter.TryGetValue('J',out jokers);
            
            if (jokers is 0 or 5)
            {
                return chooseType(counter);
            }
            counter.Remove('J');
            counter[maxkey] += jokers;
            return chooseType(counter);
        }
    }

    public static void solve(string filename)
    {
        StreamReader input = new StreamReader(filename);
        string? line = input.ReadLine();
        List<Hand> hands = new List<Hand>();
        List<Hand2> hand2s = new List<Hand2>();
        while (line != null)
        {
            string[] splitLine = line.Split(' ');
            hands.Add(new Hand(splitLine[0], Convert.ToInt64(splitLine[1])));
            hand2s.Add(new Hand2(splitLine[0], Convert.ToInt64(splitLine[1])));
            line = input.ReadLine();
        }

        hands.Sort();
        hand2s.Sort();
        long sum = 0;
        long sum2 = 0;
        for (int i = 0; i < hands.Count; i++)
        {
            long x = i;
            sum += hands[i].Bid * (x + 1);
            sum2 += hand2s[i].Bid * (x + 1);
        }

        Console.WriteLine("Day 7 part1");
        Console.WriteLine(sum);
        Console.WriteLine("Day 7 part2");
        Console.WriteLine(sum2);

        
    }
}
using System.Diagnostics;

namespace _2023_cs;

public class Day19
{
    private class Part
    {
        public Dictionary<char, long> ratings = new Dictionary<char, long>();

        public Part(long x, long m, long a, long s)
        {
            ratings['x'] = x;
            ratings['m'] = m;
            ratings['a'] = a;
            ratings['s'] = s;
        }
    }

    private class Rule
    {
        private readonly bool _isLess;
        private readonly char _rating;
        private readonly long _value;
        public string next;

        public Rule(long value, char rating, string next, bool isLess = false)
        {
            _isLess = isLess;
            _rating = rating;
            _value = value;
            this.next = next;
        }

        public bool IsSatisfied(Part part)
        {
            if (_isLess)
            {
                return part.ratings[_rating] < _value;
            }

            return part.ratings[_rating] > _value;
        }
    }

    private interface IWorkflow
    {
        public string NextWorkflow(Part part);
    }

    private class BasicWorkflow : IWorkflow
    {
        private readonly List<Rule> _rules = new();
        public string lastRule = "";


        public void AddRule(string rule)
        {
            if (!rule.Contains(':'))
            {
                lastRule = rule;
                return;
            }

            char c = rule[1];
            var split = rule.Split(':');
            var splitRule = split[0].Split(c);
            _rules.Add(new Rule(int.Parse(splitRule[1]), rule[0], split[1], c == '<'));
        }

        public string NextWorkflow(Part part)
        {
            foreach (var rule in _rules)
            {
                if (rule.IsSatisfied(part))
                {
                    return rule.next;
                }
            }

            return lastRule;
        }
    }

    private class FinalWorkflow : IWorkflow
    {
        public long Score { get; private set; } = 0;

        public string NextWorkflow(Part part)
        {
            Score += part.ratings['x'] + part.ratings['m'] + part.ratings['a'] + part.ratings['s'];
            return "";
        }
    }


    private static Dictionary<string, IWorkflow> _workflows = new Dictionary<string, IWorkflow>();

    public static void solve(string filename)
    {
        _workflows["A"] = new FinalWorkflow();
        _workflows["R"] = new FinalWorkflow();
        var lines = Util.ParseFile(filename);
        var index = 0;
        while (index < lines.Count && lines[index] != "")
        {
            var split = lines[index].Split('{');
            var workflow = new BasicWorkflow();
            var rules = split[1];
            rules = rules.Replace("}", "");
            var splitRules = rules.Split(",");
            foreach (var rule in splitRules)
            {
                workflow.AddRule(rule);
            }

            _workflows[split[0]] = workflow;
            ++index;
        }

        if (lines[index] == "")
        {
            index++;
        }

        while (index < lines.Count)
        {
            var line = lines[index].Substring(1, lines[index].Length - 2);
            var values = line.Split(",");
            var part = new Part(int.Parse(values[0][2..]), int.Parse(values[1][2..]),
                int.Parse(values[2][2..]), int.Parse(values[3][2..]));
            string workflow = "in";
            while (workflow!= "")
            {
                workflow = _workflows[workflow].NextWorkflow(part);
            }

            index++;
        }
        Util.PrintResult(((FinalWorkflow) _workflows["A"]).Score,0,19);
    }
}
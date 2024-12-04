// See https://aka.ms/new-console-template for more information

namespace _2023_cs;

internal class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine(Day3.Part1("examples/example3.txt"));
        //Console.WriteLine(Day3.Part1("inputs/input3.txt"));
        
	Console.WriteLine("day 4 example");
	Day4.solve("examples/example4.txt");
        
	Console.WriteLine("input");
        //Day4.solve("inputs/input4.txt");
        
        Day5.solve("examples/example5.txt");
        //Day5.solve("inputs/input5.txt");
        
        //Day6.solve("inputs/input6.txt");


        Day7.solve("examples/example7.txt");
        //Day7.solve("inputs/input7.txt");
        
        // does not work as there is no AAA node
        //Day8.solve("examples/example8.txt");
        //Day8.solve("inputs/input8.txt");
        
        
        Day9.solve("examples/example9.txt");
        //Day9.solve("inputs/input9.txt");
        
        Day10.solve("examples/example10.txt");
        //Day10.solve("inputs/input10.txt");
        
        Day11.solve("examples/example11.txt");
        //Day11.solve("inputs/input11.txt");
        
        Day12.solve("examples/example12.txt");
        //Day12.solve("inputs/input12.txt");
        
        Day13.Solve("examples/example13.txt");
        //Day13.Solve("inputs/input13.txt");
        
        Day14.solve("examples/example14.txt");
        //Day14.solve("inputs/input14.txt");
        
        Day15.solve("examples/example15.txt");
        //Day15.solve("inputs/input15.txt");    
        
        Day16.solve("examples/example16.txt");
        //Day16.solve("inputs/input16.txt"); 
        
        Day17.solve("examples/example17.txt");
        //Day17.solve("inputs/input17.txt"); 
        
        Day18.solve("examples/example18.txt");
        //Day18.solve("inputs/input18.txt");
       
        Day19.solve("examples/example19.txt");
        //Day19.solve("inputs/input19.txt"); 
    }
}
using System;

namespace TravellingSalesmanProblem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1.txt");
            var plain1 = new Plain(@"C:\Users\janbi\Desktop\1.txt");
            Console.WriteLine(plain1.TSP(true) + "\n");

            Console.WriteLine("2.txt");
            var plain2 = new Plain(@"C:\Users\janbi\Desktop\2.txt");
            Console.WriteLine(plain2.TSP(true) + "\n");

            Console.WriteLine("3.txt");
            var plain3 = new Plain(@"C:\Users\janbi\Desktop\3.txt");
            Console.WriteLine(plain3.TSP(true) + "\n");
        }
    }
}

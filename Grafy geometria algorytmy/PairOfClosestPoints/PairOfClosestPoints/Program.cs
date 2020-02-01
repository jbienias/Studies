using System;

namespace PairOfClosestPoints
{
    class Program
    {
        static void Main(string[] args)
        {
            var plain = new Plain(@"C:\Users\janbi\Desktop\punkty.txt");

            var result = plain.ClosestPointsDistance();
            Console.WriteLine("Divide and conquer algorithm: ");
            Console.WriteLine("Closest pair of points: " + result[0] + ", " + result[1]);
            Console.WriteLine("Distance: " + result[2]);
            Console.WriteLine();
            result = plain.naiveClosestPointDistance(plain.Points);
            Console.WriteLine("Naive algorithm: ");
            Console.WriteLine("Closest pair of points: " + result[0] + ", " + result[1]);
            Console.WriteLine("Distance: " + result[2]);
        }
    }
}

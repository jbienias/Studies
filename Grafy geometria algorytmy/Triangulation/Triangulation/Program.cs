using System;

namespace Triangulation
{
    class Program
    {
        static void Main(string[] args)
        {
            var polygon = new Polygon(@"C:\Users\janbi\Desktop\laborki1.txt");
            Console.WriteLine("Wczytane punkty (preprocessing):");
            foreach (var p in polygon.Points)
                Console.WriteLine(p.ToString(2));

            Console.WriteLine();
            Console.WriteLine("Linie:");
            var lines = polygon.Triangulation();
            foreach (var l in lines)
                Console.WriteLine(l.ToString(false));
        }
    }
}

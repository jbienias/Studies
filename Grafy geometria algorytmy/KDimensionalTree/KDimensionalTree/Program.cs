using System;

namespace KDimensionalTree
{
    class Program
    {
        static void Main(string[] args)
        {
            var plain = new Plain(@"C:\Users\janbi\Desktop\punktyW.txt");
            var root = Node.BuildTree(plain.Points);

            Console.WriteLine("Root: " + root);
            Node.PrintInOrder(root);

            var area = new Area(0, 5, 0, 8);
            Console.WriteLine();

            Console.WriteLine("Query: ");
            var query = Node.Query(root, area);
            foreach (var q in query)
                Console.WriteLine(q);
        }
    }
}

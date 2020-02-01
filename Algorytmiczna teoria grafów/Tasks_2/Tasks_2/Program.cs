using System;
using Tasks_2.Helper;

namespace Tasks_2
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileName = @"C:\Users\janbi\Desktop\tablica.txt";
            var g = new UndirectedGraph(fileName, true);
            //Console.WriteLine(g);
            Console.WriteLine("Cykl Eulera: (" + fileName + ")");
            var cycle = g.EulerCycle();
            Console.WriteLine(ArrayHelper.ToString(cycle, true));
        }
    }
}

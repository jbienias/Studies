using System;

//Jan Bienias 238201

namespace SimplePolygonKernel
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                var polygon = new Polygon(args[0]);
                Console.WriteLine("Local minimas: ");
                foreach (var min in polygon.Minimas)
                    Console.WriteLine(min);
                Console.WriteLine("\nLocal maximas: ");
                foreach (var max in polygon.Maximas)
                    Console.WriteLine(max);
                Console.WriteLine("\nMINIMUM: " + polygon.Min);
                Console.WriteLine("\nMAXIMUM: " + polygon.Max);
                Console.WriteLine("\nKernel Exists: " + polygon.KernelExists + "\n");
                double polygonPerimeter = polygon.calculateKernelPerimeter(printPerimeterPoints: true);
                Console.WriteLine("\nPerimeter: " + polygonPerimeter + "\n");
            }
            else
            {
                Console.WriteLine("File name containing points wasn't !");
                Console.WriteLine("Try: '.exe filepath/filename.extension'");
            }
        }
    }
}

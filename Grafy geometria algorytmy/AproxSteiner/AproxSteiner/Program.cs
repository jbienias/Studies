using System;

namespace AproxSteiner
{
    class Program
    {
        static void Main(string[] args)
        {
            var plain = new Plain(@"C:/Users/janbi/Desktop/p5.txt");
            var steinerLines = plain.Steiner();
            foreach (var line in steinerLines)
                Console.WriteLine(line);
        }
    }
}

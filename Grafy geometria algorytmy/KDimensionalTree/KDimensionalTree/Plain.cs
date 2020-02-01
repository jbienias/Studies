using System;
using System.Collections.Generic;
using System.IO;

namespace KDimensionalTree
{
    public class Plain
    {
        public List<Point> Points { get; private set; }

        public Plain(string filePath)
        {
            if (File.Exists(filePath))
            {
                Points = new List<Point>();
                var lines = File.ReadAllLines(filePath);
                for (int i = 0; i < lines.Length; i++)
                {
                    var x = 0;
                    var y = 0;
                    string[] point = lines[i].Split(',');
                    Int32.TryParse(point[0], out x);
                    Int32.TryParse(point[1], out y);
                    Points.Add(new Point("p" + (i + 1), x, y));
                }
            }
            else
                throw new FileNotFoundException("File " + filePath + " does not exist!");
        }

        public Point this[int i]
        {
            get { return Points[i]; }
            set { Points[i] = value; }
        }
    }
}
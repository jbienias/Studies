using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AproxSteiner
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
                    Points.Add(new Point(x, y));
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

        //z prawego gornego rogu do lewego dolnego
        //https://inf.ug.edu.pl/~zylinski/dydaktyka/GGA/GGAW6.pdf str. 30 z uzyciem tylko if'a z 4 linijki
        public List<Point> DetectMaximas(List<Point> points)
        {
            //dostawiana kartka ma być otwarta po x i po y = posortuj malejaco po Y a potem rosnaco po X
            //glownie po to zeby mniejsze y zostaly wychwycone na ostatnim x
            //wszystkie punkty w ktorych x = xmax
            var list = points.OrderBy(p => p.X).ThenByDescending(p => p.Y).ToList();
            var D = new List<Point>();
            D.Add(list[list.Count - 1]);
            var yMax = D[0].Y;
            for (int i = list.Count - 2; i >= 0; i--)
            {
                if (list[i].Y >= yMax)
                {
                    D.Add(list[i]);
                    yMax = list[i].Y;
                }
            }
            if (D.Count == 1)
            {
                var p = D[0];
                list.Remove(p);
                var secondD = new List<Point>();
                secondD.Add(list[list.Count - 1]);
                yMax = secondD[0].Y;
                for (int i = list.Count - 2; i >= 0; i--)
                {
                    if (list[i].Y >= yMax)
                    {
                        secondD.Add(list[i]);
                        yMax = list[i].Y;
                    }
                }
                secondD = secondD.OrderByDescending(x => x.Manhatan).ToList();
                D.Add(secondD[0]);
            }
            return D;
        }

        public List<Candidate> CreateCandidates(List<Point> points)
        {
            var candidates = new List<Candidate>();
            for (int i = 0; i < points.Count - 1; i++)
                candidates.Add(new Candidate(points[i], points[i + 1]));
            return candidates;
        }

        public void DeleteCandidateParents(List<Point> points, Candidate candidate)
        {
            points.Remove(candidate.FirstParent);
            points.Remove(candidate.SecondParent);
        }

        public List<Line> Steiner()
        {
            var lines = new List<Line>();
            var points = new List<Point>(Points);
            points.Add(new Point(0, 0)); //r
            points = points.OrderBy(p => p.X).ThenBy(p => p.Y).ToList();
            while (points.Count > 1)
            {
                var D = DetectMaximas(points);
                var candidates = CreateCandidates(D);
                candidates = candidates.OrderBy(p => p.Point.Manhatan).ToList();
                var lastCandidate = candidates[candidates.Count - 1];
                DeleteCandidateParents(points, lastCandidate);
                points.Add(lastCandidate.Point);
                Line l1;
                Line l2;
                if (lastCandidate.Point.Equals(lastCandidate.FirstParent) || lastCandidate.Point.Equals(lastCandidate.SecondParent))
                {

                    l1 = new Line(
                            new Point(Math.Max(lastCandidate.FirstParent.X, lastCandidate.SecondParent.X), Math.Max(lastCandidate.FirstParent.Y, lastCandidate.SecondParent.Y)),
                            new Point(Math.Max(lastCandidate.FirstParent.X, lastCandidate.SecondParent.X), Math.Min(lastCandidate.FirstParent.Y, lastCandidate.SecondParent.Y))
                            );
                    l2 = new Line(
                            new Point(Math.Min(lastCandidate.FirstParent.X, lastCandidate.SecondParent.X), Math.Min(lastCandidate.FirstParent.Y, lastCandidate.SecondParent.Y)),
                            new Point(Math.Max(lastCandidate.FirstParent.X, lastCandidate.SecondParent.X), Math.Min(lastCandidate.FirstParent.Y, lastCandidate.SecondParent.Y))
                            );
                }
                else
                {
                    l1 = new Line(lastCandidate.SecondParent, lastCandidate.Point);
                    l2 = new Line(lastCandidate.FirstParent, lastCandidate.Point);
                }
                if (!l1.A.Equals(l1.B))
                    lines.Add(l1);
                if (!l2.A.Equals(l2.B))
                    lines.Add(l2);
            }
            return lines;
        }
    }
}

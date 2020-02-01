using System;
using System.Collections.Generic;
using System.Linq;

namespace KDimensionalTree
{
    public enum NodeType
    {
        X, //X line
        Y, //Y line
        Leaf
    }

    public class Node //KDTree Node, where k = 2
    {
        public Point Point;
        public Node Left;
        public Node Right;
        public Node Parent;
        public NodeType Type;
        public int Depth;
        public Area Area;

        public Node(Point point, Node left, Node right, Node parent, int depth, NodeType type)
        {
            Type = type;
            Depth = depth;
            Point = point;
            Left = left;
            Right = right;
            Parent = parent;
        }

        public static Node BuildTree(List<Point> points)
        {
            if (points == null || points.Count == 0)
                return null;
            var Sx = Point.Sort(points, true);
            var Sy = Point.Sort(Sx, false);
            var root = buildTree(Sx, Sy, 0);
            root.Area = new Area(double.MinValue, double.MaxValue, double.MinValue, double.MaxValue);
            return root;
        }

        private static Node buildTree(List<Point> Sx, List<Point> Sy, int depth)
        {
            Node node = null;
            if (Sx.Count == 1)
            {
                node = new Node(Sx[0], null, null, null, depth, NodeType.Leaf);
                return node;
            }
            else if (Sx.Count > 1)
            {
                List<Point> SxLeft = new List<Point>();
                List<Point> SxRight = new List<Point>();
                List<Point> SyLeft = new List<Point>();
                List<Point> SyRight = new List<Point>();

                var medianIndex = (Sx.Count - 1) / 2;
                var half = medianIndex + 1;

                if (depth % 2 == 0)
                {
                    var medianPoint = Sx[medianIndex];
                    node = new Node(medianPoint, null, null, null, depth, NodeType.X);
                    SxLeft = Sx.GetRange(0, half);
                    SxRight = Sx.GetRange(half, Sx.Count - half);

                    foreach (var point in Sy)
                    {
                        if (point.X <= medianPoint.X)
                            SyLeft.Add(point);
                        else
                            SyRight.Add(point);
                    }
                }
                else
                {
                    var medianPoint = Sy[medianIndex];
                    node = new Node(medianPoint, null, null, null, depth, NodeType.Y);
                    SyLeft = Sy.GetRange(0, half);
                    SyRight = Sy.GetRange(half, Sy.Count - half);

                    foreach (var point in Sx)
                    {
                        if (point.Y <= medianPoint.Y)
                            SxLeft.Add(point);
                        else
                            SxRight.Add(point);
                    }
                }
                node.Left = buildTree(SxLeft, SyLeft, depth + 1);
                node.Left.Parent = node;
                node.Right = buildTree(SxRight, SyRight, depth + 1);
                node.Right.Parent = node;
            }

            return node;
        }

        public static List<Point> Query(Node root, Area area) //top-down
        {
            if (root.Type == NodeType.Leaf)
            {
                if (area.PointBelongs(root.Point))
                    return new List<Point>() { root.Point };
                else
                    return new List<Point>();
            }

            var points = new List<Point>();

            decreaseAreaOfNode(root.Left, true);

            if (area.Contains(root.Left.Area))
            {
                var pointsLeft = reportSubtree(root.Left);
                points = points.Concat(pointsLeft).ToList();
            }
            else if (area.Intersects(root.Left.Area))
            {
                var pointsLeft = Query(root.Left, area);
                points = points.Concat(pointsLeft).ToList();
            }

            decreaseAreaOfNode(root.Right, false);

            if (area.Contains(root.Right.Area))
            {
                var pointsRight = reportSubtree(root.Right);
                points = points.Concat(pointsRight).ToList();
            }
            else if (area.Intersects(root.Right.Area))
            {
                var pointsRight = Query(root.Right, area);
                points = points.Concat(pointsRight).ToList();
            }

            return points;
        }

        private static List<Point> reportSubtree(Node root)
        {
            if (root.Type == NodeType.Leaf)
            {
                return new List<Point>() { root.Point };
            }
            else
            {
                List<Point> leftPoints = reportSubtree(root.Left);
                List<Point> rightPoints = reportSubtree(root.Right);
                var combined = leftPoints.Concat(rightPoints);
                return combined.ToList();
            }
        }

        private static void decreaseAreaOfNode(Node node, bool leftSon)
        {
            var parentArea = node.Parent.Area;
            node.Area = new Area(parentArea);
            if (node.Depth % 2 == 0) //ojcem był zatem Y, ew. zapisac node.Parent.Depth % 2 == 1
            {
                if (leftSon)
                    node.Area.Top = node.Parent.Point.Y;
                else
                    node.Area.Bottom = node.Parent.Point.Y;
            }
            else //ojcem był zatem X
            {
                if (leftSon)
                    node.Area.Right = node.Parent.Point.X;
                else
                    node.Area.Left = node.Parent.Point.X;
            }
        }

        public static void PrintInOrder(Node root)
        {
            if (root.Left != null) PrintInOrder(root.Left);
            Console.WriteLine(root);
            if (root.Right != null) PrintInOrder(root.Right);
        }

        public static void PreOrder(Node root)
        {
            Console.WriteLine(root);
            if (root.Left != null) PreOrder(root.Left);
            if (root.Right != null) PreOrder(root.Right);
        }

        public static void PostOrder(Node root)
        {
            if (root.Left != null) PostOrder(root.Left);
            if (root.Right != null) PostOrder(root.Right);
            Console.WriteLine(root);
        }

        public override string ToString()
        {
            var result = "[" + Point;
            if (Type == NodeType.X) result += ", vertical/x line";
            if (Type == NodeType.Y) result += ", horizontal/y line";
            if (Type == NodeType.Leaf) result += ", leaf";
            result += ", depth: " + Depth + "]";
            return result;
        }
    }
}

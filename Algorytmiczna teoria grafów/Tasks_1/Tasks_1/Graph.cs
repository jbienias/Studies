using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Tasks_1
{
    public class Graph
    {
        public int[,] AdjacencyMatrix { get; private set; }
        public List<char> Names { get; private set; }
        public List<int> Degrees { get; private set; }
        public List<List<int>> AdjacencyList { get; private set; }

        public Graph(int n)
        {
            AdjacencyMatrix = new int[n, n];
            Names = new List<char>();
            Degrees = new List<int>();
            for (int i = 0; i < n; i++)
            {
                Names.Add(((char)(97 + i)));
                Degrees.Add(0);
            }
            CreateAdjacencyList();
        }

        public Graph(int[,] matrix)
        {
            int n = matrix.GetLength(0);
            if (matrix.GetLength(0) != matrix.GetLength(1))
                throw new ArgumentException("Niepoprawny rozmiar macierzy!");
            AdjacencyMatrix = new int[n, n];
            Names = new List<char>();
            Degrees = new List<int>();
            for (int i = 0; i < n; i++)
            {
                Names.Add(((char)(97 + i)));
                Degrees.Add(0);
            }
            SetupDegreesList();
            CreateAdjacencyList();
        }

        public Graph(string fileName)
        {
            if (!File.Exists(fileName))
                throw new FileNotFoundException("Plik (" + fileName + ") z macierzą nie istnieje!");
            var lines = File.ReadAllLines(fileName);
            int n = lines.Length;
            if (n == 0)
                throw new FileLoadException("Plik (" + fileName + ") jest pusty!");
            int m = lines[0].Split(' ').Length;
            if (n != m)
                throw new FileLoadException("Plik (" + fileName + ") nie zawiera poprawnej macierzy (n x n)!");
            int[,] array = new int[n, n];
            try
            {
                for (int i = 0; i < n; i++)
                {
                    var line = lines[i].Split(' ');
                    for (int j = 0; j < n; j++)
                    {
                        Int32.TryParse(line[j], out array[i, j]);
                    }
                }
            }
            catch (Exception) { throw new Exception("Plik (" + fileName + ") nie zawiera poprawnej macierzy (n x n)!"); }

            if (!ValidateArray(array))
                throw new ArgumentException("Wczytana macierz z pliku (" + fileName + ") jest niepoprawna!");

            AdjacencyMatrix = array;
            Names = new List<char>();
            Degrees = new List<int>();
            for (int i = 0; i < n; i++)
            {
                Names.Add(((char)(97 + i)));
                Degrees.Add(0);
            }
            SetupDegreesList();
            CreateAdjacencyList();
        }

        private bool ValidateArray(int[,] array)
        {
            if (array.GetLength(0) != array.GetLength(1))
                return false;
            var n = array.GetLength(0);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (array[i, j] > 1 || array[i, j] < 0 || array[i, j] != array[j, i])
                        return false;
                    else if (i == j && array[i, j] != 0)
                        return false;
                }
            }
            return true;
        }

        public bool AddVertex(char name)
        {
            int exists = Names.FindIndex(e => e == name);
            if (exists == -1)
            {
                int n = this.AdjacencyMatrix.GetLength(0);
                int[,] NewAdjacencyMatrix = new int[n + 1, n + 1];
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                        NewAdjacencyMatrix[i, j] = AdjacencyMatrix[i, j];
                AdjacencyMatrix = NewAdjacencyMatrix;
                Names.Add(name);
                Degrees.Add(0);
                AdjacencyList.Add(new List<int>());
                return true;
            }
            else
                return false;
        }

        public bool AddVertex()
        {
            int lastChar = Names[Names.Count - 1];
            var ch = ((char)(lastChar + 1));
            return AddVertex(ch);
        }

        public bool DeleteVertex(char name)
        {
            int exists = Names.FindIndex(e => e == name);
            if (exists != -1)
            {
                int index = exists;
                int n = this.AdjacencyMatrix.GetLength(0);
                int[,] NewAdjacencyMatrix = new int[n - 1, n - 1];

                bool iFlag = false;
                for (int i = 0; i < n - 1; i++)
                {
                    if (i >= index)
                        iFlag = true;

                    bool jFlag = false;
                    for (int j = 0; j < n - 1; j++)
                    {
                        if (j >= index)
                            jFlag = true;

                        if (iFlag && jFlag)
                            NewAdjacencyMatrix[i, j] = AdjacencyMatrix[i + 1, j + 1];
                        else if (iFlag && !jFlag)
                            NewAdjacencyMatrix[i, j] = AdjacencyMatrix[i + 1, j];
                        else if (!iFlag && jFlag)
                            NewAdjacencyMatrix[i, j] = AdjacencyMatrix[i, j + 1];
                        else
                            NewAdjacencyMatrix[i, j] = AdjacencyMatrix[i, j];
                    }
                }
                AdjacencyMatrix = NewAdjacencyMatrix;
                Names.RemoveAt(index);
                Degrees.RemoveAt(index);
                CreateAdjacencyList();
                SetupDegreesList();
                return true;
            }
            else
                return false;
        }

        public bool DeleteVertex(int id)
        {
            if (id < Names.Count && id >= 0)
                return DeleteVertex(Names[id]);
            else
                return false;
        }

        public bool AddEdge(char a, char b)
        {
            int i = Names.FindIndex(e => e == a);
            int j = Names.FindIndex(e => e == b);
            if (i == -1 || j == -1 || i == j) //i == j - obsluga grafu prostego
                return false;
            else
            {
                if (AdjacencyMatrix[i, j] == 1 || AdjacencyMatrix[j, i] == 1) //obsluga grafu prostego
                    return false;
                AdjacencyMatrix[i, j] += 1;
                AdjacencyMatrix[j, i] += 1;
                AdjacencyList[i].Add(j);
                AdjacencyList[i].Sort();
                AdjacencyList[j].Add(i);
                AdjacencyList[j].Sort();
                Degrees[i] += 1;
                Degrees[j] += 1;
                return true;
            }
        }

        public bool AddEdge(int a, int b)
        {
            if (a < Names.Count && a >= 0 && b < Names.Count && b >= 0)
                return AddEdge(Names[a], Names[b]);
            else
                return false;
        }

        public bool DeleteEdge(char a, char b)
        {
            int i = Names.FindIndex(e => e == a);
            int j = Names.FindIndex(e => e == b);
            if (i == -1 || j == -1)
                return false;
            else
            {
                if (AdjacencyMatrix[i, j] == 0 || AdjacencyMatrix[j, i] == 0)
                    return false;
                AdjacencyMatrix[i, j] -= 1;
                AdjacencyMatrix[j, i] -= 1;
                AdjacencyList[i].Remove(j);
                AdjacencyList[j].Remove(i);
                Degrees[i] -= 1;
                Degrees[j] -= 1;
                return true;
            }
        }

        public bool DeleteEdge(int a, int b)
        {
            if (a < Names.Count && a >= 0 && b < Names.Count && b >= 0)
                return DeleteEdge(Names[a], Names[b]);
            else
                return false;
        }

        public int FindVertexIndexWithDegree(int degree)
        {
            for (int i = 0; i < Degrees.Count; i++)
                if (degree == Degrees[i])
                    return i;
            return -1;
        }

        private void SetupDegreesList()
        {
            int n = AdjacencyMatrix.GetLength(0);
            for (int i = 0; i < n; i++)
            {
                int degree = 0;
                for (int j = 0; j < n; j++)
                {
                    degree += AdjacencyMatrix[i, j];
                }
                Degrees[i] = degree;
            }
        }

        public int Degree(char a)
        {
            int index = Names.FindIndex(e => e == a);
            if (index != -1)
                return Degrees[index];
            else
                return index;
        }

        public int Degree(int id)
        {
            if (id < Names.Count && id >= 0)
                return Degree(Names[id]);
            else
                return -1;
        }

        public int FindMaxDegree()
        {
            return Degrees.Max();
        }

        public int FindMinDegree()
        {
            return Degrees.Min();
        }

        public int CountEvenDegrees()
        {
            return Degrees.FindAll(e => e % 2 == 0).Count;
        }

        public int CountOddDegrees()
        {
            return Degrees.FindAll(e => e % 2 == 1).Count;
        }

        public List<int> DegreesSortedAscending()
        {
            return new List<int>(Degrees).OrderBy(x => x).ToList();
        }

        public List<int> DegreesSortedDescending()
        {
            return new List<int>(Degrees).OrderByDescending(x => x).ToList();
        }

        public bool FindC3Naive()
        {
            int n = AdjacencyMatrix.GetLength(0);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (AdjacencyMatrix[i, j] > 0 && i != j)
                    {
                        for (int k = 0; k < n; k++)
                        {
                            if (k != j && k != i && AdjacencyMatrix[i, k] > 0 && AdjacencyMatrix[j, k] > 0) //k > 0
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public int FindC3ByMatrixMul3()
        {
            int n = AdjacencyMatrix.GetLength(0);
            var AdjacencyMatrixCubed = ArrayHelper.Multiply(AdjacencyMatrix, ArrayHelper.Multiply(AdjacencyMatrix, AdjacencyMatrix));
            int sum = 0;
            for (int i = 0; i < n; i++)
            {
                sum += AdjacencyMatrixCubed[i, i];
            }
            return (sum / 6);
        }

        public bool FindC3ByMatrixMul()
        {
            int n = AdjacencyMatrix.GetLength(0);
            var AdjacencyMatrixSquare = ArrayHelper.Multiply(AdjacencyMatrix, AdjacencyMatrix);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (AdjacencyMatrixSquare[i, j] > 0 && AdjacencyMatrix[i, j] > 0)
                        return true;
                }
            }
            return false;
        }

        public int CountEdges()
        {
            int n = AdjacencyMatrix.GetLength(0);
            int counter = 0;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (AdjacencyMatrix[i, j] == 1)
                        counter++;
                }
            }

            return counter / 2;
        }

        public bool IsAcyclic()
        {
            return ((AdjacencyMatrix.GetLength(0) - 1) == CountEdges());
        }

        public bool IsConnected()
        {
            for (int i = 0; i < AdjacencyList.Count; i++)
            {
                if (AdjacencyList[i].Count == 0)
                    return false;
            }
            return true;
        }

        public List<int> BFS(int startIndex)
        {

            int n = AdjacencyMatrix.GetLength(0);
            if (startIndex < 0 || startIndex >= n)
                throw new ArgumentException("Graf o indexie " + startIndex + " nie istnieje!");

            List<int> queue = new List<int>();
            bool[] visited = new bool[n];

            queue.Add(startIndex);
            visited[startIndex] = true;

            List<int> path = new List<int>();

            while (queue.Count != 0)
            {
                int v = queue[0];
                queue.RemoveAt(0);

                path.Add(v);

                for (int i = 0; i < AdjacencyList[v].Count; i++)
                {
                    if (!visited[AdjacencyList[v][i]])
                    {
                        queue.Add(AdjacencyList[v][i]);
                        visited[AdjacencyList[v][i]] = true;
                    }
                }
            }
            return path;
        }

        public List<int> JordanCenterBFS()
        {
            int n = AdjacencyMatrix.GetLength(0);
            List<int> path = new List<int>();

            path = BFS(0);
            int v1 = path.Last();

            path = BFS(v1);
            int v2 = path.Last();

            if (path.Count % 2 == 1)
                return new List<int>() { path[path.Count / 2] };
            else
                return new List<int>() { path[path.Count / 2], path[path.Count / 2 - 1] };

        }

        public List<int> JordanCenterHKC()
        {
            int n = AdjacencyMatrix.GetLength(0);

            bool[] visited = new bool[n];
            int[] newIndex = new int[n];

            List<int> queue = new List<int>();
            queue.Add(0);
            visited[0] = true;

            int[] C = new int[n];
            C[0] = -1;

            for (int i = 0; i < n; i++)
            {
                foreach (int j in AdjacencyList[i])
                {

                    if (!visited[j])
                    {
                        queue.Add(j);
                        visited[j] = true;
                        C[j] = i;
                    }
                }
                newIndex[queue[i]] = i;
            }

            int[] tmp = new int[n];
            Array.Copy(C, tmp, n);

            for (int i = 1; i < n; i++)
                C[newIndex[i]] = newIndex[tmp[i]];

            return HKC(C, newIndex);
        }

        private List<int> HKC(int[] C, int[] newIndex)
        {
            int n = AdjacencyMatrix.GetLength(0);
            int[] height = new int[n];
            int diameter = 0;

            for (int i = (n - 1); i > 0; i--)
            {
                var remote = C[i];
                int newHeight = height[i] + 1;
                int newDiam = height[remote] + newHeight;

                if (newHeight > height[remote])
                    height[remote] = newHeight;

                if (newDiam > diameter)
                    diameter = newDiam;
            }

            int radiusFloor = diameter / 2;

            List<int> center = new List<int>();

            for (int i = 0; i < n; i++)
            {
                if (height[i] == radiusFloor)
                {
                    if (diameter % 2 == 1)
                    {
                        center.Add(i);
                        center.Add(C[i]);
                    }
                    else
                        center.Add(i);
                    break;
                }
            }

            var result = new List<int>();
            for (int i = 0; i < n; i++)
            {
                if (center.Contains(newIndex[i]))
                    result.Add(i);

            }
            return result;
        }

        public List<int> JordanCenterNaive()
        {
            int n = AdjacencyList.Count;
            var AdjacencyListCopy = ArrayHelper.Copy(AdjacencyList);
            List<int> leafs = new List<int>();
            for (int i = 0; i < n; i++)
                if (AdjacencyList[i].Count == 1)
                    leafs.Add(i);

            int howManyResults = 0;
            if (n % 2 == 0)
                howManyResults = 2;
            else
                howManyResults = 1;
            int end = n;

            while (end > howManyResults)
            {
                var currentLeaf = leafs[0];
                leafs.RemoveAt(0);
                AdjacencyListCopy[currentLeaf] = new List<int>();
                end--;

                for (int i = 0; i < AdjacencyListCopy.Count; i++)
                {
                    if (AdjacencyListCopy[i].Contains(currentLeaf))
                    {
                        AdjacencyListCopy[i].Remove(currentLeaf);
                        if (AdjacencyListCopy[i].Count == 1)
                            leafs.Add(i);
                    }
                }
            }
            return leafs;
        }

        private void CreateAdjacencyList()
        {
            int n = AdjacencyMatrix.GetLength(0);
            AdjacencyList = new List<List<int>>();
            for (int i = 0; i < n; i++)
            {
                AdjacencyList.Add(new List<int>());
                for (int j = 0; j < n; j++)
                {
                    if (AdjacencyMatrix[i, j] > 0 && AdjacencyMatrix[j, i] > 0)
                        AdjacencyList[i].Add(j);
                }
            }
        }

        public List<int> FindCN()
        {
            int minDegree = FindMinDegree();
            if (minDegree < 2)
                throw new Exception("Minimalny stopień grafu jest mniejszy od 2!");
            var largestDegreeVertexIndex = Degrees.IndexOf(Degrees.Max());
            var list = FindCNRecursive(new List<int>() { largestDegreeVertexIndex }, minDegree);

            return list;
        }

        private List<int> FindCNRecursive(List<int> cycle, int minDegree)
        {
            int n = AdjacencyMatrix.GetLength(0);
            if (cycle.Count >= minDegree + 1)
            {
                int i = cycle.First();
                int j = cycle.Last();
                if (AdjacencyMatrix[i, j] > 0)
                {
                    cycle.Add(i);
                    return cycle;
                }
            }

            if (cycle.Count > 0) //"cycle" jest narazie ścieżką po kolejnych wierzchołkach (połączonych krawędziami)
            {
                for (int i = 0; i < n; i++)
                {
                    //szukamy wierzchołka który :
                    //- jest połączony z ostatnim wierzchołkiem na liśćie
                    //- nie wystąpił na naszej ścieżce

                    if (AdjacencyMatrix[i, cycle.Last()] > 0 && !cycle.Exists(x => x == i))
                    {
                        cycle.Add(i); //i nie wystąpiło, dodajemy do listy
                        //rekurencja - szukamy kolejnych wierzchołków
                        var currentCycle = FindCNRecursive(cycle, minDegree);
                        //jeśli znajdziemy odpowiedni cykl - zwracamy
                        if (currentCycle.First() == currentCycle.Last())
                            return currentCycle;
                        else //przypadek gdy nie znajdziemy cyklu od tego wierzchołka (z reguły przypadek mostu)
                        {
                            //usuwamy go i rozpoczynamy poszukiwanie od nowego wierzchołka
                            cycle.RemoveAt(0);
                            return FindCNRecursive(cycle, minDegree);
                        }
                    }
                }
            }
            return cycle;
        }

        public static Graph HavelHakimi(List<int> list)
        {
            var n = list.Count;

            var degreesList = new List<List<int>>();
            var vertexList = new List<int>();

            var l = new List<int>(list);

            if (l.Sum() % 2 == 1)
                return null;

            while (true)
            {
                l = l.OrderByDescending(x => x).ToList();

                if (l[0] == 0 && l[l.Count - 1] == 0)
                {
                    degreesList.Reverse();
                    vertexList.Reverse();
                    return CreateGraphFromDegreesList(degreesList, vertexList, l.Count);
                }

                var vertex = l[0];
                vertexList.Add(vertex);
                l.RemoveAt(0);

                if (vertex > l.Count)
                    return null;

                for (int i = 0; i < vertex; i++)
                {
                    l[i] -= 1;
                    if (l[i] < 0)
                        return null;
                }

                degreesList.Add(l);
            }
        }

        private static Graph CreateGraphFromDegreesList(List<List<int>> degreesList, List<int> vertexList, int initialSize)
        {
            Graph graph = new Graph(initialSize);
            var n = degreesList.Count; //vertexList.Count

            for (int i = 0; i < n; i++)
            {
                graph.AddVertex();
                var newVertexId = graph.AdjacencyMatrix.GetLength(0) - 1;

                for (int j = 0; j < vertexList[i]; j++)
                {
                    var currentVertexId = graph.FindVertexIndexWithDegree(degreesList[i][j]);
                    graph.AddEdge(newVertexId, currentVertexId);
                }
            }
            return graph;
        }

        public override string ToString()
        {
            string s = "Macierz sąsiedztwa\n  ";
            int n = AdjacencyMatrix.GetLength(0);
            foreach (var name in Names)
                s += name + " ";
            s += "\n";
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (j == 0)
                        s += Names[i] + " ";
                    s += AdjacencyMatrix[i, j] + " ";
                }
                s += "\n";
            }
            s += "\nLista sąsiedztwa\n";

            //s += ArrayHelper.ToString(AdjacencyList, Names);
            s += ArrayHelper.ToString(AdjacencyList);

            s += "\nStopnie \n";
            foreach (var name in Names)
                s += name + " ";
            s += "\n";
            foreach (var degree in Degrees)
                s += degree + " ";
            return s;
        }

        public string AdditionalInfo()
        {
            string s = "";
            s += "\nStopień maksymalny: " + FindMaxDegree();
            s += "\nStopień minimalny: " + FindMinDegree();
            s += "\nIlość stopni parzystych: " + CountEvenDegrees();
            s += "\nIlość stopni nieparzystych: " + CountOddDegrees();
            s += "\nStopnie posortowane rosnąco: " + ArrayHelper.ToString(DegreesSortedAscending());
            s += "\nStopnie posortowane malejąco: " + ArrayHelper.ToString(DegreesSortedDescending());
            return s;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Tasks_2.Helper;
using Tasks_2.Interface;
using Tasks_2.Struct;

namespace Tasks_2
{
    public class DirectedGraph : IGraph
    {
        public int[,] AdjacencyMatrix { get; private set; }
        public List<List<int>> AdjacencyList { get; private set; }
        public List<Edge> Edges { get; private set; }
        public List<int> Degrees { get; private set; }

        private Dictionary<Tuple<int, int>, Edge> EdgesDictionary { get; set; }

        public int VCount
        {
            get => AdjacencyMatrix != null ? AdjacencyMatrix.GetLength(0) : 0;
            private set { }
        }

        public int ECount
        {
            get => Edges != null ? Edges.Count : 0;
            private set { }
        }

        public DirectedGraph(int n)
        {
            AdjacencyMatrix = new int[n, n];
            CreateAdjacencyList();
            CreateDegreesList();
            CreateEdgesList();
        }

        public DirectedGraph(string fileName)
        {

            if (!File.Exists(fileName))
                throw new FileNotFoundException("Plik (" + fileName + ") z listą krawędzi nie istnieje!");
            var lines = File.ReadAllLines(fileName);
            int n = 0;
            int.TryParse(lines[0].Trim(), out n);
            AdjacencyMatrix = new int[n, n];
            CreateAdjacencyList();
            CreateDegreesList();
            Edges = new List<Edge>();

            for (int i = 1; i < lines.Length; i++)
            {
                var line = lines[i].Split(' ');
                int v = 0, u = 0, weight = 0;
                if (line.Length < 2)
                    throw new FileLoadException("Plik (" + fileName + ") nie zawiera poprawnej listy krawędzi. (Linia " + (i + 1) + " nie ma dwoch liczb odzdzielonych spacjami.");
                if (line.Length == 2)
                {
                    int.TryParse(line[0], out v);
                    int.TryParse(line[1], out u);
                }
                else if (line.Length > 2)
                {
                    int.TryParse(line[0], out v);
                    int.TryParse(line[1], out u);
                    int.TryParse(line[2], out weight);
                }
                if (v >= n || v < 0 || u >= n || u < 0)
                    throw new FileLoadException("Plik (" + fileName + ") nie zawiera poprawnej listy krawędzi. (Linia " + (i + 1) + " zawiera ujemne badz wieksze niz " + n + " liczby.");
                if (!AddEdge(v, u, weight))
                    throw new Exception("Plik (" + fileName + ") nie zawiera grafu prostego.");
            }
        }

        public bool AddEdge(int i, int j, int weight = 0)
        {
            if ((i < 0 || i >= VCount) || (j < 0 || j >= VCount))
                return false;
            if (i == j)
                return false;
            if (AdjacencyMatrix[i, j] == 1 || AdjacencyMatrix[j, i] == 1)
                return false;
            AdjacencyMatrix[i, j] += 1;
            AdjacencyList[i].Add(j);
            AdjacencyList[i].Sort();
            Degrees[j] += 1;
            Edges.Add(new Edge(i, j, weight));
            return true;
        }

        public void AddVertex()
        {
            int[,] NewAdjacencyMatrix = new int[VCount + 1, VCount + 1];
            for (int i = 0; i < VCount; i++)
                for (int j = 0; j < VCount; j++)
                    NewAdjacencyMatrix[i, j] = AdjacencyMatrix[i, j];
            AdjacencyMatrix = NewAdjacencyMatrix;
            Degrees.Add(0);
            AdjacencyList.Add(new List<int>());
        }

        public bool DeleteEdge(int i, int j)
        {
            if ((i < 0 || i >= VCount) || (j < 0 || j >= VCount))
                return false;
            if (AdjacencyMatrix[i, j] == 0)
                return false;
            AdjacencyMatrix[i, j] -= 1;
            AdjacencyList[i].Remove(j);
            Degrees[j] -= 1;
            Edges.Remove(Edges.Find(x => x.U == i && x.V == j));
            return true;
        }

        public bool DeleteVertex(int id)
        {
            if (id < 0 || id >= VCount)
                return false;
            int[,] NewAdjacencyMatrix = new int[VCount - 1, VCount - 1];

            bool iFlag = false;
            for (int i = 0; i < VCount - 1; i++)
            {
                if (i >= id)
                    iFlag = true;

                bool jFlag = false;
                for (int j = 0; j < VCount - 1; j++)
                {
                    if (j >= id)
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
            Edges.RemoveAll(x => x.U == id || x.V == id);
            CreateAdjacencyList();
            CreateDegreesList();
            return true;
        }

        public void CreateAdjacencyList()
        {
            AdjacencyList = new List<List<int>>();
            for (int i = 0; i < VCount; i++)
            {
                AdjacencyList.Add(new List<int>());
                for (int j = 0; j < VCount; j++)
                {
                    if (AdjacencyMatrix[i, j] > 0)
                        AdjacencyList[i].Add(j);
                }
            }
        }

        public void CreateEdgesList()
        {
            Edges = new List<Edge>();
            for (int i = 0; i < VCount; i++)
            {
                for (int j = 0; j < VCount; j++)
                {
                    if (AdjacencyMatrix[i, j] > 0 && !Edges.Exists(x => x == new Edge(i, j)))
                        Edges.Add(new Edge(i, j));
                }
            }
        }

        public void CreateDegreesList()
        {
            Degrees = new List<int>(new int[VCount]);
            for (int i = 0; i < VCount; i++)
                for (int j = 0; j < VCount; j++)
                    if (AdjacencyMatrix[i, j] > 0)
                    {
                        Degrees[i]++;
                        Degrees[j]++;
                    }
        }

        public void CreateEdgesDictionary()
        {
            EdgesDictionary = new Dictionary<Tuple<int, int>, Edge>();
            foreach (var e in Edges)
                EdgesDictionary.Add(Tuple.Create(e.V, e.U), e);
        }

        public override string ToString()
        {
            string s = "Macierz sąsiedztwa (" + AdjacencyMatrix.GetLength(0) + " * " + AdjacencyMatrix.GetLength(1) + ")\n";
            s += ArrayHelper.ToString(AdjacencyMatrix);
            s += "\nStopnie\n";
            s += ArrayHelper.ToString(Degrees, false);
            s += "\nLista sąsiedztwa\n";
            s += ArrayHelper.ToString(AdjacencyList);
            s += "\nLista krawędzi\n";
            s += ArrayHelper.ToString(Edges, true);
            return s;
        }

        //------- Algorytmy -------

        //Sprawdzenie cykliczności grafu (BFS)
        //Źródło: https://www.geeksforgeeks.org/detect-cycle-in-a-directed-graph-using-bfs/
        public bool IsCyclic()
        {
            List<int> queue = new List<int>();
            List<int> degrees = new List<int>(Degrees); //kopia
            for (int i = 0; i < VCount; i++)
                if (Degrees[i] == 0)
                    queue.Add(i);
            int counter = 0;
            while (queue.Count != 0)
            {
                int v = queue.First();
                queue.RemoveAt(0);

                foreach (int neighbour in AdjacencyList[v])
                {
                    degrees[neighbour] -= 1;
                    if (degrees[neighbour] == 0)
                        queue.Add(neighbour);
                }
                counter++;
            }
            if (counter == VCount)
                return false;
            else
                return true;
        }

        //Sprawdzenie spójności grafu (konwersja na graf prosty + sprawdzenie)
        public bool IsConnected()
        {
            return CreateUndirectedGraph().IsConnected();
        }

        //Transponowanie digrafu
        private DirectedGraph CreateTransposedGraph()
        {
            DirectedGraph transposedGraph = new DirectedGraph(VCount);
            foreach (var edge in Edges)
                transposedGraph.AddEdge(edge.U, edge.V, edge.Weight);
            return transposedGraph;
        }

        //Tworzenie grafu nieskierowanego na podstawie grafu skierowanego
        private UndirectedGraph CreateUndirectedGraph()
        {
            UndirectedGraph graph = new UndirectedGraph(VCount);
            foreach (var edge in Edges)
                graph.AddEdge(edge.V, edge.U, edge.Weight);
            return graph;
        }

        //DFS z zapisywaniem wierzchołków do listy/stosu w kolejności pre order
        private void DFSPreOrder(int v, bool[] visited, List<int> list)
        {
            list.Add(v);
            visited[v] = true;
            foreach (var neighbour in AdjacencyList[v])
            {
                if (!visited[neighbour])
                    DFSPreOrder(neighbour, visited, list);
            }
        }

        //DFS z zapisywaniem wierzchołków do listy/stosu w kolejności post order
        private void DFSPostOrder(int v, bool[] visited, List<int> list)
        {
            visited[v] = true;
            foreach (var neighbour in AdjacencyList[v])
                if (!visited[neighbour])
                    DFSPostOrder(neighbour, visited, list);
            list.Add(v);
        }

        //Algorytm Kosaraju - wyznaczanie silnie składowych spójności
        //Źródło: https://www.geeksforgeeks.org/strongly-connected-components/
        public List<List<int>> Kosaraju()
        {
            int n = AdjacencyMatrix.GetLength(0);
            //Lista list wierzchołków należących do silnie składowych spójności
            List<List<int>> SCCs = new List<List<int>>();
            List<int> stack = new List<int>();
            bool[] visited = new bool[n];

            //Wykonaj DFS dla każdego wierzchołka i zapisz w kolejności post order
            for (int i = 0; i < n; i++)
                if (!visited[i])
                    DFSPostOrder(i, visited, stack);

            //Stwórz digraf transponowany
            DirectedGraph transposedGraph = CreateTransposedGraph();
            //Zresetuj tablicę odwiedzonych = ustaw wszędzie false
            visited = new bool[n];
            //Dla każdego wierzchołka na stosie (z DFSPreOrder) wykonaj DFS na digrafie transponowanym i zapisuj w kolejności pre order
            //Każdą silnie składową spójności zapisuj do osobnej listy
            while (stack.Count != 0)
            {
                int i = stack.Last();
                stack.RemoveAt(stack.Count - 1);
                if (!visited[i])
                {
                    List<int> currentSSC = new List<int>();
                    transposedGraph.DFSPreOrder(i, visited, currentSSC);
                    SCCs.Add(currentSSC);
                }
            }
            return SCCs;
        }

        //Znajdowanie najdłuższych ścieżek
        //Źródło: https://www.geeksforgeeks.org/find-longest-path-directed-acyclic-graph/
        private object[] FindLongestPaths(int v = 0)
        {
            //Topologiczna kolejność
            bool[] visited = new bool[VCount];
            List<int> stack = new List<int>();
            for (int i = 0; i < VCount; i++)
                if (!visited[i])
                    DFSPostOrder(i, visited, stack);

            int[] distances = new int[VCount];
            for (int i = 0; i < VCount; i++)
                distances[i] = int.MinValue;
            distances[v] = 0;
            int minCostIndex = stack.First();

            while (stack.Count != 0)
            {
                int u = stack.Last();
                stack.RemoveAt(stack.Count - 1);

                foreach (int neighbour in AdjacencyList[u])
                {
                    if (distances[neighbour] < distances[u] + EdgesDictionary[Tuple.Create(u, neighbour)].Weight)
                        distances[neighbour] = distances[u] + EdgesDictionary[Tuple.Create(u, neighbour)].Weight;
                }
            }
            return new object[] { distances, distances[minCostIndex] };
        }

        public string Jobs()
        {
            if (!IsConnected())
                return "Graf nie jest spójny!";
            if (IsCyclic())
                return "Graf jest cykliczny!";
            //Przed wywołaniem FindLongestPaths() musimy mieć pewność, że słownik krawędzi będzie istnieć
            CreateEdgesDictionary();
            var longestPaths = FindLongestPaths();
            var distances = (int[])longestPaths[0];
            var minCost = (int)longestPaths[1];
            string s = "Minimalny czas realizacji całej pracy: " + minCost + ".\n\n";
            for (int i = 0; i < VCount; i++)
                foreach (var neighbour in AdjacencyList[i])
                {
                    var longestPathFromNeighbour = (int)FindLongestPaths(neighbour)[1];
                    s += "Łuk " + i + " --> " + neighbour + "\n";
                    s += "Najwcześniejszy moment rozpoczęcia pracy: " + distances[i] + ".\n";
                    s += "Najpóźniejszy moment rozpoczęcia pracy: " + (minCost - longestPathFromNeighbour - EdgesDictionary[Tuple.Create(i, neighbour)].Weight) +
                        " (" + minCost + " - " + longestPathFromNeighbour + " - " + EdgesDictionary[Tuple.Create(i, neighbour)].Weight + ").\n\n";
                }
            return s;
        }

    }
}

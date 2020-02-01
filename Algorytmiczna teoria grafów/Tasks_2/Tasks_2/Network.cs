using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Tasks_2.Helper;
using Tasks_2.Struct;

namespace Tasks_2
{
    public class Network
    {
        public int[,] AdjacencyMatrix { get; private set; }
        public List<Edge> Edges { get; private set; }
        public List<List<int>> AdjacencyList { get; private set; }

        public Network(int n)
        {
            AdjacencyMatrix = new int[n, n];
            CreateAdjacencyList();
            CreateEdgesList();
        }

        public Network(Network network)
        {
            AdjacencyMatrix = ArrayHelper.CopyArray(network.AdjacencyMatrix);
            CreateAdjacencyList();
            CreateEdgesList();
        }

        public Network(int vCount, List<Edge> edges, int initCapacity = 1)
        {
            AdjacencyMatrix = new int[vCount, vCount];
            CreateAdjacencyList();
            CreateEdgesList();
            foreach (var edge in edges)
                if (!AddEdge(edge.V, edge.U, initCapacity))
                    throw new Exception("Podana lista krawędzi nie tworzy grafu prostego.");
        }

        public Network(string fileName)
        {

            if (!File.Exists(fileName))
                throw new FileNotFoundException("Plik (" + fileName + ") z listą krawędzi nie istnieje!");
            var lines = File.ReadAllLines(fileName);
            int n = 0;
            int.TryParse(lines[0].Trim(), out n);
            AdjacencyMatrix = new int[n, n];
            CreateAdjacencyList();
            CreateEdgesList();

            for (int i = 1; i < lines.Length; i++)
            {
                var line = lines[i].Split(' ');
                int v = 0, u = 0, capacity = 1;
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
                    int.TryParse(line[2], out capacity);
                }
                if (v >= n || v < 0 || u >= n || u < 0)
                    throw new FileLoadException("Plik (" + fileName + ") nie zawiera poprawnej listy krawędzi. (Linia " + (i + 1) + " zawiera ujemne badz wieksze niz " + n + " liczby.");
                if (!AddEdge(v, u, capacity))
                    throw new Exception("Plik (" + fileName + ") nie zawiera grafu prostego.");
            }
        }

        public int VCount
        {
            get => AdjacencyMatrix != null ? AdjacencyMatrix.GetLength(0) : 0;
            private set { }
        }

        public bool AddEdge(int i, int j, int capacity = 1)
        {
            if ((i < 0 || i >= VCount) || (j < 0 || j >= VCount))
                return false;
            if (i == j)
                return false;
            if (AdjacencyMatrix[i, j] > 0)
                return false;
            AdjacencyMatrix[i, j] = capacity;
            AdjacencyList[i].Add(j);
            AdjacencyList[i].Sort();
            Edges.Add(new Edge(i, j, capacity));
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

        private UndirectedGraph CreateUndirectedGraph()
        {
            UndirectedGraph graph = new UndirectedGraph(VCount);
            foreach (var edge in Edges)
                graph.AddEdge(edge.V, edge.U, edge.Weight);
            return graph;
        }

        public override string ToString()
        {
            string s = "Macierz sąsiedztwa (" + AdjacencyMatrix.GetLength(0) + " * " + AdjacencyMatrix.GetLength(1) + ")\n";
            s += ArrayHelper.ToString(AdjacencyMatrix);

            s += "\nLista sąsiedztwa\n";
            s += ArrayHelper.ToString(AdjacencyList);

            return s;
        }

        //Walidajca

        private bool IsConnected()
        {
            return CreateUndirectedGraph().IsConnected();
        }

        private void DFS(int v, bool[] visited)
        {
            visited[v] = true;
            foreach (var neighbour in AdjacencyList[v])
                if (!visited[neighbour])
                    DFS(neighbour, visited);
        }

        private bool PathExists(int start, int end)
        {
            bool[] visited = new bool[VCount];
            DFS(start, visited);
            return visited[end];
        }

        private bool AreCapacitiesPositive()
        {
            foreach (var e in Edges)
                if (e.Capacity <= 0)
                    return false;
            return true;
        }

        //Algorytmy
        void DFS(int[,] adjacencyMatrix, int s, bool[] visited)
        {
            visited[s] = true;
            for (int i = 0; i < adjacencyMatrix.GetLength(0); i++)
                if (adjacencyMatrix[s, i] > 0 && !visited[i])
                    DFS(adjacencyMatrix, i, visited);
        }

        public bool BFS(int[,] adjacencyMatrix, int s, int t, int[] parent)
        {
            int n = adjacencyMatrix.GetLength(0);
            bool[] visited = new bool[n];
            List<int> queue = new List<int> { s };
            visited[s] = true;
            parent[s] = -1;

            while (queue.Count != 0)
            {
                int u = queue.First();
                queue.RemoveAt(0);

                for (int i = 0; i < n; i++)
                    if (adjacencyMatrix[u, i] > 0 && !visited[i])
                    {
                        queue.Add(i);
                        visited[i] = true;
                        parent[i] = u;
                    }
            }
            return visited[t]; //visited[t] == true
        }

        //Źródło: https://www.geeksforgeeks.org/minimum-cut-in-a-directed-graph/
        public List<object> EdmondsKarp(int source, int sink)
        {
            //zwraca liste obiektow : maxPrzeplyw, minmalnyPrzekroj(zbior lukow), koncowa siec (graf) residualna

            int n = VCount;

            if (sink == source)
                throw new Exception("Źródło nie może być równocześnie ujściem!");
            if (!IsConnected())
                throw new Exception("Graf nie jest spójny!");
            if (!AreCapacitiesPositive())
                throw new Exception("Nie wszystkie przepustowości krawędzi są dodatnie! (>0)");
            if (!PathExists(source, sink))
                throw new Exception("Nie istnieje żadna ścieżka między źródłem a ujściem!");

            int maxFlow = 0;
            List<Edge> edges = new List<Edge>();

            int[,] residualGraph = ArrayHelper.CopyArray(AdjacencyMatrix);
            int[] parent = new int[n];

            while (BFS(residualGraph, source, sink, parent))
            {
                int pathFlow = int.MaxValue;

                for (int v = sink; v != source; v = parent[v])
                    pathFlow = Math.Min(pathFlow, residualGraph[parent[v], v]);

                maxFlow += pathFlow;

                for (int v = sink; v != source; v = parent[v])
                {
                    residualGraph[parent[v], v] -= pathFlow;
                    residualGraph[v, parent[v]] += pathFlow;
                }
            }
            bool[] visited = new bool[n];
            DFS(residualGraph, source, visited);
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    if (visited[i] && !visited[j] && AdjacencyMatrix[i, j] > 0)
                        edges.Add(new Edge(i, j));
            return new List<object> { maxFlow, edges, residualGraph };
        }

        public List<object> EdmondsKarpWithTraits(int source, int sink)
        {
            //zwraca liste obiektow : maxPrzeplyw, minmalnyPrzekroj(zbior lukow), sciezkiPowiekszajace, koncowa macierz przeplywu
            int n = VCount;

            if (sink == source)
                throw new Exception("Źródło nie może być równocześnie ujściem!");
            if (!IsConnected())
                throw new Exception("Graf nie jest spójny!");
            if (!AreCapacitiesPositive())
                throw new Exception("Nie wszystkie przepustowości krawędzi są dodatnie! (>0)");
            if (!PathExists(source, sink))
                throw new Exception("Nie istnieje żadna ścieżka między źródłem a ujściem!");

            int maxFlow = 0; //wartosc maksymalnego przekroju
            List<List<int>> paths = new List<List<int>>(); //sciezki powiekszajace
            List<Edge> edges = new List<Edge>(); //minimalny przekroj (zbior lukow)

            int[,] flowMatrix = new int[n, n]; //macierz przepływów między wierzchołkami (same 0)
            int[] traitValue = new int[n]; //wartosc cechy
            char?[] traitSign = new char?[n]; //znak cechy (+,- lub null jesli jeszcze nieocechowany)
            int[] parent = ArrayHelper.InitArray(n, -1); //kto cechowal dany wierzchołek
            List<int> L = new List<int> { source }; //kolejka BFS
            traitValue[source] = int.MaxValue;
            traitSign[source] = '-';
            while (L.Count != 0)
            {
                int i = L.First();
                L.RemoveAt(0);

                bool atSink = false; //czy znaleźliśmy się na ujściu

                for (int j = 0; j < n; j++) //cechowanie sąsiadów
                {
                    //poruszamy sie zgodnie z orientacja; łuk nienasycony i nieocechowany
                    if ((flowMatrix[i, j] < AdjacencyMatrix[i, j]) && traitSign[j] == null)
                    {
                        int d = AdjacencyMatrix[i, j] - flowMatrix[i, j]; //wartosc przeplywu do wezla i j
                        traitValue[j] = Math.Min(traitValue[i], d);
                        traitSign[j] = '+';
                        parent[j] = i;
                        L.Add(j);
                        if (j == sink)
                        {
                            atSink = true;
                            break;
                        }
                    }
                    //poruszamy sie niezgodnie z orientacją
                    else if ((flowMatrix[j, i] > 0) && traitSign[j] == null)
                    {
                        traitValue[j] = Math.Min(traitValue[i], flowMatrix[j, i]);
                        traitSign[j] = '-';
                        parent[j] = i;
                        L.Add(j);
                        if (j == sink)
                        {
                            atSink = true;
                            break;
                        }
                    }
                }

                if (atSink)
                {
                    List<int> path = new List<int> { sink };

                    for (int v = sink; v != source; v = parent[v])
                    {
                        path.Add(parent[v]);
                        if (traitSign[v] == '+')
                            flowMatrix[parent[v], v] += traitValue[sink];
                        else if (traitSign[v] == '-')
                            flowMatrix[v, parent[v]] -= traitValue[sink];
                    }

                    traitValue = new int[n];
                    traitSign = new char?[n];
                    parent = new int[n];
                    L = new List<int> { source };
                    traitValue[source] = int.MaxValue;
                    traitSign[source] = '-';
                    paths.Add(path);
                }
            }
            List<int> x = new List<int>();
            List<int> vx = new List<int>();
            for (int i = 0; i < n; i++)
            {
                if (traitSign[i] != null) //traitValue[i] != 0
                    x.Add(i);
                else
                    vx.Add(i);
            }
            foreach (int i in x)
                foreach (int j in vx)
                    if (AdjacencyMatrix[i, j] > 0)
                    {
                        edges.Add(new Edge(i, j));
                        maxFlow += AdjacencyMatrix[i, j];
                    }
            return new List<object> { maxFlow, edges, paths, flowMatrix };
        }
    }
}
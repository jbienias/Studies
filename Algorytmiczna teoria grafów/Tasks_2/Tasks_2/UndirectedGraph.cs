using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Tasks_2.Helper;
using Tasks_2.Struct;

namespace Tasks_2
{
    public class UndirectedGraph
    {
        public int[,] AdjacencyMatrix { get; private set; }
        public List<List<int>> AdjacencyList { get; private set; }
        public List<Edge> Edges { get; private set; }
        public List<int> Degrees { get; private set; }

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

        public UndirectedGraph(int n)
        {
            AdjacencyMatrix = new int[n, n];
            CreateAdjacencyList();
            CreateDegreesList();
            CreateEdgesList();
        }

        public UndirectedGraph(UndirectedGraph ug)
        {
            AdjacencyMatrix = ArrayHelper.CopyArray(ug.AdjacencyMatrix);
            CreateAdjacencyList();
            CreateDegreesList();
            CreateEdgesList();
        }

        public UndirectedGraph(int[,] matrix)
        {
            int n = matrix.GetLength(0);
            if (matrix.GetLength(0) != matrix.GetLength(1))
                throw new ArgumentException("Niepoprawny rozmiar macierzy!");

            AdjacencyMatrix = new int[n, n];
            CreateAdjacencyList();
            CreateDegreesList();
            CreateEdgesList();
        }

        public UndirectedGraph(string fileName, bool multiGraph = false)
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
                if (!AddEdge(v, u, weight, multiGraph))
                    throw new Exception("Plik (" + fileName + ") nie zawiera grafu prostego.");
            }
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
                    if (array[i, j] > 1 || array[i, j] != array[j, i])
                        return false;
                    else if (i == j && array[i, j] != 0)
                        return false;
                }
            }
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

        public bool AddEdge(int i, int j, int weight = 0, bool multiGraph = false)
        {
            if ((i < 0 || i >= VCount) || (j < 0 || j >= VCount))
                return false;
            if (i == j)
                return false;
            if (!multiGraph && (AdjacencyMatrix[i, j] == 1 || AdjacencyMatrix[j, i] == 1))
                return false;
            AdjacencyMatrix[i, j] += 1;
            AdjacencyMatrix[j, i] += 1;
            AdjacencyList[i].Add(j);
            AdjacencyList[i].Sort();
            AdjacencyList[j].Add(i);
            AdjacencyList[j].Sort();
            Degrees[i] += 1;
            Degrees[j] += 1;
            Edges.Add(new Edge(i, j, weight));
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
            CreateDegreesList();
            CreateAdjacencyList();
            return true;
        }

        public bool DeleteEdge(int i, int j)
        {
            if ((i < 0 || i >= VCount) || (j < 0 || j >= VCount))
                return false;
            if (AdjacencyMatrix[i, j] == 0 || AdjacencyMatrix[j, i] == 0)
                return false;
            AdjacencyMatrix[i, j] -= 1;
            AdjacencyMatrix[j, i] -= 1;
            AdjacencyList[i].Remove(j);
            AdjacencyList[j].Remove(i);
            Degrees[i] -= 1;
            Degrees[j] -= 1;
            Edges.Remove(new Edge(i, j));
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
                    if (AdjacencyMatrix[i, j] > 0 && AdjacencyMatrix[j, i] > 0)
                        AdjacencyList[i].Add(j);
                }
            }
        }

        public void CreateDegreesList()
        {
            Degrees = new List<int>(new int[VCount]);
            for (int i = 0; i < VCount; i++)
                for (int j = 0; j < VCount; j++)
                    if (AdjacencyMatrix[i, j] > 0)
                        Degrees[j]++;
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
        //Źródło: https://www.geeksforgeeks.org/detect-cycle-in-an-undirected-graph-using-bfs/
        public bool IsCyclic()
        {
            bool isCyclic = false;
            int[] parent = new int[VCount];
            bool[] visited = new bool[VCount];
            for (int i = 0; i < VCount; i++)
                parent[i] = -1;
            List<int> queue = new List<int>();
            for (int i = 0; i < VCount; i++)
            {
                if (!visited[i])
                {
                    visited[i] = true;
                    queue.Add(i);

                    while (queue.Count != 0)
                    {
                        int u = queue[0];
                        queue.RemoveAt(0);
                        foreach (var neighbour in AdjacencyList[u])
                        {
                            if (!visited[neighbour])
                            {
                                visited[neighbour] = true;
                                queue.Add(neighbour);
                                parent[neighbour] = u;
                            }
                            else if (parent[u] != neighbour)
                            {
                                isCyclic = true;
                                break;
                            }
                        }
                    }

                }
            }
            return isCyclic;
        }

        //Sprawdzenie spójności grafu (BFS)
        public bool IsConnected()
        {
            bool[] visited = new bool[VCount];
            List<int> queue = new List<int>();

            visited[0] = true;
            queue.Add(0);
            while (queue.Count != 0)
            {
                int u = queue[0];
                queue.RemoveAt(0);
                foreach (var neighbour in AdjacencyList[u])
                {
                    if (!visited[neighbour])
                    {
                        visited[neighbour] = true;
                        queue.Add(neighbour);
                    }
                }
            }
            for (int i = 0; i < VCount; i++)
                if (!visited[i])
                    return false;

            return true;
        }

        //Wyznaczenie drzewa spinającego DFS (wersja iteracyjna)
        //Źródło: https://inf.ug.edu.pl/~hanna/grafy/skrypt_grafy.pdf
        public List<Edge> DFSSpanningTreeIterative()
        {
            List<Edge> edges = new List<Edge>();
            List<int> stack = new List<int>();
            var visited = new bool[VCount];
            stack.Add(0);
            visited[0] = true;

            while (stack.Count != 0)
            {
                int v = stack.Last();
                //Flaga jest zapalana, jeśli v ma sąsiada
                bool flag = false;

                for (int i = 0; i < AdjacencyList[v].Count; i++)
                {
                    if (!visited[AdjacencyList[v][i]])
                    {
                        flag = true;
                        visited[AdjacencyList[v][i]] = true;
                        stack.Add(AdjacencyList[v][i]);
                        edges.Add(new Edge(v, AdjacencyList[v][i]));
                        break;
                    }
                }
                //Jesli v nie ma już sąsiadów - zdejmujemy ze stosu
                if (!flag)
                    stack.RemoveAt(stack.Count - 1);
            }
            return edges;
        }

        //Wyznaczenie drzewa spinającego DFS (wersja rekurencyjna)
        //Źródło: https://eduinf.waw.pl/inf/alg/001_search/0125.php
        private void DFSSpanningTreeRecursiveUtil(int v, bool[] visited, List<Edge> edges)
        {
            foreach (int neighbour in AdjacencyList[v])
            {
                if (!visited[neighbour])
                {
                    visited[neighbour] = true;
                    edges.Add(new Edge(v, neighbour));
                    DFSSpanningTreeRecursiveUtil(neighbour, visited, edges);
                }
            }
        }

        public List<Edge> DFSSpanningTreeRecursive(int v = 0)
        {
            //Chyba musi byc spójny?
            if (!IsConnected())
            {
                Console.WriteLine("Graf nie jest spójny!");
                return new List<Edge>();
            }
            bool[] visited = new bool[VCount];
            List<Edge> edges = new List<Edge>();
            visited[v] = true;

            for (int i = 0; i < VCount; i++)
            {
                if (!visited[i])
                {
                    visited[i] = true;
                    edges.Add(new Edge(v, i));
                    DFSSpanningTreeRecursiveUtil(i, visited, edges);
                }
            }
            return edges;
        }

        //Wyznaczenie składowych spójności
        //Źródło: https://eduinf.waw.pl/inf/alg/001_search/0129.php
        public List<List<int>> Components()
        {
            List<List<int>> components = new List<List<int>>();
            int[] C = new int[VCount];
            int componentCount = 0;
            var stack = new List<int>();

            for (int i = 0; i < VCount; i++)
            {
                if (C[i] > 0)
                    continue;

                componentCount++;
                stack.Add(i);
                C[i] = componentCount;

                while (stack.Count != 0)
                {
                    int v = stack.Last();
                    stack.RemoveAt(stack.Count - 1);

                    for (int u = 0; u < AdjacencyList[v].Count; u++)
                    {
                        if (!(C[AdjacencyList[v][u]] > 0))
                        {
                            stack.Add(AdjacencyList[v][u]);
                            C[AdjacencyList[v][u]] = componentCount;
                        }
                    }
                }
            }

            for (int i = 0; i < componentCount; i++)
            {
                components.Add(new List<int>());
                for (int j = 0; j < VCount; j++)
                {
                    if (C[j] == (i + 1))
                        components[i].Add(j);
                }
            }
            return components;
        }

        //Algorytm Kruskala (minimalne drzewo rozpinające)
        //Źródło: https://inf.ug.edu.pl/~hanna/md/skrypt_okl_full.pdf CTRL+F Kruskal
        public List<Edge> Kruskal()
        {
            //Chyba musi byc spójny?
            if (!IsConnected())
            {
                Console.WriteLine("Graf nie jest spójny!");
                return new List<Edge>();
            }
            //T:= (V,E'), gdzie E' = 0
            //nowy graf tymczasowy z n wierzchołkami, bez krawędzi
            UndirectedGraph graph = new UndirectedGraph(VCount);

            //Lista krawędzi wynikowych (to co algorytm zwraca)
            List<Edge> edges = new List<Edge>();

            //Posortowane krawędzie grafu G w kolejności niemalejących wag
            Edges = Edges.OrderBy(x => x.Weight).ToList();

            //Dla każdej krawędzi
            foreach (var e in Edges)
            {
                //Dodajemy do grafu tymczasowego kolejną krawędź
                graph.AddEdge(e.V, e.U);
                //Jeśli ta krawędź utworzyłą cykl w grafie tymczasowym to usuń krawędź i kontynuuj
                //W przeciwnym przypadku (krawędź nie utworzyła cyklu) - dodaj krawędź do listy krawędzi wynikowych
                if (graph.IsCyclic())
                    graph.DeleteEdge(e.V, e.U);

                else
                    edges.Add(e);
            }
            return edges;
        }

        public bool IsBipartiteGraph(bool?[] color = null)
        {
            if (color == null)
                color = new bool?[VCount]; //null - niepokolorowany, true - kolor niebieski, false - kolor czerwony
            bool[] visited = new bool[VCount];
            List<int> queue = new List<int>();
            for (int i = 0; i < VCount; i++)
            {
                if (color[i] != null)
                    continue;
                color[i] = true; //kolorujemy najpierw niebieskim
                queue.Add(i);
                while (queue.Count != 0)
                {
                    int v = queue.First();
                    queue.RemoveAt(0);
                    foreach (var neighbour in AdjacencyList[v])
                    {
                        if (color[v] == color[neighbour])
                            return false;
                        if (color[neighbour] != null)
                            continue;
                        color[neighbour] = !color[v];

                        queue.Add(neighbour);
                    }
                }
            }
            return true;
        }

        //Algorytmy 17.12.19

        //Źródło: https://eduinf.waw.pl/inf/alg/001_search/0147.php
        public void MaximumMatching() //Znajdowanie maksymalnych skojarzeń za pomocą wyznaczania maksymalnego przepływu
        {
            bool?[] color = new bool?[VCount];
            if (!IsBipartiteGraph(color))
                throw new Exception("Graf nie jest dwudzielny!");

            //Podziały wierzchołków po dwukolorowaniu:
            List<int> blue = new List<int>();
            List<int> red = new List<int>();
            for (int i = 0; i < VCount; i++)
                if (color[i] == true)
                    blue.Add(i);
                else if (color[i] == false)
                    red.Add(i);
            Console.WriteLine("Wierzchołki niebieskie: " + ArrayHelper.ToString(blue));
            Console.WriteLine("Wierzchołki czerwone: " + ArrayHelper.ToString(red) + "\n");

            //Utworzenie sieci na podstawie grafu prostego o n+2 wierzchołkach (+2 dla source (s) i sink (t))
            //source - wierzcholek o przedostanim indeksie
            //sink - wierzcholek o ostatnim indeksie
            //wszystkie krawedzie w sieci beda mialy wage 1
            int n = VCount + 2;
            int source = n - 2;
            int sink = n - 1;
            var edgesBlueToRed = new List<Edge>();
            foreach (var edge in Edges)
            {
                if (color[edge.V] == true && color[edge.U] == false)
                    edgesBlueToRed.Add(new Edge(edge.V, edge.U));
                else
                    edgesBlueToRed.Add(new Edge(edge.U, edge.V));
            }
            Network network = new Network(n, edgesBlueToRed, 1);

            //Tworzymy krawedzie w sieci: source -> niebieskie, czerwone -> sink (wszystkie krawedzie w sieci o wadze 1)
            for (int i = 0; i < VCount; i++)
                if (color[i] == true) //niebieski
                    network.AddEdge(source, i, 1);
                else if (color[i] == false) //czerwony
                    network.AddEdge(i, sink, 1);

            List<Edge> edges = new List<Edge>(); //lista krawędzi skojarzonych

            List<object> edmondsKarpResults = network.EdmondsKarpWithTraits(source, sink);
            int[,] flowMatrix = (int[,])edmondsKarpResults[3];

            /*
            Console.WriteLine("Źródło: " + source);
            Console.WriteLine("Ujście: " + sink);
            Console.WriteLine("Maks. przepływ:" + edmondsKarpResults[0] + "\n");
            Console.WriteLine("Minimalny przekrój:\n" + ArrayHelper.ToString((List<Edge>)edmondsKarpResults[1]) + "\n");
            Console.WriteLine("Ścieżki powiększające:\n" + ArrayHelper.ToString((List<List<int>>)edmondsKarpResults[2]) + "\n");
            */

            Console.WriteLine("Skojarzenie");
            for (int i = 0; i < VCount; i++) //VCount = (n-2) (nie interesuja nas zrodlo i ujscie)
                for (int j = 0; j < VCount; j++)
                    if (flowMatrix[i, j] == 1)
                    {
                        Console.WriteLine(new Edge(i, j));
                        edges.Add(new Edge(i, j));
                    }
            Console.WriteLine("\n" + "Ilość niebieskich wierzchołków: " + blue.Count);
            Console.WriteLine("Ilość czerwonych wierzchołków: " + red.Count + "\n");
            return;
        }

        public int DFSCount(int v, bool[] visited)
        {
            int count = 1;
            visited[v] = true;
            foreach (var neighbour in AdjacencyList[v])
                if (!visited[neighbour])
                    count = count + DFSCount(neighbour, visited);
            return count;
        }

        //Cykl Eulera - Algorytm Fleury'ego
        //https://pl.wikipedia.org/wiki/Algorytm_Fleury%E2%80%99ego
        //https://www.geeksforgeeks.org/fleurys-algorithm-for-printing-eulerian-path/

        private bool AreDegreesDivisibleByTwo()
        {
            for (int i = 0; i < Degrees.Count; i++)
            {
                if (Degrees[i] % 2 == 1)
                    return false;
            }
            return true;
        }

        public List<Edge> EulerCycle(int u = 0)
        {
            if (!AreDegreesDivisibleByTwo())
                throw new Exception("Nie wszystkie wierzchołki w grafie są parzystego stopnia!");
            if (!IsConnected())
                throw new Exception("Graf nie jest spójny!");
            List<Edge> cycle = new List<Edge>();
            Fleury(u, cycle);
            return cycle;
        }

        public bool IsValidEdge(int u, int v)
        {
            //v jest jedynym sąsiadem u
            if (AdjacencyList[u].Count == 1)
                return true;
            else
            {
                //Liczymy wierzchołki osiągalne z u
                int countBeforeDelete = DFSCount(u, new bool[VCount]);
                DeleteEdge(u, v);
                //Liczymy wierzchołki osiągalne z u po usunięciu krawędzi u-v
                int countAfterDelete = DFSCount(u, new bool[VCount]);
                AddEdge(u, v, 0, true);
                //Jesli ilosc wierzcholkow przed usunieciem jest wieksza niz po usunieciu to u-v jest mostem
                if (countBeforeDelete > countAfterDelete)
                    return false;
                else
                    return true;
            }
        }

        private void Fleury(int u, List<Edge> cycle)
        {
            for (int i = 0; i < AdjacencyList[u].Count; i++)
            {
                int v = AdjacencyList[u][i];

                //Jesli u ma tylko jednego sąsiada (v) - nie mamy wyjścia - idziemy tą drogą
                //Jeśli v ma więcej sąsiadów niż tylko u to nie jest to most
                if (IsValidEdge(u, v))
                {
                    //Console.WriteLine("Dodaję krawędź (" + u + " -> " + v + ") do cyklu.");
                    cycle.Add(new Edge(u, v));
                    DeleteEdge(u, v);
                    Fleury(v, cycle);
                }
            }
        }
    }
}

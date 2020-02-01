using System.Collections.Generic;
using Tasks_2.Struct;

namespace Tasks_2.Interface
{
    interface IGraph
    {
        int[,] AdjacencyMatrix { get; }
        List<List<int>> AdjacencyList { get; }
        List<Edge> Edges { get; }
        List<int> Degrees { get; }
        int VCount { get; }
        int ECount { get; }
        void AddVertex();
        bool DeleteVertex(int index);
        bool AddEdge(int i, int j, int weight = 0);
        bool DeleteEdge(int i, int j);
        void CreateAdjacencyList();
        void CreateEdgesList();
        void CreateDegreesList();
        bool IsCyclic();
        bool IsConnected();
    }
}

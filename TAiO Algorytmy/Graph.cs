using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAiO_Algorytmy
{
    public class Graph
    {
        public int this[int i, int j] => _adjacencyMatrix[i, j];
        public int Size => _adjacencyMatrix.GetLength(0);
        private int[,] _adjacencyMatrix;

        public int[,] AdjacencyMatrix
        {
            get { return _adjacencyMatrix; }
        }
        private List<Edge> Edges=new List<Edge>();
        public Graph(int[,] matrix)
        {
            if (!IsAdjacencyMatrixCorrect(matrix))
                throw new ArgumentException("Invalid input matrix");
            _adjacencyMatrix = matrix;
            for (var i = 0; i < Size; i++)
                for (var j = 0; j < i; j++)
                    if (_adjacencyMatrix[i, j]==1)
                        Edges.Add(new Edge(i, j));
        }
        public Graph(int size)
        {
            _adjacencyMatrix = new int[size, size];
        }
        public int CountEdges()
        {
            var count = 0;
            for (var i = 0; i < Size; i++)
                for (var j = 0; j < i; j++)
                    if (_adjacencyMatrix[i, j]==1) count++;
            return count;
        }
        public bool IsEdge(int i, int j)
        {
            
            return this[i, j]==1 ? true : false ;
        }
        private void DeleteVertex(int index)
        {
            var newSize = Size - 1;
            var newMatrix = new int[newSize, newSize];
            int iPrim = 0, jPrim = 0;
            for (var i = 0; i < Size; i++)
            {
                if (i == index) continue;
                for (var j = 0; j < Size; j++)
                {
                    if (j == index) continue;
                    newMatrix[iPrim, jPrim] = _adjacencyMatrix[i, j];
                    jPrim++;
                }

                jPrim = 0;
                iPrim++;
            }
            _adjacencyMatrix = newMatrix;
        }

        public static bool IsAdjacencyMatrixCorrect(int[,] matrix)
        {
            if (matrix.GetLength(0) != matrix.GetLength(1))
                return false;
            for (var i = 0; i < matrix.GetLength(0); i++)
                for (var j = i; j < matrix.GetLength(1); j++)
                    if (j == i && matrix[i, j]==1) // loops
                        return false;
                    else if (matrix[i, j] != matrix[j, i])
                        return false;
            return true;
        }
       

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            if (Size <= 10)
            {
                stringBuilder.AppendLine("Adjacency matrix:");
                for (var i = 0; i < Size; i++)
                {
                    for (var j = 0; j < Size; j++)
                    {
                        stringBuilder.Append($"{_adjacencyMatrix[i, j]} ");
                    }

                    stringBuilder.AppendLine();
                }

                stringBuilder.AppendLine();
            }

            var edgeCount = CountEdges();
            stringBuilder.AppendLine($"Number of vertices in graph: {Size}");
            stringBuilder.AppendLine($"Number of edges in graph: {edgeCount}");
            if (edgeCount <= 20)
            {
                stringBuilder.AppendLine($"List of edges: ");
                foreach (var edge in Edges)
                {
                    stringBuilder.AppendLine($"<{edge.v1},{edge.v2}>");
                }
            }

            return stringBuilder.ToString();
        }
    }
}

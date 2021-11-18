using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAiO_Algorytmy
{
    public class Graph
    {
        public int this[int i, int j]
        {
            get { return _adjacencyMatrix[i, j]; }
            set { _adjacencyMatrix[i, j] = this[i, j]; }
        }
        public int Size => _adjacencyMatrix.GetLength(0);
        private int[,] _adjacencyMatrix;
        private string _graphName;

        public int[,] AdjacencyMatrix
        {
            get { return _adjacencyMatrix; }
        }
        private List<Edge> Edges = new List<Edge>();
        public int EdgeSize => Edges.Count;
        public Edge this[int index]
        {
            get => Edges[index];
        }

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
        public Graph(int[,] matrix, string name)
        {
            this._graphName = name;
            if (!IsAdjacencyMatrixCorrect(matrix))
                throw new ArgumentException("Invalid input matrix");
            _adjacencyMatrix = matrix;
            for (var i = 0; i < Size; i++)
                for (var j = 0; j < i; j++)
                    if (_adjacencyMatrix[i, j] == 1)
                        Edges.Add(new Edge(i, j));
        }
        public Graph(int size)
        {
            _adjacencyMatrix = new int[size, size];
        }
        public bool IsEdge(int i, int j)
        {           
            return this[i, j]==1 ? true : false ;
        }

        public static bool IsAdjacencyMatrixCorrect(int[,] matrix)
        {
            if (matrix.GetLength(0) != matrix.GetLength(1))
                return false;
            for (var i = 0; i < matrix.GetLength(0); i++)
                for (var j = i; j < matrix.GetLength(1); j++)
                    if (j == i && matrix[i, j]==1)
                        return false;
                    else if (matrix[i, j] != matrix[j, i])
                        return false;
            return true;
        }
       

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if(this._graphName!=null)
                sb.Append($"Verticles from Graph {this._graphName}:\n");
            else
                sb.Append("Verticles from Graph:\n");
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                        sb.Append(AdjacencyMatrix[i, j] + " ");
                }
                sb.Append("\n");
            }
            sb.Append("\n");
    
            return sb.ToString();
        }
    }
}

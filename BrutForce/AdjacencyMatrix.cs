using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrutForce
{
    class AdjacencyMatrix
    {
        public int Size { get; set; }

        public int[][] matrix { get; set; }
        public int EdgeNumber;
        public AdjacencyMatrix(int[][] graph)
        {
            InitializeMatrix(graph);
            Size = graph.Length;
            EdgeNumber = matrix.SelectMany(item => item).Sum();
        }
        public AdjacencyMatrix(int[,] graph)
        {
            var len = graph.GetLength(0);
            matrix = new int[len][];
            for(int i = 0; i < len; i++)
            {
                matrix[i] = new int[len];
                for(int y = 0; y < len; y++)
                {
                    matrix[i][y] = graph[i, y];
                }
            }
            Size = matrix.Length;
            EdgeNumber = matrix.SelectMany(item => item).Sum();
        }
        private void InitializeMatrix(int[][] graph)
        {

            int[][] graphcopy = new int[graph.Length][];
            for (int i = 0; i < graph.Length; i++)
            {
                graphcopy[i] = new int[graph.Length];
                graph[i].CopyTo(graphcopy[i], 0);
            }

            matrix = graphcopy;
        }

        public void SwapColumn(int columnA, int columnB)
        {
            int[] colB = matrix.Select(row => row[columnB]).ToArray();

            for (int i = 0; i < matrix.Length; i++)
            {
                matrix[i][columnB] = matrix[i][columnA];
                matrix[i][columnA] = colB[i];
            }
        }

        public void SwapRow(int rowA, int rowB)
        {
            int[] temp = matrix[rowB];
            matrix[rowB] = matrix[rowA];
            matrix[rowA] = temp;
        }
        public  void Swap(int[] list, int index1, int index2)
        {
            int tmp = list[index1];
            list[index1] =list[index2];
            list[index2] = tmp;
        }

        public AdjacencyMatrix GetSubMatrix(int startIndexX, int startIndexY, int size)
        {
            int[][] subGraph = new int[size][];
            for(int i=0;i<size;i++)
            {
                subGraph[i] = new int[size];
            }
            for (int i = startIndexX; i < startIndexX + size; i++)
            {
                for (int j = startIndexY; j < startIndexY + size; j++)
                {
                    subGraph[i - startIndexX][j - startIndexY] = matrix[i][j];
                }
            }

            return new AdjacencyMatrix(subGraph);
        }
        public  void SortMatrix()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if(vertexDegree(i)>vertexDegree(j))
                    {
                        SwapColumn(i, j);
                        SwapRow(i, j);
                    }
                }
            }
        }
        private int vertexDegree(int n)
        {
            int sum = 0;
            for (int x = 0; x < Size; x++)
            {
                sum += matrix[n][x];
                sum += matrix[x][n];
            }
            return sum;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
         
           
                sb.Append("Verticles from Graph:\n");
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    sb.Append(matrix[i][j] + " ");
                }
                sb.Append("\n");
            }
            sb.Append("\n");

            return sb.ToString();
        }
    }


}


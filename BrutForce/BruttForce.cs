using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAiO_Algorytmy;

namespace BrutForce
{
    public static class BruttForce
    {
        public static AdjacencyMatrix MyBrutForce(AdjacencyMatrix A, AdjacencyMatrix B)
        {
            if (A.Size < B.Size)
            {
                AdjacencyMatrix tmp = B;
                B = A;
                A = tmp;
            }
            PermutationGenerator permu = new PermutationGenerator(A.Size);

            AdjacencyMatrix biggestSubGraph = null;
            int[] order;
            int maxCommonEdges = 0;
            for (int i = 0; i < permu.permutations.Length; i++)
            {
                order = permu.permutations[i].Clone() as int[];
                AdjacencyMatrix M = GetMatrixfromPer(permu.permutations[i], A);
                AdjacencyMatrix subMatrix = GetSubMatrix(B.Size,M.matrix);
                AdjacencyMatrix commonMatrix = CommonMatrix(subMatrix, B);
                commonMatrix.UpdateEdges();
                if (commonMatrix.EdgeNumber > maxCommonEdges)
                {
                    maxCommonEdges = commonMatrix.EdgeNumber;
                    biggestSubGraph = commonMatrix;
                    biggestSubGraph.VerticeOrder = order.Clone() as int[]; ;
                }
            }
            return biggestSubGraph;
        }
        public static AdjacencyMatrix MyBrutForceApproximate(AdjacencyMatrix A, AdjacencyMatrix B)
        {
            if (A.Size < B.Size)
            {
                AdjacencyMatrix tmp = B;
                B = A;
                A = tmp;
            }
            A.SortMatrix();
            B.SortMatrix();
            AdjacencyMatrix subMatrix = GetSubMatrix(B.Size, A.matrix);
            AdjacencyMatrix commonMatrix = CommonMatrix(subMatrix, B);
            commonMatrix.UpdateEdges();
            return commonMatrix;
        }
        public static  AdjacencyMatrix GetSubMatrix(int size, int[][] mat)
        {
            Array.Resize(ref mat, size);
            for(int i=0;i<mat.Length;i++)
            {
                Array.Resize(ref mat[i], size);
            }
            return new AdjacencyMatrix(mat);

        }


        public static AdjacencyMatrix GetMatrixfromPer(int[] list, AdjacencyMatrix matPer)
        {
            AdjacencyMatrix newMatrix = new AdjacencyMatrix(matPer.matrix);
            for (int j = 0; j < list.Length; j++)
            {
                for (int i = 0; i < list.Length; i++)
                {
                    if (list[i] != i)
                    {
                        newMatrix.SwapColumn(list[i], i);
                        newMatrix.SwapRow(list[i], i);
                        newMatrix.Swap(list, list[i], i);
                    }
                }
            }
            return newMatrix;
        }
        public static AdjacencyMatrix CommonMatrix(AdjacencyMatrix A, AdjacencyMatrix B)
        {
            for (int i = 0; i < A.Size; i++)
            {
                for (int j = 0; j < A.Size; j++)
                {
                    if ((A.matrix[i][j] == 1) && (B.matrix[i][j] == 0))
                        A.matrix[i][j] = 0;
                }
            }
            return A;
        }
    }
}

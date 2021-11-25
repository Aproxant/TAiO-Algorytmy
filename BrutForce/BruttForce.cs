using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAiO_Algorytmy;

namespace BrutForce
{
    static class BruttForce
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
            int maxCommonEdges = 0;

            for (int i = 0; i < permu.permutations.Length; i++)
            {
                AdjacencyMatrix M = GetMatrixfromPer(permu.permutations[i], A);
                for (int x = 0; x <= M.Size - B.Size; x++)
                {
                    for (int y = 0; y <= M.Size - B.Size; y++)
                    {
                        AdjacencyMatrix subMatrix = M.GetSubMatrix(x, y, B.Size);
                        AdjacencyMatrix commonMatrix =CommonMatrix(subMatrix, B);
                        commonMatrix.UpdateEdges();
                        if (commonMatrix.EdgeNumber > maxCommonEdges)
                        {
                            maxCommonEdges = commonMatrix.EdgeNumber;
                            biggestSubGraph = commonMatrix;
                        }
                    }
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
            AdjacencyMatrix biggestSubGraph = null;
            int maxCommonEdges = 0;
            for (int x = 0; x <= A.Size - B.Size; x++)
            {
                for (int y = 0; y <= A.Size - B.Size; y++)
                {
                    AdjacencyMatrix subMatrix = A.GetSubMatrix(x, y, B.Size);
                    AdjacencyMatrix commonMatrix = CommonMatrix(subMatrix, B);
                    commonMatrix.UpdateEdges();
                    if (commonMatrix.EdgeNumber > maxCommonEdges)
                    {
                        maxCommonEdges = commonMatrix.EdgeNumber;
                        biggestSubGraph = commonMatrix;
                    }
                }
            }
            return biggestSubGraph;
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

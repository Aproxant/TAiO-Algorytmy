using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MAX_McGreg;
using TAiO_Algorytmy;
using BrutForce;

namespace MCS_McGreg
{
    public static class MinBrutt
    {
        public static AdjacencyMatrix MinBruttForce(AdjacencyMatrix A, AdjacencyMatrix B)
        {
            if (A.Size < B.Size)
            {
                AdjacencyMatrix tmp = B;
                B = A;
                A = tmp;
            }
            PermutationGenerator permu = new PermutationGenerator(A.Size);
            AdjacencyMatrix MinSuperGraph = null;
            int maxEdges = int.MaxValue;
            for (int i = 0; i < permu.permutations.Length; i++)
            {
                AdjacencyMatrix M = BruttForce.GetMatrixfromPer(permu.permutations[i], A);
                MaximumNumberOfEdgeGraph(M, B);
                M.UpdateEdges();
                if (M.EdgeNumber < maxEdges)
                {
                    maxEdges = M.EdgeNumber;
                    MinSuperGraph = M;
                }
            }
            return MinSuperGraph;
        }
        private static void MaximumNumberOfEdgeGraph(AdjacencyMatrix A,AdjacencyMatrix B)
        {
            for(int i=0;i<B.matrix.Length;i++)
            {
                for(int j=0;j<B.matrix.Length;j++)
                {
                    if(B.matrix[i][j] == 1)
                        A.matrix[i][j] = 1;
                }
            }
        }
    }
}

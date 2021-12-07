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
    public static class MinimumAppro
    {
        public static AdjacencyMatrix MinimumSuperGraph(AdjacencyMatrix G1, AdjacencyMatrix G2)
        {
            if (G1.Size < G2.Size)
            {
                AdjacencyMatrix tmp = G1;
                G1 = G2;
                G2 = tmp;
            }
            AdjacencyMatrix Mcs;
            Mcs = BruttForce.MyBrutForceApproximate(G1, G2);

            int[] per1 = G1.VerticeOrder.Clone() as int[];
            int[] per2 = G1.VerticeOrder.Clone() as int[];

            int[] per11 = G2.VerticeOrder.Clone() as int[];
            int[] per22 = G2.VerticeOrder.Clone() as int[];
            G1 = BruttForce.GetMatrixfromPer(G1.VerticeOrder, G1);

            G2 = BruttForce.GetMatrixfromPer(G2.VerticeOrder, G2);

            AdjacencyMatrix G1_mcs = SubstractionOfGraphs(G1, Mcs);

            AdjacencyMatrix G2_mcs = SubstractionOfGraphs(G2, Mcs);

            

            AdjacencyMatrix Mcs_G1_emb1 = FindSetOfEmbeddedEdges(Mcs, G1);

            


            AdjacencyMatrix Mcs_G2_emb2 = FindSetOfEmbeddedEdges(Mcs, G2);

            G1_mcs = RevertPer(per1, G1_mcs);
            Mcs_G1_emb1 = RevertPer(per2, Mcs_G1_emb1);

            G2_mcs = RevertPer(per11, G1_mcs);
            Mcs_G2_emb2 = RevertPer(per22, Mcs_G1_emb1);

            List<AdjacencyMatrix> subGraphs = new List<AdjacencyMatrix> { Mcs, Mcs_G1_emb1, G1_mcs, Mcs_G2_emb2, G2_mcs };
            return GenerateSuperGraph(subGraphs);
        }
        public static AdjacencyMatrix RevertPer(int[] list, AdjacencyMatrix matPer)
        {

            AdjacencyMatrix newMatrix = new AdjacencyMatrix(matPer.matrix);
            int[] orginalOrder = new int[list.Length];
            for(int i=0;i<orginalOrder.Length;i++)
            {
                orginalOrder[i] = i;
            }
            for (int j = 0; j < list.Length; j++)
            {
                for (int i = 0; i < list.Length; i++)
                {
                    if (list[i] != orginalOrder[i])
                    {
                        newMatrix.SwapColumn(list[i], orginalOrder[i]);
                        newMatrix.SwapRow(list[i], orginalOrder[i]);
                        newMatrix.Swap(list, list[i], orginalOrder[i]);
                    }
                }
            }
            return newMatrix;
        }
        public static AdjacencyMatrix GenerateSuperGraph(List<AdjacencyMatrix> subGraphs)
        {
            int maxSize = 0;
            foreach(var k in subGraphs)
            {
                if (maxSize < k.Size)
                    maxSize = k.Size;
            }
            int[][] minGraph = new int[maxSize][];
            for (int i = 0; i < maxSize; i++)
                minGraph[i] = new int[maxSize];
            foreach(AdjacencyMatrix sub in subGraphs)
            {
                Console.WriteLine(sub);
                for (int i = 0; i < sub.Size; i++)
                    for (int j = 0; j < sub.Size; j++)
                        if(sub.matrix[i][j]==1)
                            minGraph[i][j] = sub.matrix[i][j];
            }
            return new AdjacencyMatrix(minGraph);
            
        }
        public static AdjacencyMatrix SubstractionOfGraphs(AdjacencyMatrix G1, AdjacencyMatrix G2)
        {
            int[][] mat = new int[G1.Size][];
            for (int i = 0; i < G1.Size; i++)
                mat[i] = new int[G1.Size];
            for (int i = G2.Size; i < G1.Size; i++)
            {
                for (int j = G2.Size; j < G1.Size; j++)
                {
                    mat[i][j] = G1.matrix[i][j];
                }
            }
            return new AdjacencyMatrix(mat);
        }
        public static AdjacencyMatrix FindSetOfEmbeddedEdges(AdjacencyMatrix mcs, AdjacencyMatrix g2)
        {
            int[][] emb = g2.matrix.Clone() as int[][];
            for (int i = 0;i < mcs.Size; i++)
            {
                for(int j=0; j<mcs.Size;j++)
                {
                     emb[i][j] = 0;
                }
            }
            for (int i = mcs.Size; i < g2.Size; i++)
            {
                for (int j = mcs.Size; j < g2.Size; j++)
                {
                    emb[i][j] = 0;
                }
            }
            return new AdjacencyMatrix(emb);
        }

        



    }
}

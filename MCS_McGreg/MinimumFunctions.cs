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
    public static class MinimumFunctions
    {
        public static AdjacencyMatrix MinimumSuperGraph(AdjacencyMatrix G1, AdjacencyMatrix G2,bool appro)
        {
            if (G1.Size < G2.Size)
            {
                AdjacencyMatrix tmp = G1;
                G1 = G2;
                G2 = tmp;
            }
            AdjacencyMatrix Mcs;
            if (appro)
                Mcs = BruttForce.MyBrutForceApproximate(G1, G2);
            else
                Mcs = BruttForce.MyBrutForce(G1, G2);

            AdjacencyMatrix G1_mcs = SubstractionOfGraphs(G1, Mcs);

            AdjacencyMatrix G2_mcs = SubstractionOfGraphs(G2, Mcs);

            AdjacencyMatrix Mcs_G1_emb1 = FindSetOfEmbeddedEdges(Mcs, G1);
            AdjacencyMatrix Mcs_G2_emb2 = FindSetOfEmbeddedEdges(Mcs, G2);
            
            List<AdjacencyMatrix> subGraphs = new List<AdjacencyMatrix> { Mcs, Mcs_G1_emb1, G1_mcs, Mcs_G2_emb2, G2_mcs };
            return GenerateSuperGraph(subGraphs);
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
                for (int i = 0; i < sub.Size; i++)
                    for (int j = 0; j < sub.Size; j++)
                        if(sub.matrix[i][j]==1)
                            minGraph[i][j] = sub.matrix[i][j];
            }
            return new AdjacencyMatrix(minGraph);
            
        }
        public static AdjacencyMatrix SubstractionOfGraphs(AdjacencyMatrix G1, AdjacencyMatrix G2)
        {
            //int[][] mat = G1.matrix.Clone() as int[][];
            int[][] mat = new int[G1.Size][];
            for (int i = 0; i < G1.Size; i++)
                mat[i] = new int[G1.Size];
            for (int i = G2.Size; i < G1.Size; i++)
            {
                for (int j = G2.Size; j < G1.Size; j++)
                {
                    mat[i][j] = G1.matrix[i][j];
                    /*
                    if (G1.matrix[i][j] == G2.matrix[i][j])
                    {
                        mat[i][j] = 0;
                    }*/
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

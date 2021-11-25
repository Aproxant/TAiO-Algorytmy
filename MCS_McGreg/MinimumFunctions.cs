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
        public static AdjacencyMatrix MinimumSuperGraph(AdjacencyMatrix G1, AdjacencyMatrix G2)
        {
            if (G1.Size < G2.Size)
            {
                AdjacencyMatrix tmp = G1;
                G1 = G2;
                G2 = tmp;
            }
            AdjacencyMatrix Mcs = BruttForce.MyBrutForce(G1, G2);

            AdjacencyMatrix G1_mcs = SubstractionOfGraphs(G1, Mcs);

            AdjacencyMatrix G2_mcs = SubstractionOfGraphs(G2, Mcs);

            AdjacencyMatrix Mcs_G1_emb=
            List<Edge> k = FindSetOfEmbeddedEdges(Mcs, G1);
            List<Edge> k2 = FindSetOfEmbeddedEdges(Mcs, G2);
            List<Graph> subGraphs = new List<Graph> { Mcs, G1_mcs, G2_mcs };
            return Mcs;
        }
        public static AdjacencyMatrix SubstractionOfGraphs(AdjacencyMatrix G1, AdjacencyMatrix G2)
        {
            int[][] mat = G1.matrix.Clone() as int[][];

            for (int i = 0; i < G2.Size; i++)
            {
                for (int j = 0; j < G2.Size; j++)
                {
                    if (G1.matrix[i][j] == G2.matrix[i][j])
                    {
                        mat[i][j] = 0;
                    }

                }
            }
            return new AdjacencyMatrix(mat);
        }
        public static AdjacencyMatrix FindSetOfEmbeddedEdges(AdjacencyMatrix mcs, AdjacencyMatrix g2)
        {
            int[][] emb = new int[g2.Size][];
            for (int i = 0; i < emb.Length; i++)
                emb[i] = new int[g2.Size];

            for (int i = 0; i < g2.EdgeSize; i++)
            {
                if (i < mcs.EdgeSize)
                {
                    if (mcs[i].v1 != g2[i].v1 && mcs[i].v2 != g2[i].v2)
                    {
                        EmbeddedSet.Add(new Edge(mcs[i].v1, mcs[i].v2));
                    }
                }
                else
                    break;
            }
            return EmbeddedSet;
        }

        public static Graph GenerateSuperGraph(List<Graph> graphs, List<Edge> emb1, List<Edge> emb2)
        {
            int[,] Min;
            if (graphs[0].Size>=graphs[1].Size && graphs[0].Size>=graphs[2].Size)
                Min = new int[graphs[0].Size, graphs[0].Size];
            else if(graphs[1].Size >= graphs[0].Size && graphs[1].Size >= graphs[2].Size)
                Min = new int[graphs[0].Size, graphs[0].Size];
            else
                Min = new int[graphs[2].Size, graphs[2].Size];
            int counter = 0;
            foreach (var sub in graphs)
            {

                if (counter == 2)
                {
                    foreach (var el in emb1)
                    {
                        Min[el.v1, el.v2] = 1;
                        Min[el.v2, el.v1] = 1;
                    }
                }
                for (int i = 0; i < sub.Size; i++)
                {
                    for (int j = 0; j < sub.Size; j++)
                    {
                        if (sub[i, j] == 1)
                        {
                            Min[i, j] = sub[i, j];
                        }
                    }
                }
            counter += 1;
        }
            return new Graph(Min);
        }



    }
}

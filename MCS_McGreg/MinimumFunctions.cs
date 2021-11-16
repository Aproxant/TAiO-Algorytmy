using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MAX_McGreg;
using TAiO_Algorytmy;

namespace MCS_McGreg
{
    public static class MinimumFunctions
    {
        public static Graph MinimumSuperGraph(State s,Graph G1,Graph G2)
        {
            
            Graph Mcs = TransformGraph(s);
            Graph G1_mcs = SubstractionOfGraphs(G1, Mcs);
            Graph G2_mcs = SubstractionOfGraphs(G2, Mcs);
            List<Edge> k = FindSetOfEmbeddedEdges(Mcs, G1);
            List<Edge> k2 = FindSetOfEmbeddedEdges(Mcs, G2);
            List<Graph> subGraphs = new List<Graph> { Mcs, G1, G2, G1_mcs, G2_mcs };
            return GenerateGraph(subGraphs,k,k2);
        }
        public static Graph GenerateGraph(List<Graph> graphs,List<Edge> emb1,List<Edge> emb2)
        {
            int[,] Min = new int[graphs[1].Size, graphs[1].Size];
            foreach(var sub in graphs)
            {
                for(int i=0;i<sub.Size;i++)
                {
                    for(int j=0;j<sub.Size;j++)
                    {
                        if(sub[i,j]==1)
                        {
                            Min[i, j] = sub[i, j];
                        }                      
                    }
                }
            }
            return new Graph(Min);
        }

        public static Graph TransformGraph(State gr)
        {
            int[,] G1 = new int[gr.G1.GetLength(0), gr.G1.GetLength(1)];

            foreach (var el in gr.correspondingEdges)
            {
                G1[el.Item1.v1, el.Item1.v2] = 1;
                G1[el.Item1.v2, el.Item1.v1] = 1;
            }
            return new Graph(G1);
        }

        public static List<Edge> FindSetOfEmbeddedEdges(Graph mcs,Graph g2)
        {
            List<Edge> EmbeddedSet = new List<Edge>();
            for(int i=0;i<g2.EdgeSize;i++)
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
        public static Graph SubstractionOfGraphs(Graph G1,Graph G2)
        {
            int[,] mat = G1.AdjacencyMatrix.Clone() as int[,];

            for(int i=0;i<G1.Size;i++)
            {
                for(int j=0;j<G2.Size;j++)
                {
                    if (G1.AdjacencyMatrix[i,j] == G2.AdjacencyMatrix[i,j] && G1.AdjacencyMatrix[j,i] == G2.AdjacencyMatrix[j,i])
                    {
                        mat[i, j] = 0;
                    }
                }
            }
            return new Graph(mat);
        }


    }
}

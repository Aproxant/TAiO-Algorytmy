using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrutForce;
using TAiO_Algorytmy;
using TAIO_konsola.utils;

namespace MAX_McGreg
{
  public class MaxMcgregor
    {
        public  void Test()
        {
            DateTime dt = DateTime.Now;


            string file1 = "4_4_A_Hartman.csv";
            string file2 = "4_4_B_Hartman.csv";
            DirectoryInfo dir = new DirectoryInfo(Environment.CurrentDirectory);
            dir = dir.Parent.Parent.Parent;
            string path1 = Path.Combine(dir.FullName, @"Examples/", file1);
            string path2 = Path.Combine(dir.FullName, @"Examples/", file2);
            var G1 = GraphLoader.LoadGraph(path1, "G1");
            var G2 = GraphLoader.LoadGraph(path2, "G2");
            State s = new State(G1.AdjacencyMatrix, G2.AdjacencyMatrix);
            //MyState s = new MyState(G1, G2);
            Console.Write("V+E Solution\n");
            Console.WriteLine(s);
            McGregorE.McGregor(new State(G1.AdjacencyMatrix, G2.AdjacencyMatrix), ref s, true);

            ///Algorithm.McGregor(new MyState(G1, G2), ref s);


        }

        public static void RunExact(Graph G1, Graph G2)
        {
        
            Console.WriteLine("Processing graph...");
            State s = new State(G1.AdjacencyMatrix, G2.AdjacencyMatrix); //bug
            Console.Write("V+E Solution\n");
            McGregorE.McGregor(new State(G1.AdjacencyMatrix, G2.AdjacencyMatrix), ref s, false);
            Console.WriteLine(s.countOfEdges);
            GraphDisplayer.PrintTwoGraphsInRow(new AdjacencyMatrix(G1.AdjacencyMatrix), new AdjacencyMatrix(G2.AdjacencyMatrix));
            //GraphDisplayer.printGraphWithDifferences(new AdjacencyMatrix(G1.AdjacencyMatrix), new AdjacencyMatrix(G2.AdjacencyMatrix), s.);
            Console.WriteLine(s);
        }

        public static int RunExactWithEdgeCount(Graph G1, Graph G2)
        {

            Console.WriteLine("Processing graph...");
            State s = new State(G1.AdjacencyMatrix, G2.AdjacencyMatrix); //bug
            Console.Write("V+E Solution\n");
            McGregorE.McGregor(new State(G1.AdjacencyMatrix, G2.AdjacencyMatrix), ref s, false);
            return s.countOfEdges;

        }
        public static void RunApprox(Graph G1, Graph G2)
        {

            Console.WriteLine("Processing graph...");
            State s = new State(G1.AdjacencyMatrix, G2.AdjacencyMatrix);
            Console.Write("V+E Solution\n");
            McGregorE.McGregor(new State(G1.AdjacencyMatrix, G2.AdjacencyMatrix), ref s, true);
            GraphDisplayer.PrintTwoGraphsInRow(new AdjacencyMatrix(G1.AdjacencyMatrix), new AdjacencyMatrix(G2.AdjacencyMatrix));
            Console.WriteLine(s);

        }

        public static bool LeafOfSearchTree(State s)
        {
            int limit = s.G1.GetLength(0);
            return s.correspondingVerticles.Count >= limit;
        }

        public static int firstNeighbour(State s)
        {
            int v1 = -1;
            bool selected = false;
            bool contains = false;

            if (!(s.correspondingVerticles.Count - s.countOfNullNodes != 0))
            {
                for (int i = 0; i < s.G1.GetLength(0); i++)
                {
                    contains = checkUsed(s, i, 1);
                    if (!contains)
                    {
                        v1 = i;
                        break;
                    }
                }
            }
            else
            {
                //wez sasiada pierwszego lepszego
                foreach (var el in s.correspondingVerticles)
                {
                    if (el.Item2 == -1) continue;
                    for (int i = 0; i < s.G1.GetLength(0); i++)
                    {
                        //jesli jest polaczenie w g1
                        if (isConnectedG1(s, i, el))
                        {
                            contains = checkUsed(s, i, 1);
                            if (!contains)
                            {
                                selected = true;
                            }
                            contains = false;
                        }
                        if (selected)
                        {
                            v1 = i;
                            break;
                        }
                    }
                    if (selected)
                        break;
                }
            }
            return v1;
        }

        public static bool isConnectedG1(State s, int i, (int, int) el)
        {
            if (s.G1[i, el.Item1] == 1)
                return true;
            else
                return false;
        }

        public static bool isConnectedG2(State s, int i, (int,int) el)
        {
            if (s.G2[i, el.Item1] == 1)
                return true;
            else
                return false;
        }

        //bierz pierwszy, rozbic na funkcje (foreache) 
        public static IEnumerable<Tuple<int, int>> nextPair(State s, int v1)
        {

            bool used = false;
            for (int i = 0; i < s.G2.GetLength(0); i++)
            {
                //patrz czy wierzcholek z G2 juz sparowany wczesniej
                used = checkUsed(s, i, 2);
                // jesli nie to sparuj go razem z v1
                if (!used)
                {
                    yield return new Tuple<int, int>(v1, i);
                }


            }
            yield return null;
        }


        public static bool checkUsed(State s, int i, int graph)
        {
            foreach (var el in s.correspondingVerticles)
            {
                if (graph == 1)
                {
                    if (el.Item1 == i)
                    {
                        return true;
                    }                  
                }
                else
                {
                    if (el.Item2 == i)
                    {
                        return true;
                    }
                }
            }
            return false;
        }


    }
}

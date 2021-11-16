using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAiO_Algorytmy;

namespace MAX_McGreg
{
    public class McGregorE
    {
        public static void McGregor(State s, ref State max)
        {
            int count = 0;
            int v1 = Program.firstNeighbour(s);
            if (v1 != -1)
                foreach (var pair in Program.nextPair(s, v1))
                {
                    if (pair == null) break;
                    v1 = pair.Item1;
                  
                    if (isFeasiblePair(s, pair, s.G1, s.G2, ref count)) //positive count guarantees cohesion
                    {

                        s.AddNewPair(pair.Item1, pair.Item2, count);
                        checkMax(s, ref max);
                        if (!Program.LeafOfSearchTree(s))
                            McGregor(s, ref max);
                        s.Backtrack(count);
                    }
                    else
                        Console.WriteLine("");
                    count = 0;
                }
            //case with null node
            s.AddNewPair(v1, -1, 0);
            if (!Program.LeafOfSearchTree(s))
                McGregor(s, ref max);
            s.Backtrack(0);
        }
        private static bool isFeasiblePair(State s, Tuple<int, int> pair, int[,] G1, int[,] G2, ref int countOfEdges)
        {
            int count = 0;
            List<Tuple<Edge, Edge>> listOfEdges = new List<Tuple<Edge, Edge>>();

            foreach ((int v1, int v2) el in s.correspondingVerticles)
                if (el.Item2 != -1)
                {
                    if (G1[el.Item1, pair.Item1] != 0 ^ G2[el.Item2, pair.Item2] != 0)
                        continue; //do not return false, try to add all edges
                    else
                    {
                        if (G1[el.Item1, pair.Item1] == 1)
                        {
                            //we can immediately add to State s
                            s.correspondingEdges.Add((new Edge(el.Item1, pair.Item1), new Edge(el.Item2, pair.Item2)));
                            count++;
                        }
                    }
                    s.countOfTrackedEdges++;
                }
            countOfEdges = count;
            return true;
        }
        private static void checkMax(State s, ref State max)
        {
            if (s.correspondingVerticles.Count - s.countOfNullNodes + s.correspondingEdges.Count
                > max.correspondingVerticles.Count - max.countOfNullNodes + max.correspondingEdges.Count)
                max = new State(s);
        }
    }
}

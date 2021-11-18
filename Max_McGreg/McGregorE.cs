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
        public static void McGregor(State s, ref State max, bool approximate)
        {
            int count = 0;
            int v1 = MaxMcgregor.firstNeighbour(s);
            
            if (v1 != -1)
                findPair(s, v1, ref count, ref max, approximate);

            //case with null node
            s.AddNewPair(v1, -1, 0);

            if (approximate)
            {
                if (!MaxMcgregor.LeafOfSearchTree(s) && !PruningCondition(s, max))
                    McGregor(s, ref max, approximate);
            }
            else
            {
                if (!MaxMcgregor.LeafOfSearchTree(s))
                    McGregor(s, ref max, approximate);
            }

           

            s.Backtrack(0);
        }


        private static void findPair(State s, int v1, ref int count, ref State max, bool approximate)
        {
            foreach (var pair in MaxMcgregor.nextPair(s, v1))
            {
                if (pair == null) break;
                v1 = pair.Item1;

                if (isFeasiblePair(s, pair, ref count))
                {

                    s.AddNewPair(pair.Item1, pair.Item2, count);

                    checkMax(s, ref max);
                    if (approximate)
                    {
                        if (!MaxMcgregor.LeafOfSearchTree(s) && !PruningCondition(s, max))
                            McGregor(s, ref max, approximate);
                    }
                    else
                    {
                        if (!MaxMcgregor.LeafOfSearchTree(s))
                            McGregor(s, ref max, approximate);
                    }


                    //revert to previous state
                    s.Backtrack(count);
                }
                else
                    Console.WriteLine("");
                count = 0;
            }
        }

        private static bool PruningCondition(State s, State max)
        {
            int limit = s.G1.GetLength(0);
            return limit - s.countOfNullNodes <= max.correspondingVerticles.Count - max.countOfNullNodes;
        }

        private static bool isFeasiblePair(State s, Tuple<int, int> pair, ref int countOfEdges)
        {
            int count = 0;
            List<Tuple<Edge, Edge>> listOfEdges = new List<Tuple<Edge, Edge>>();

            foreach ((int v1, int v2) el in s.correspondingVerticles)
                if (el.Item2 != -1)
                {
                    if (s.G1[el.Item1, pair.Item1] != 0 ^ s.G2[el.Item2, pair.Item2] != 0)
                        continue; //do not return false, try to add all edges
                    else
                    {
                        if (s.G1[el.Item1, pair.Item1] == 1)
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

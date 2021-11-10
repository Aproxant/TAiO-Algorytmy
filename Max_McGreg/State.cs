using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAiO_Algorytmy;

namespace MAX_McGreg
{
    class State
    {
        public int[,] G1, G2;
        public int countOfNullNodes;
        public List<(int v1, int v2)> correspondingVerticles = new List<(int v1, int v2)>();
        public List<(Edge edge1, Edge edge2)> correspondingEdges = new List<(Edge edge1, Edge edge2)>();
        public int countOfEdges;
        public int countOfTrackedEdges = 0;
        public State(int[,] G1 = null, int[,] G2 = null)
        {
            this.G1 = G1.Clone() as int[,];
            this.G2 = G2.Clone() as int[,];
        }
        public void Backtrack(int count)
        {
            if (correspondingVerticles.Count > 0)
            {
                if (correspondingVerticles.Last().Item2 == -1)
                    countOfNullNodes--;
                else
                {
                    if (correspondingEdges.Count > 0)
                        for (int i = 0; i < count; i++)
                            correspondingEdges.Remove(correspondingEdges.Last());
                }
                correspondingVerticles.Remove(correspondingVerticles.Last());
                countOfEdges -= count;
            }
        }
        public State(State p)
        {
            G1 = p.G1;
            G2 = p.G2;
            correspondingVerticles = new List<(int v1, int v2)>(p.correspondingVerticles);
            countOfEdges = p.countOfEdges;
            countOfNullNodes = p.countOfNullNodes;
            correspondingEdges = new List<(Edge edge1, Edge edge2)>(p.correspondingEdges);
            countOfTrackedEdges = p.countOfTrackedEdges;
        }

        public void AddNewPair(int n1, int n2, int edgesToAdd)
        {
            if (n2 == -1)
                countOfNullNodes++;
            countOfEdges += edgesToAdd;
            correspondingVerticles.Add((n1, n2));
        }

        public override string ToString()
        {
            foreach (var el in correspondingEdges)
            {
                G1[el.Item1.v1, el.Item1.v2] = 2;
                G1[el.Item1.v2, el.Item1.v1] = 2;
                G2[el.Item2.v1, el.Item2.v2] = 2;
                G2[el.Item2.v2, el.Item2.v1] = 2;
            }
            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder(), sb2 = new StringBuilder();
            sb1.Append("Verticles from G1: ");
            sb2.Append("Verticles from G2: ");
            foreach (var el in correspondingVerticles)
            {
                if (el.Item2 != -1)
                {
                    sb1.Append(el.Item1 + " ");
                    sb2.Append(el.Item2 + " ");
                }
            }
            sb.AppendLine(sb1.ToString());
            sb.AppendLine(sb2.ToString());

            for (int i = 0; i < G1.GetLength(0); i++)
            {
                for (int j = 0; j < G1.GetLength(0); j++)
                {
                    if (G1[i, j] == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write("1");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write(" ");
                    }
                    else
                        Console.Write(G1[i, j] + " ");
                }
                Console.Write("\n");
            }
            Console.Write("\n");
            for (int i = 0; i < G2.GetLength(0); i++)
            {
                for (int j = 0; j < G2.GetLength(0); j++)
                {

                    if (G2[i, j] == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write("1");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write(" ");
                    }
                    else
                        Console.Write(G2[i, j] + " ");
                }
                Console.Write("\n");
            }
            return sb.ToString();
        }
    }
}

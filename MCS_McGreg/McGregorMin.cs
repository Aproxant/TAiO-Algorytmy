using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAiO_Algorytmy;
using MAX_McGreg;

namespace MCS_McGreg
{
  public class McGregorMin
    {
        static void Main()
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

            Console.WriteLine(G1);

            Console.WriteLine(G2);
            Console.WriteLine("V+E Solution\n");
            McGregorE.McGregor(new State(G1.AdjacencyMatrix, G2.AdjacencyMatrix), ref s);

            //Graph Min = new Graph(s.G1);

            Graph k = MinimumFunctions.MinimumSuperGraph(s,G1,G2);
            Console.WriteLine(k);
            DateTime dk = DateTime.Now;
            

        }
        public static void Run(String[] args)
        {
            string file1 = args[0];
            string file2 = args[1];
  
            var G1 = GraphLoader.LoadGraph(file1, "G1");
            var G2 = GraphLoader.LoadGraph(file2, "G2");
            State s = new State(G1.AdjacencyMatrix, G2.AdjacencyMatrix);

            Console.WriteLine(G1);

            Console.WriteLine(G2);
            Console.WriteLine("V+E Solution\n");
            McGregorE.McGregor(new State(G1.AdjacencyMatrix, G2.AdjacencyMatrix), ref s);

            //Graph Min = new Graph(s.G1);

            Graph k = MinimumFunctions.MinimumSuperGraph(s, G1, G2);
            Console.WriteLine(k);
            DateTime dk = DateTime.Now;
        }
    }

}

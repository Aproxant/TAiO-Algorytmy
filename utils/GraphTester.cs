using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using BrutForce;
using MAX_McGreg;
using MCS_McGreg;
using TAiO_Algorytmy;

namespace TAIO_konsola.utils
{
    public class GraphTester
    {
        public GraphTester()
        {
        }

        public static void Run()
        {
            DirectoryInfo d = new DirectoryInfo(Environment.CurrentDirectory + "/tests"); //Assuming Test is your Folder

            FileInfo[] Files = d.GetFiles("*.txt"); //Getting Text files
   
            for (int i = 0; i < Files.Length; i++)
            {
                RunTests(Files[i].Name, i.ToString());
            }

        }
        public static void RunTests(String fileName, String testNumber)
        {
            Console.WriteLine($"Starting test for {fileName}");

            (var G1, var G2) = GraphLoader.SingleFileGraphLoader("tests/"+fileName, "G");
            Stopwatch sw = new Stopwatch();
            Dictionary<String, TimeSpan> times = new Dictionary<string, TimeSpan>();
            //// Max Macgregor Exact
            {
                sw.Start();
                MaxMcgregor.RunExact(G1, G2);
                sw.Stop();
                times.Add("MaxMacGregor_Exact", sw.Elapsed);
            }

            // Max Macgregor Approx

            {
                sw = new Stopwatch();
                sw.Start();
                MaxMcgregor.RunApprox(G1, G2);
                sw.Stop();
                times.Add("MaxMacGregor_Approx", sw.Elapsed);
            }
            //// BruteForce Max Exact
            {
                sw = new Stopwatch();
                sw.Start();
                var biggestSubExact = BruttForce.MyBrutForce(new AdjacencyMatrix(G1.AdjacencyMatrix), new AdjacencyMatrix(G2.AdjacencyMatrix));
                sw.Stop();
                times.Add("MaxBruteForce_Exact", sw.Elapsed);
            }
            // BruteForce Max Approx
            {
                sw = new Stopwatch();
                sw.Start();
                var biggestSubAprox = BruttForce.MyBrutForceApproximate(new AdjacencyMatrix(G1.AdjacencyMatrix), new AdjacencyMatrix(G2.AdjacencyMatrix));
                times.Add("MaxBruteForce_Approx", sw.Elapsed);
                sw.Stop();
            }

            //// BruteForce Min Exact
            {
                sw = new Stopwatch();
                sw.Start();
                var minGraphExact = MinBrutt.MinBruttForce(new AdjacencyMatrix(G1.AdjacencyMatrix), new AdjacencyMatrix(G2.AdjacencyMatrix));
                sw.Stop();
                times.Add("MinBruteForce_Exact", sw.Elapsed);
            }
            // BruteForce Min Approx
            {
                sw = new Stopwatch();
                sw.Start();
                var minGraphApprox = MinimumAppro.MinimumSuperGraph(new AdjacencyMatrix(G1.AdjacencyMatrix), new AdjacencyMatrix(G2.AdjacencyMatrix));
                sw.Stop();
                times.Add("MinBruteForce_Approx", sw.Elapsed);
            }
            var csv = new StringBuilder();
            foreach (var i in times)
            {
                var newLine = string.Format("{0},{1}", i.Key, i.Value);
                csv.AppendLine(newLine);
            }
            File.WriteAllText(Environment.CurrentDirectory + $"/testResults_{fileName}.csv", csv.ToString());
            Console.WriteLine($"Finished test for {fileName}");

        }

    }
}

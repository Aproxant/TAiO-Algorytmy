﻿using System;
using BrutForce;
using MAX_McGreg;
using MCS_McGreg;
using TAiO_Algorytmy;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                args = new String[2];
                Console.WriteLine("Input first file path\n");
                args[0] = Console.ReadLine();

            }

            try
            {
                (var G1, var G2) = GraphLoader.SingleFileGraphLoader(args[0], "G");
                Console.WriteLine(G1);
                Console.WriteLine(G2);
                bool flag = true;
                while (flag)
                {
                    Console.WriteLine("Choose algorithm to execute");
                    Console.WriteLine("1 - Maximum common subgraph (McGregor algorithm only for undirected graphs)");
                    Console.WriteLine("2 - Maximum common subgraph (Brut Force algorithm)");
                    Console.WriteLine("3 - Minimum common supergraph");

                    Console.WriteLine("9 - Quit the application");
                    char key = Console.ReadKey().KeyChar;
                    Console.WriteLine();
                
                    switch (key)
                    {
                        case '1':
                            Console.WriteLine("a - Exact algorithm");
                            Console.WriteLine("b - Approximate algorithm");
                            char val1 = Console.ReadKey().KeyChar;
                            switch (val1)
                            {
                                case 'a':
                                    MaxMcgregor.RunExact(args);
                                    break;
                                case 'b':
                                    MaxMcgregor.RunApprox(args);
                                    break;
                            }
                            break;
                        case '2':
                            Console.WriteLine("a - Exact algorithm");
                            Console.WriteLine("b - Approximate algorithm");
                            char val3 = Console.ReadKey().KeyChar;
                            switch (val3)
                            {
                                case 'a':
                                    var biggestSubExact = BruttForce.MyBrutForce(new AdjacencyMatrix(G1.AdjacencyMatrix), new AdjacencyMatrix(G2.AdjacencyMatrix));
                                    Console.WriteLine(biggestSubExact);
                                    Console.WriteLine($"Number of Edges {biggestSubExact.EdgeNumber}");
                                    break;
                                case 'b':
                                    var biggestSubAprox = BruttForce.MyBrutForceApproximate(new AdjacencyMatrix(G1.AdjacencyMatrix), new AdjacencyMatrix(G2.AdjacencyMatrix));
                                    Console.WriteLine(biggestSubAprox);
                                    Console.WriteLine($"Number of Edges {biggestSubAprox.EdgeNumber}");

                                    break;
                            }
                            break;
                        case '3':

                            Console.WriteLine("a - Exact algorithm");
                            Console.WriteLine("b - Approximate algorithm");
                            char val2 = Console.ReadKey().KeyChar;
                            switch (val2)
                            {
                                case 'a':
                                    var minGraphExact = MinimumFunctions.MinimumSuperGraph(new AdjacencyMatrix(G1.AdjacencyMatrix), new AdjacencyMatrix(G2.AdjacencyMatrix),false);
                                    Console.WriteLine(minGraphExact);
                                    Console.WriteLine($"Number of Edges {minGraphExact.EdgeNumber}");
                                    break;
                                case 'b':
                                    var minGraphApprox = MinimumFunctions.MinimumSuperGraph(new AdjacencyMatrix(G1.AdjacencyMatrix), new AdjacencyMatrix(G2.AdjacencyMatrix), true);
                                    Console.WriteLine(minGraphApprox);
                                    Console.WriteLine($"Number of Edges {minGraphApprox.EdgeNumber}");
                                    break;
                            }
                            break;
                        case '9':
                            flag = false;
                            Console.WriteLine();
                            break;
                        default:
                            Console.WriteLine("\nInvalid option selected.\n");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
            }



        }
    }
}

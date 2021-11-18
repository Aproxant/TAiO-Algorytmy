﻿using System;
using MAX_McGreg;
using MCS_McGreg;

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
                Console.WriteLine("Input second file path\n");
                args[1] = Console.ReadLine();

            }
           
                try
                {
                    bool flag = true;
                    while (flag)
                    {
                        Console.WriteLine("Choose algorithm to execute");
                        Console.WriteLine("1 - Maximum common subgraph");
                        Console.WriteLine("2 - Minimum common supergraph");
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
                                        MaxMcgregor.Run(args);
                                    Environment.Exit(0);
                                    break;
                                    case 'b':
                                        MaxMcgregor.Run(args);
                                        break;
                                }
                                MaxMcgregor.Run(args);
                                break;
                            case '2':
                                Console.WriteLine("a - Exact algorithm");
                                Console.WriteLine("b - Approximate algorithm");
                                char val2 = Console.ReadKey().KeyChar;
                                switch (val2)
                                {
                                    case 'a':
                                        McGregorMin.Run(args);
                                        break;
                                    case 'b':
                                        McGregorMin.Run(args);
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

                    Console.WriteLine("Press any key to exit.");
                    Console.ReadKey();
                }
            


        }
    }
}
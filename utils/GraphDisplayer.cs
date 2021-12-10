using System;
using BrutForce;

namespace TAIO_konsola.utils
{
    public class GraphDisplayer
    {
        public GraphDisplayer()
        {
        }
        public static void PrintTwoGraphsInRow(AdjacencyMatrix A, AdjacencyMatrix B)
        {
            Console.WriteLine("G1 && G2");
            for (int i = 0; i < A.Size; i++)
            {
                for (int y = 0; y < A.Size; y++)
                {
                    if(A.matrix[i][y]==1)
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(A.matrix[i][y]);
                    Console.ResetColor();
                    Console.Write(" ");

                }
                if (i < B.Size)
                {
                    Console.Write(" ");
                    Console.Write(" ");
                    Console.Write(" ");

                    for (int g = 0; g < B.Size; g++)
                    {
                        if(g < B.matrix[i].Length)
                        {
                            if (B.matrix[i][g] == 1)
                                Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(B.matrix[i][g]);
                            Console.ResetColor();
                            Console.Write(" ");
                        }
                     

                    }
                }
                Console.Write("\n");
            }
        }
        // diffreneces in a2 compared to a1
        public static void printGraphWithDifferences(AdjacencyMatrix a1, AdjacencyMatrix b1, AdjacencyMatrix a2)
        {
            Console.Write("\n");
            for (int i = 0; i < a2.Size; i++)
            {
                for (int j = 0; j < a2.Size; j++)
                {
                    if (a1.matrix[i][j] == a2.matrix[i][j] && a1.matrix[i][j]==1)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    if ( b1.matrix.Length > i && b1.matrix[i].Length > j && b1.matrix[i][j] == a2.matrix[i][j] && a2.matrix[i][j] == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    Console.Write(a2.matrix[i][j]);
                    Console.ResetColor();
                    Console.Write(" ");

                }
                Console.Write("\n");
            }
        }
    }
}

using System;
using BrutForce;

namespace TAIO_konsola.utils
{
    public class GraphDisplayer
    {
        public GraphDisplayer()
        {
        }
        // diffreneces in a2 compared to a1
        public static void printGraphWithDifferences(AdjacencyMatrix a1, AdjacencyMatrix b1, AdjacencyMatrix a2)
        {
            Console.Write("\n");
            for (int i = 0; i < a2.Size; i++)
            {
                for (int j = 0; j < a2.Size; j++)
                {
                    if( a1.matrix[i][j] != a2.matrix[i][j])
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                    }
                    if (b1.matrix[i][j] != a2.matrix[i][j])
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
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

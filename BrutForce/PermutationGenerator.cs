using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrutForce
{
    public class PermutationGenerator
    {
        public int[][] permutations;
        
        public PermutationGenerator(int size)
        {
            permutations = new int[Factorial(size)][];
            int[] tes = new int[size];
            for(int i=0;i<size;i++)
            {
                tes[i] = i;
            }
            GetPer(tes);
        }
        private int Factorial(int f)
        {
            if (f == 0)
                return 1;
            else
                return f * Factorial(f - 1);
        }
        public  static void Swap(ref int a, ref int b)
        {
            if (a == b) return;
            var temp = a;
            a = b;
            b = temp;
        }

        public  void GetPer(int[] list)
        {
            int counter = 0;
            int x = list.Length - 1;

            GetPer(list, 0, x,ref counter);
        }

        private  void GetPer(int[] list, int k, int m,ref int counter)
        {
            int[] copiedData = new int[list.Length];
            list.CopyTo(copiedData, 0);
            if (k == m)
            { 
                permutations[counter] = copiedData;
                counter++;
            }
            else
                for (int i = k; i <= m; i++)
                {
                    Swap(ref list[k], ref list[i]);
                    GetPer(list, k + 1, m,ref counter);
                    Swap(ref list[k], ref list[i]);
                }
        }

    }
}

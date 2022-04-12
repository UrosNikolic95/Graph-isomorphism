using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ClassLibrary1
{
    public class Class2
    {
        public static bool[,] Permute(bool[,] b,Random r)
        {
            int n1 = b.GetLength(0);
            int i1, i2;
            List<int> L1 = new List<int>(n1);
            for (i1 = 0; i1 < n1; i1++)
            {
                L1.Add(i1);
            }
            int[] p1 = new int[n1];
            for (i1 = 0; i1 < n1; i1++)
            {
                i2 = r.Next() % L1.Count;
                p1[i1] = L1.ElementAt(i2);
                L1.RemoveAt(i2);
            }
            b = Rearange_Matrix_According_To_Indexes(b, p1);
            return b;
        }
        public static bool[,] Create_Random_Adjancy_Matrix(int a1, Random r)
        {
            bool[,] b = new bool[a1, a1];
            int i1, i2;
            for (i1 = 0; i1 < a1; i1++)
            {
                for (i2 = 0; i2 < a1; i2++)
                {
                    b[i1, i2] = r.Next() % 100 > 45;
                }
            }
            return b;
        }
        private static bool[,] Rearange_Matrix_According_To_Indexes(bool[,] a1, int[] a2)
        {
            int n1 = a1.GetLength(0);
            int n2 = a2.Count();
            int i1, i2;
            int d1;
            bool[,] b1 = new bool[n1, n1];
            for (i1 = 0; i1 < n1; i1++)
            {
                d1 = a2[i1];
                for (i2 = 0; i2 < n1; i2++)
                {
                    b1[i1, i2] = a1[d1, i2];
                }
            }
            bool[,] b2 = new bool[n1, n1];
            for (i1 = 0; i1 < n1; i1++)
            {
                d1 = a2[i1];
                for (i2 = 0; i2 < n1; i2++)
                {
                    b2[i2, i1] = b1[i2, d1];
                }
            }
            return b2;
        }
        public static bool[,] F1(int n1,int n2)
        {
            Random r = new Random();
            int n3 = n1 * n2;
            bool[,] q1 = new bool[n3,n3];
            bool[,] q2 = Class2.Create_Random_Adjancy_Matrix(n2, r);
            bool[,] q3 = Class2.Create_Random_Adjancy_Matrix(n2, r);
            bool[,] q4 = Class2.Create_Random_Adjancy_Matrix(n2, r);
            for(int i1=0;i1< n1;i1++)
            {                
                F2(i1 * n2, i1 * n2, q1, q2);
                F2(i1 * n2, ((i1 * n2)+1)%n1, q1, q3);
                F2(((i1 * n2) + 1) % n1, i1 * n2, q1, q4);
            }
            return q1;
        }
        private static void F2(int d1, int d2,bool[,] b1,bool[,] b2)
        {
            for(int i1=0;i1<b2.GetLength(0);i1++)
            {
                for(int i2=0;i2<b2.GetLength(1);i2++)
                {
                    b1[d1 + i1, d2 + i2] = b2[i1, i2];
                }
            }
        }
    }
}

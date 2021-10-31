using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GraphFunctions95
{
    public class Generate_Test_Graph
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
            bool[,] q2 = Create_Random_Adjancy_Matrix(n2, r);
            bool[,] q3 = Create_Random_Adjancy_Matrix(n2, r);
            bool[,] q4 = Create_Random_Adjancy_Matrix(n2, r);

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
        





            public static bool[,] F3()
            {
                int t3;
                int n1 = 200;



                Random r = new Random();
                bool[,] b1 = new bool[n1, n1];

                int i1;


                for (i1 = 1; i1 < n1; i1++)//tree
                {
                    t3 = r.Next() % i1;

                    b1[i1, t3] = true;
                    b1[t3, i1] = true;




                }

                return b1;
            }

            static public bool[,] F4()//make non directional regular graph 
            {
                int t1, t2, t3, t4, t5;
                int n1 = 20;//
                int n2 = 3;//number of neigburs of each node
                int n3 = 10;//levels
                List<int> L1 = new List<int>();
                Random r = new Random();
                bool[,] b1 = new bool[n1, n1];
                bool[,] b2 = new bool[n1 * n3, n1 * n3];
                int[] v1 = new int[n1];
                int i1, i2, i3;
                for (i1 = 0; i1 < n1; i1++)
                {
                    v1[i1] = n2;
                }
                L1.Add(0);
                for (i1 = 1; i1 < n1; i1++)
                {
                    t3 = r.Next() % L1.Count;
                    t1 = L1.ElementAt(t3);
                    b1[i1, t1] = true;
                    b1[t1, i1] = true;
                    v1[i1]--;
                    v1[t1]--;
                    if (v1[t1] < 1)
                    {
                        L1.RemoveAt(t3);
                    }
                    if (v1[i1] > 0)
                    {
                        L1.Add(i1);
                    }
                }


                t1 = L1.Count;
                for (i1 = 0; (i1 < t1) && (L1.Count != 0); i1++)
                {
                    t2 = L1.ElementAt(0);
                    L1.RemoveAt(0);
                    t3 = v1[t2];
                    for (i2 = 0; (i2 < t3) && (L1.Count != 0); i2++)
                    {
                        t4 = r.Next() % L1.Count;
                        t5 = L1.ElementAt(t4);
                        b1[t2, t5] = true;
                        b1[t5, t2] = true;
                        v1[t2]--;
                        v1[t5]--;
                        if (v1[t5] < 1)
                        {
                            L1.RemoveAt(t4);
                        }
                    }
                }


                int[,] c1 = new int[n1, n3];
                t1 = 0;
                for (i1 = 0; i1 < n1; i1++)
                {
                    for (i2 = 0; i2 < n3; i2++)
                    {
                        c1[i1, i2] = t1;
                        t1++;
                    }
                }
                for (i1 = 0; i1 < n1; i1++)
                {
                    for (i2 = i1 + 1; i2 < n1; i2++)
                    {
                        if (b1[i1, i2])
                        {
                            List<int> p1 = new List<int>();
                            for (i3 = 0; i3 < n3; i3++)
                            {
                                p1.Add(c1[i1, i3]);
                            }
                            for (i3 = 0; i3 < n3; i3++)
                            {
                                t3 = r.Next() % p1.Count;
                                t1 = p1.ElementAt(t3);
                                p1.RemoveAt(t3);
                                t2 = c1[i2, i3];
                                b2[t2, t1] = true;
                                b2[t1, t2] = true;
                            }
                        }
                    }
                }
                return b2;

            }















        
    }
}

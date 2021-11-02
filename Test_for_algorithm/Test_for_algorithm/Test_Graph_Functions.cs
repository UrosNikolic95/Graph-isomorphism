using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Test_For_Algorithm;
using GraphFunctions95;
namespace ClassLibrary1
{
    public class Test_Graph_Functions
    {
        public static void Test_1_isomorphic_samples()
        {
            Random r = new Random();
            Stopwatch t1 = new Stopwatch();
            bool[,] F_1_Adjancy_Matrix;
            F_1_Adjancy_Matrix = Class2.F1(20, 20);
            bool[,] F_2_Adjancy_Matrix = Class2.Permute(F_1_Adjancy_Matrix, r);
            t1.Start();
            bool is_isomorphic = Graph_Functions.Graph_Isomorphism(
               Graph_Functions.To_Adjency_Lists_From_Adjency_Matrix(F_1_Adjancy_Matrix),
               Graph_Functions.To_Adjency_Lists_From_Adjency_Matrix(F_2_Adjancy_Matrix));
            long time = t1.ElapsedMilliseconds;
            Console.WriteLine(
                "\n\nisomorphic samples"
                + "\nnumber of nodes:" + F_1_Adjancy_Matrix.GetLength(0).ToString().PadLeft(10)
                + "\ntime in miliseconds:" + time.ToString().PadLeft(10));
            Console.WriteLine("is isomorpfic:  " + is_isomorphic);
        }
        public static void Test_2_NOT_isomorphic_samples(int n)
        {
            Random r = new Random();
            Stopwatch t1 = new Stopwatch();
            bool[,] F_1_Adjancy_Matrix = Class2.Create_Random_Adjancy_Matrix(n, r);
            bool[,] F_2_Adjancy_Matrix = Class2.Create_Random_Adjancy_Matrix(n, r);
            t1.Start();
            bool is_isomorphic = Graph_Functions.Graph_Isomorphism(Graph_Functions.To_Adjency_Lists_From_Adjency_Matrix(F_1_Adjancy_Matrix), Graph_Functions.To_Adjency_Lists_From_Adjency_Matrix(F_2_Adjancy_Matrix));
            long time = t1.ElapsedMilliseconds;
            Console.WriteLine(
                "\n\nNot isomorphic samples"
                + "\nnumber of nodes:" + n.ToString().PadLeft(10)
                + "\ntime in miliseconds:" + time.ToString().PadLeft(10));
            Console.WriteLine("///  " + !is_isomorphic);
        }
        public class input_test_3
        {
            public int starting_number_of_nodes;
            public int incresement_of_nodes;
            public int number_of_increasments;
            public int repeatings_for_each_case;
            public bool are_isomorphic;
        }
        public class output_test_3
        {
            public int[] number_of_nodes;
            public double[] average_times;
            public void print()
            {
                string s1 = "number of nodes";
                string s2 = "".PadLeft(4);
                string s3 = "average times";
                int n1 = s1.Length;
                int n2 = s2.Length + s3.Length;
                Console.WriteLine("\n" + s1 + s2 + s3 + " in miliseconds" + "\n");
                int i1;
                for (i1 = 0; i1 < number_of_nodes.Length; i1++)
                {
                    Console.WriteLine(number_of_nodes[i1].ToString().PadLeft(n1) + average_times[i1].ToString("F3").PadLeft(n2));
                }
            }
        }
        public static void Test_3(input_test_3 A1, out output_test_3 B1)
        {
            int number_of_nodes;
            double[] D1 = new double[A1.number_of_increasments];
            int[] G1 = new int[A1.number_of_increasments];
            Stopwatch S = new Stopwatch();
            S.Start();
            Random r = new Random();
            int i1, i2;
            for (i1 = 0; i1 < A1.number_of_increasments; i1++)
            {
                number_of_nodes = A1.starting_number_of_nodes + A1.incresement_of_nodes * i1;
                G1[i1] = number_of_nodes;
                for (i2 = 0; i2 < A1.repeatings_for_each_case; i2++)
                {
                    bool[,] F_1_Adjancy_Matrix = Class2.Create_Random_Adjancy_Matrix(number_of_nodes, r);
                    bool[,] F_2_Adjancy_Matrix;
                    if (A1.are_isomorphic)
                    {
                        F_2_Adjancy_Matrix = Class2.Permute(F_1_Adjancy_Matrix, r);
                    }
                    else
                    {
                        F_2_Adjancy_Matrix = Class2.Create_Random_Adjancy_Matrix(number_of_nodes, r);
                    }
                    S.Restart();
                    bool result = Graph_Functions.Graph_Isomorphism(Graph_Functions.To_Adjency_Lists_From_Adjency_Matrix(F_1_Adjancy_Matrix), Graph_Functions.To_Adjency_Lists_From_Adjency_Matrix(F_2_Adjancy_Matrix));
                    long time = S.ElapsedMilliseconds;
                    D1[i1] += (double)time;
                }
                D1[i1] /= A1.repeatings_for_each_case;
            }
            B1 = new output_test_3();
            B1.average_times = D1;
            B1.number_of_nodes = G1;
        }
        public static void Test_4()
        {
            Random r = new Random();
            int n1 = 100;
            bool[,] Z1 = Class2.Create_Random_Adjancy_Matrix(n1, r);
            bool[,] a1;
            bool[,] a2;
            bool[,] b1;
            bool[,] b2;
            bool c1;
            a1 = Class2.Permute(Z1, r);
            a2 = Class2.Permute(Z1, r);
            b1 = Graph_Functions.Transform_From_Any_Isomorphism_To_Single_Same_Isomorphism(a1);
            b2 = Graph_Functions.Transform_From_Any_Isomorphism_To_Single_Same_Isomorphism(a2);
            c1 = Compare_Matrices(b1, b2);
            Console.WriteLine("Test_4       Rasult:" + c1);
        }
        private static bool Compare_Matrices(bool[,] k1, bool[,] k2)
        {
            int i1, i2;
            for (i1 = 0; i1 < k1.GetLength(0); i1++)
            {
                for (i2 = 0; i2 < k1.GetLength(0); i2++)
                {
                    if (k1[i1, i2] != k2[i1, i2])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public static void Test_5()
        {
            Random r = new Random();
            GraphFunctions95.Mark_Graph g1 = new GraphFunctions95.Mark_Graph();
            int n1 = 100;
            int t1, t2;
            bool[,] Z1;
            bool[,] a1;
            bool[,] a2;
            bool c1;
            Z1 = Class2.Create_Random_Adjancy_Matrix(n1, r);
            a1 = Class2.Permute(Z1, r);
            a2 = Class2.Permute(Z1, r);
            t1 = g1.Insert(a1);
            t2 = g1.Insert(a2);
            c1 = t1 == t2;
            Console.WriteLine("Test_5       Rasult:" + c1);
        }
        public static void Test_6()
        {
            Random r = new Random();
            bool[,] z = Class2.Create_Random_Adjancy_Matrix(100, r);
            int[] b1 = Graph_Functions.Transform_Bool_Matrix_To_Int_Array(z);
            bool[,] v1 = Graph_Functions.Transform_Int_Array_To_Bool_Matrix(b1);
            Console.WriteLine("Test_6       Result:" + Graph_Functions.Are_Identical_Matrixes(z, v1));
        }
        public static void Test_7()
        {
            Random r = new Random();
            Stopwatch t1 = new Stopwatch();
            bool[,] F_1_Adjancy_Matrix;
            F_1_Adjancy_Matrix = Class2.F1(5, 50);
            bool[,] F_2_Adjancy_Matrix = Class2.Permute(F_1_Adjancy_Matrix, r);
            t1.Start();
            bool is_isomorphic = Graph_Functions.Graph_Isomorphism(
               Graph_Functions.To_Adjency_Lists_From_Adjency_Matrix(F_1_Adjancy_Matrix),
               Graph_Functions.To_Adjency_Lists_From_Adjency_Matrix(F_2_Adjancy_Matrix));
            long time = t1.ElapsedMilliseconds;
            Console.WriteLine(
                "\n\nTest_7"
                + "\ntime in miliseconds:" + time.ToString().PadLeft(10));
            Console.WriteLine("///  " + is_isomorphic);
        }
        public static void Test_8()
        {
            Random r = new Random();
            bool[,] Z1 = Class2.F1(5, 30);
            bool[,] a1;
            bool[,] a2;
            bool[,] b1;
            bool[,] b2;
            bool c1;
            a1 = Class2.Permute(Z1, r);
            a2 = Class2.Permute(Z1, r);
            b1 = Graph_Functions.Transform_From_Any_Isomorphism_To_Single_Same_Isomorphism(a1);
            b2 = Graph_Functions.Transform_From_Any_Isomorphism_To_Single_Same_Isomorphism(a2);
            c1 = Graph_Functions.Are_Identical_Matrixes(b1, b2);
            Console.WriteLine("Test_8       Rasult:" + c1);
        }
        public static void Test_Graph_Key_Dictionary()
        {
            Random rand = new Random();
            GraphFunctions95.Graph_Key_Dictionary<int> A = new GraphFunctions95.Graph_Key_Dictionary<int>();
            bool[,] b1 = Class2.Create_Random_Adjancy_Matrix(100, rand);
            A.Add(b1, 1);
            Console.WriteLine("Test_Graph_Key_Dictionary      Result: " + A.ContainsKey(b1));
        }
    }
}

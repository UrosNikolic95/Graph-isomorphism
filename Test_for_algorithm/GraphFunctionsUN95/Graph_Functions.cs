using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace GraphFunctions95
{
    public class Graph_Functions
    {


        private static int[,] Replace_Markers_With_Indexes(int[] marker_maping, int[,] array2D)
        {

            Dictionary<int, int> D1 = new Dictionary<int, int>();
            int i1, i2;
            for (i1 = 0; i1 < marker_maping.Length; i1++)
            {
                D1.Add(marker_maping[i1], i1);
            }
            int[,] c1 = new int[array2D.GetLength(0), array2D.GetLength(1)];
            int t1;
            for (i1 = 0; i1 < array2D.GetLength(0); i1++)
            {
                for (i2 = 0; i2 < array2D.GetLength(1); i2++)
                {
                    D1.TryGetValue(array2D[i1, i2], out t1);
                    array2D[i1, i2] = t1;
                }
            }
            return array2D;
        }


        private static int[] Replace_Markers_With_Indexes(int[] marker_maping, int[] array1D)
        {





            Dictionary<int, int> D1 = new Dictionary<int, int>();
            int i1;
            for (i1 = 0; i1 < marker_maping.Length; i1++)
            {
                D1.Add(marker_maping[i1], i1);
            }
            int[] c1 = new int[array1D.Length];
            int t1;
            for (i1 = 0; i1 < array1D.Length; i1++)
            {
                D1.TryGetValue(array1D[i1], out t1);
                array1D[i1] = t1;

            }
            return array1D;
        }

        private static int[] Calculate_Distances_From_Node_BFS(uint[][] adjancy_lists, int index_of_starting_node)
        {
            List<int> L1 = new List<int>();
            List<int> L2 = new List<int>();
            L1.Add(index_of_starting_node);
            L2.Add(0);
            int[] shortest_distances = new int[adjancy_lists.Length];
            bool[] queued = new bool[adjancy_lists.Length];
            queued[index_of_starting_node] = true;
            int current_node;
            int i1;
            int t1;
            while (L1.Count > 0)
            {
                current_node = L1.ElementAt(0);
                L1.RemoveAt(0);
                t1 = L2.ElementAt(0);
                L2.RemoveAt(0);
                for (i1 = 0; i1 < adjancy_lists[current_node].Length; i1++)
                {

                    if (!queued[adjancy_lists[current_node][i1]])
                    {
                        L1.Add((int)adjancy_lists[current_node][i1]);
                        L2.Add(t1 + 1);
                        queued[adjancy_lists[current_node][i1]] = true;
                        shortest_distances[adjancy_lists[current_node][i1]] = t1 + 1;
                    }


                }




            }
            return shortest_distances;
        }


        static int[][] Calculate_Distances_From_Each_Node(uint[][] adjancy_lists)
        {
            int[][] R1 = new int[adjancy_lists.GetLength(0)][];
            int i1;
            for (i1 = 0; i1 < R1.GetLength(0); i1++)
            {
                R1[i1] = Calculate_Distances_From_Node_BFS(adjancy_lists, i1);
            }

            return R1;
        }

        private static int[] Get_Node_Markers(uint[][] adjancy_lists)
        {
            Mark_Int_List mark_int_list = new Mark_Int_List();
            int[][][] array3D = new int[4][][];

            array3D[0] = Calculate_Distances_From_Each_Node(adjancy_lists);

            array3D[1] = Count_Neighburs_With_Smaler_Distances_From_Each_Node(array3D[0], adjancy_lists);

            array3D[2] = Count_Neighburs_With_Equal_Distances_From_Each_Node(array3D[0], adjancy_lists);

            array3D[3] = Count_Neighburs_With_Greater_Distances_From_Each_Node(array3D[0], adjancy_lists);

            int[] array1D;
            int[,] array2D;


            array2D = From_3D_Array_To_2D_Array(array3D);
            array1D = From_2D_Array_To_1D_Array(array2D);
            return array1D;

        }

        private static int[,] From_3D_Array_To_2D_Array(int[][][] array3D)
        {
            Mark_Int_List mark_int_list = new Mark_Int_List();
            int[] t1;
            int i1, i2, i3;
            int[,] array2D = new int[array3D[0].Length, array3D[0].Length];

            for (i1 = 0; i1 < array3D[0].Length; i1++)
            {
                for (i2 = 0; i2 < array3D[0][0].Length; i2++)
                {
                    t1 = new int[array3D.Length];
                    for (i3 = 0; i3 < array3D.Length; i3++)
                    {
                        t1[i3] = array3D[i3][i1][i2];
                    }
                    array2D[i1, i2] = mark_int_list.Get_Marker(t1.ToList());
                }
            }
            return Replace_Markers_With_Indexes(mark_int_list.Get_All_Marker_Maping(), array2D);

        }

        private static int[] From_2D_Array_To_1D_Array(int[,] array2D)
        {
            Mark_Int_List mark_int_list = new Mark_Int_List();
            int[] t1;
            int[] array1D = new int[array2D.GetLength(0)];
            for (int i1 = 0; i1 < array2D.GetLength(0); i1++)
            {
                t1 = new int[array2D.GetLength(1)];
                for (int i2 = 0; i2 < array2D.GetLength(1); i2++)
                {
                    t1[i2] = array2D[i1, i2];

                }
                Array.Sort(t1);
                array1D[i1] = mark_int_list.Get_Marker(t1.ToList());
            }
            return Replace_Markers_With_Indexes(mark_int_list.Get_All_Marker_Maping(), array1D);


        }


        static int[] Count_Neighburs_With_Smaler_Distances_From_Single_Node(int[] distances, uint[][] adjancy_lists)
        {
            int[] R1 = new int[distances.Length];
            int i1, i2;
            for (i1 = 0; i1 < adjancy_lists.Length; i1++)
            {
                for (i2 = 0; i2 < adjancy_lists[i1].Length; i2++)
                {

                    if (distances[i1] < distances[adjancy_lists[i1][i2]])
                    {
                        R1[i1] += 1;

                    }

                }
            }
            return R1;
        }

        static int[][] Count_Neighburs_With_Smaler_Distances_From_Each_Node(int[][] distances, uint[][] adjancy_lists)
        {
            int[][] R1 = new int[distances.Length][];
            int i1;
            for (i1 = 0; i1 < distances.Length; i1++)
            {

                R1[i1] = Count_Neighburs_With_Smaler_Distances_From_Single_Node(distances[i1], adjancy_lists);
            }
            return R1;
        }


        static int[] Count_Neighburs_With_Equal_Distances_From_Single_Node(int[] a1, uint[][] adjancy_lists)
        {
            int[] R1 = new int[a1.Length];
            int i1, i2;
            for (i1 = 0; i1 < adjancy_lists.Length; i1++)
            {
                for (i2 = 0; i2 < adjancy_lists[i1].Length; i2++)
                {

                    if (a1[i1] == a1[adjancy_lists[i1][i2]])
                    {
                        R1[i1] += 1;

                    }

                }
            }
            return R1;
        }

        static int[][] Count_Neighburs_With_Equal_Distances_From_Each_Node(int[][] a1, uint[][] adjancy_lists)
        {
            int[][] R1 = new int[a1.Length][];
            int i1;
            for (i1 = 0; i1 < a1.Length; i1++)
            {

                R1[i1] = Count_Neighburs_With_Equal_Distances_From_Single_Node(a1[i1], adjancy_lists);
            }
            return R1;
        }


        static int[] Count_Neighburs_With_Greater_Distances_From_Single_Node(int[] a1, uint[][] adjancy_lists)
        {
            int[] R1 = new int[a1.Length];
            int i1, i2;
            for (i1 = 0; i1 < adjancy_lists.Length; i1++)
            {
                for (i2 = 0; i2 < adjancy_lists[i1].Length; i2++)
                {

                    if (a1[i1] > a1[adjancy_lists[i1][i2]])
                    {
                        R1[i1] += 1;

                    }

                }
            }
            return R1;
        }

        static int[][] Count_Neighburs_With_Greater_Distances_From_Each_Node(int[][] a1, uint[][] adjancy_lists)
        {
            int[][] R1 = new int[a1.Length][];
            int i1;
            for (i1 = 0; i1 < a1.Length; i1++)
            {

                R1[i1] = Count_Neighburs_With_Greater_Distances_From_Single_Node(a1[i1], adjancy_lists);
            }
            return R1;
        }

        public static uint[][] To_Adjency_Lists_From_Adjency_Matrix(bool[,] adjency_matrix)
        {
            uint[][] adjancy_lists = new uint[adjency_matrix.GetLength(0)][];
            uint i1, i2;
            for (i1 = 0; i1 < adjency_matrix.GetLength(0); i1++)
            {
                List<uint> L1 = new List<uint>();
                for (i2 = 0; i2 < adjency_matrix.GetLength(1); i2++)
                {
                    if (adjency_matrix[i1, i2])
                    {
                        L1.Add(i2);
                    }
                }
                adjancy_lists[i1] = L1.ToArray();
            }
            return adjancy_lists;
        }

        public static bool[,] To_Adjancy_Matrix_From_Adjency_Lists(uint[][] adjancy_lists)
        {
            bool[,] adjancy_matrix = new bool[adjancy_lists.Length, adjancy_lists.Length];
            int i1, i2;
            for (i1 = 0; i1 < adjancy_lists.Length; i1++)
            {
                for (i2 = 0; i2 < adjancy_lists[i1].Length; i2++)
                {
                    adjancy_matrix[i1, adjancy_lists[i1][i2]] = true;
                }
            }
            return adjancy_matrix;
        }


        private static int Count_Conections(bool[,] a1)
        {
            int t1 = 0;
            int i1, i2;
            for (i1 = 0; i1 < a1.GetLength(0); i1++)
            {
                for (i2 = 0; i2 < a1.GetLength(1); i2++)
                {
                    if (a1[i1, i2])
                    {
                        t1++;
                    }
                }
            }
            return t1;
        }


        private static int[] Count_Number_Of_Neighburs_For_Each_Node(bool[,] a1)
        {
            int[] t1 = new int[a1.GetLength(0)];
            int i1, i2;
            for (i1 = 0; i1 < a1.GetLength(0); i1++)
            {
                for (i2 = 0; i2 < a1.GetLength(1); i2++)
                {
                    if (a1[i1, i2])
                    {
                        t1[i1]++;
                    }
                }
            }
            return t1;
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










        private static void Get_New_Marker_For_Single_Node(Mark_Int_List a1, uint[][] v_b, int i1, int[] t1_b, int[] t2_b)
        {
            int i2;
            List<int> L1 = new List<int>();
            for (i2 = 0; i2 < v_b[i1].Length; i2++)
            {

                L1.Add(t1_b[v_b[i1][i2]]);

            }
            L1.Sort();
            t2_b[i1] = a1.Get_Marker(L1);
        }

        private static bool[,] Rearange_Matrix_According_To_Markers(bool[,] b, int[] t1_b)
        {
            int[] g1 = t1_b.ToArray();
            int[] a1 = new int[g1.Length];
            int i1;
            for (i1 = 0; i1 < g1.Length; i1++)
            {
                a1[i1] = i1;
            }
            Array.Sort(g1, a1);
            return Rearange_Matrix_According_To_Indexes(b, a1);
        }




        public static bool Graph_Isomorphism(uint[][] v_b1, uint[][] v_b2)
        {

            if (v_b1.Length != v_b2.Length)
            {
                return false;
            }





            int[] t1_b1;
            int[] t2_b1 = null;
            t1_b1 = Get_Node_Markers(v_b1);





            int[] t1_b2;
            int[] t2_b2 = null;
            t1_b2 = Get_Node_Markers(v_b2);







            int[] p1;
            int[] p2;




            Mark_Int_List G2 = new Mark_Int_List();

            int i2;
            int r1, r2;
            bool t1 = true;
            for (r1 = 0; r1 < t1_b1.Length; r1++)
            {
                for (r2 = 0; r2 < t1_b1.Length; r2++)
                {
                    p1 = t1_b1.ToArray();
                    p2 = t1_b2.ToArray();
                    Array.Sort(p1);
                    Array.Sort(p2);

                    if (!p1.SequenceEqual(p2))
                    {
                        return false;
                    }



                    t1 = true;
                    for (i2 = 1; i2 < p1.Length; i2++)
                    {
                        if (p1[i2] == p1[i2 - 1])
                        {
                            t1 = false;
                            break;

                        }
                    }


                    if (t1)
                    {
                        break;
                    }

                    t1 = true;
                    for (i2 = 1; i2 < p1.Length; i2++)
                    {
                        if (p2[i2] == p2[i2 - 1])
                        {
                            t1 = false;
                            break;

                        }
                    }


                    if (t1)
                    {
                        break;
                    }


                    t2_b1 = new int[t1_b1.Length];
                    t2_b2 = new int[t1_b2.Length];

                    G2.Clear();


                    Get_New_Marker_For_Each_Node(G2, v_b1, t1_b1, t2_b1);
                    Get_New_Marker_For_Each_Node(G2, v_b2, t1_b2, t2_b2);


                    if (t1_b1.SequenceEqual(t2_b1))
                    {
                        break;
                    }

                    if (t1_b2.SequenceEqual(t2_b2))
                    {
                        break;
                    }
                    t1_b1 = t2_b1;
                    t1_b2 = t2_b2;
                }


                if (t1)
                {
                    break;
                }




                int t4 = Find_Lowest_Duplicate(t1_b1);
                Change_Marker(t4, t1_b1);
                Change_Marker(t4, t1_b2);


            }


            bool[,] k1 = Rearange_Matrix_According_To_Markers(To_Adjancy_Matrix_From_Adjency_Lists(v_b1), t1_b1);
            bool[,] k2 = Rearange_Matrix_According_To_Markers(To_Adjancy_Matrix_From_Adjency_Lists(v_b2), t1_b2);

            return Are_Identical_Matrixes(k1, k2);
        }



        public static bool Is_Square_Matrix(bool[,] k1)
        {
            return k1.GetLength(0) == k1.GetLength(1);
        }

        public static bool Are_Same_Sizes_Of_Matrixes(bool[,] k1, bool[,] k2)
        {
            return (k1.GetLength(0) == k2.GetLength(0)) && (k1.GetLength(1) == k2.GetLength(1));
        }



        public static bool Are_Identical_Matrixes(bool[,] k1, bool[,] k2)
        {
            if (!Are_Same_Sizes_Of_Matrixes(k1, k2))
            {
                return false;
            }
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




        public static bool[,] Transform_From_Any_Isomorphism_To_Single_Same_Isomorphism(bool[,] b1)
        {


            if (b1.GetLength(0) != b1.GetLength(1))
            {
                return null;
            }

            uint[][] v_b1 = To_Adjency_Lists_From_Adjency_Matrix(b1);


            int[] t1_b1;
            int[] t2_b1 = null;
            t1_b1 = Get_Node_Markers(v_b1);









            int[] p1;




            Mark_Int_List G2 = new Mark_Int_List();

            int i2;
            int r1, r2;
            bool t1 = true;
            for (r1 = 0; r1 < 100; r1++)
            {
                for (r2 = 0; r2 < 5; r2++)
                {
                    p1 = t1_b1.ToArray();
                    Array.Sort(p1);

                    t1 = true;
                    for (i2 = 1; i2 < p1.Length; i2++)
                    {
                        if (p1[i2] == p1[i2 - 1])
                        {
                            t1 = false;
                            break;

                        }
                    }


                    if (t1)
                    {
                        break;
                    }



                    t2_b1 = new int[t1_b1.Length];



                    G2.Clear();


                    Get_New_Marker_For_Each_Node(G2, v_b1, t1_b1, t2_b1);


                    if (t1_b1.SequenceEqual(t2_b1))
                    {
                        break;
                    }

                    t1_b1 = Replace_Markers_With_Indexes(G2.Get_All_Marker_Maping().ToArray(), t2_b1);

                }


                if (t1)
                {
                    break;
                }


                int t4 = Find_Lowest_Duplicate(t1_b1);
                Change_Marker(t4, t1_b1);



            }
            return Rearange_Matrix_According_To_Markers(b1, t1_b1);
        }


        private static void Get_New_Marker_For_Each_Node(Mark_Int_List G2, uint[][] v_b1, int[] t1_b1, int[] t2_b1)
        {
            for (int i1 = 0; i1 < t1_b1.GetLength(0); i1++)
            {



                Get_New_Marker_For_Single_Node(G2, v_b1, i1, t1_b1, t2_b1);


            }
        }


        private static void Change_Marker(int t4, int[] t1_b1)
        {
            if (t4 != -1)
            {
                t1_b1[Array.IndexOf(t1_b1, t4)] = int.MaxValue;
            }
        }

        private static int Find_Lowest_Duplicate(int[] t1_b1)
        {
            int[] t3 = t1_b1.ToArray();
            Array.Sort(t3);
            for (int i1 = 0; i1 < t3.Length - 1; i1++)
            {
                if (t3[i1] == t3[i1 + 1])
                {
                    return t3[i1];
                }
            }
            return -1;
        }




        public static char[] Serialize(bool[,] a1)
        {
            int b1 = 8;
            int i1, i2;
            int t0, t1, t2;
            t0 = 0;
            int p1 = (a1.GetLength(0) * a1.GetLength(1)) / b1;
            if ((a1.GetLength(0) * a1.GetLength(1)) % b1 != 0)
            {
                p1++;
            }
            char[] B1 = new char[p1];
            for (i1 = 0; i1 < a1.GetLength(0); i1++)
            {
                for (i2 = 0; i2 < a1.GetLength(1); i2++)
                {
                    t1 = t0 / b1;
                    t2 = t0 % b1;
                    B1[t1] = bit_switch(t2, a1[i1, i2], B1[t1]);
                    t0++;
                }

            }
            return B1;
        }

        public static int[] Transform_Bool_Matrix_To_Int_Array(bool[,] a1)
        {
            int b1 = 32;
            int i1, i2;
            int t0, t1, t2;
            t0 = 0;
            int p1 = (a1.GetLength(0) * a1.GetLength(1)) / b1;
            if ((a1.GetLength(0) * a1.GetLength(1)) % b1 != 0)
            {
                p1++;
            }
            int[] B1 = new int[p1 + 2];
            B1[0] = a1.GetLength(0);
            B1[1] = a1.GetLength(1);
            for (i1 = 0; i1 < a1.GetLength(0); i1++)
            {
                for (i2 = 0; i2 < a1.GetLength(1); i2++)
                {
                    t1 = (t0 / b1) + 2;
                    t2 = t0 % b1;
                    B1[t1] = bit_switch(t2, a1[i1, i2], B1[t1]);
                    t0++;
                }

            }
            return B1;
        }


        static char bit_switch(int index, bool switch_on, char value)
        {
            if (index < 8)
            {
                if (switch_on)
                {
                    value = (char)(value | (char)Math.Pow(2, index));
                }
                else
                {
                    value = (char)(value & (~(char)Math.Pow(2, index)));
                }
                return value;
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }

        private static int bit_switch(int index, bool switch_on, int value)
        {
            if (index < 32)
            {
                if (switch_on)
                {
                    value = value | (1 << index);
                }
                else
                {
                    value = value & (~(1 << index));
                }
                return value;
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }


        private static bool bit_read_from_int(int a1, int i1)
        {
            if (i1 < 32)
            {
                int t1 = 1 << i1;
                return (a1 & t1) == t1;
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }


        public static bool[,] Transform_Int_Array_To_Bool_Matrix(int[] a1)
        {
            int n1 = a1[0];
            int n2 = a1[1];
            int n3 = n1 * n2;
            int i1;
            int k1, k2, k3, k4, k5;

            bool[,] B1 = new bool[n1, n2];
            for (i1 = 0; i1 < n3; i1++)
            {
                k1 = i1 / 32;
                k2 = i1 % 32;
                k3 = k1 + 2;
                k4 = i1 / n1;
                k5 = i1 % n2;
                B1[k4, k5] = bit_read_from_int(a1[k3], k2);

            }

            return B1;
        }

    }
}





﻿using System.Diagnostics;
using System.Text;

namespace GraphFunctions
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
            for (int i1 = 0; i1 < marker_maping.Length; i1++)
            {
                D1.Add(marker_maping[i1], i1);
            }
            int[] c1 = new int[array1D.Length];
            int t1;
            for (int i1 = 0; i1 < array1D.Length; i1++)
            {
                D1.TryGetValue(array1D[i1], out t1);
                array1D[i1] = t1;
            }
            return array1D;
        }
        private static int[] Calculate_Distances_From_Node_BFS(int[][] adjancy_lists, int index_of_starting_node)
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

        static Task Chunked_Calculate_Distances_From_Each_Node(int[][] adjancy_lists, int start, int end, int[][] result)
        {
            int last = Math.Min(end, adjancy_lists.Length);
            return new Task(() =>
            {
                for (int i1 = start; i1 < last; i1++)
                {
                    result[i1] = Calculate_Distances_From_Node_BFS(adjancy_lists, i1);
                }
            });
        }

        static void WaitAllTasks(Task[] tasks)
        {
            for (int i1 = 0; i1 < tasks.Length; i1++)
            {
                tasks[i1].Wait();
            }
        }

        static int calculate_batch_size(int total)
        {
            return (total / 10) + 1;
        }

        static int[][] Calculate_Distances_From_Each_Node(int[][] adjancy_lists)
        {
            int batch_size = calculate_batch_size(adjancy_lists.Length);
            int[][] result = new int[adjancy_lists.Length][];
            List<Task> tasks = new List<Task>();
            for (int i1 = 0; i1 < adjancy_lists.Length; i1 += batch_size)
            {
                Task task = Chunked_Calculate_Distances_From_Each_Node(adjancy_lists, i1, i1 + batch_size, result);
                task.Start();
                tasks.Add(task);
            }
            WaitAllTasks(tasks.ToArray());
            return result;
        }

        private static int[][][] Create_3D_Array(int[][] adjancy_lists)
        {
            int[][][] array3D = new int[4][][];
            array3D[0] = Calculate_Distances_From_Each_Node(adjancy_lists);
            array3D[1] = Count_Neighburs_With_Smaler_Distances_From_Each_Node(array3D[0], adjancy_lists);
            array3D[2] = Count_Neighburs_With_Equal_Distances_From_Each_Node(array3D[0], adjancy_lists);
            array3D[3] = Count_Neighburs_With_Greater_Distances_From_Each_Node(array3D[0], adjancy_lists);
            return array3D;
        }
        private static int[] Get_Primary_Markers(int[][] adjancy_lists)
        {
            Stopwatch watch = new Stopwatch();
            int[][][] array3D = Create_3D_Array(adjancy_lists);
            int[,] array2D = From_3D_Array_To_2D_Array(array3D);
            int[] array1D = From_2D_Array_To_1D_Array(array2D);
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
                    array2D[i1, i2] = mark_int_list.Get_Marker(t1);
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
                array1D[i1] = mark_int_list.Get_Marker(t1);
            }
            return Replace_Markers_With_Indexes(mark_int_list.Get_All_Marker_Maping(), array1D);
        }
        static int[] Count_Neighburs_With_Smaler_Distances_From_Single_Node(int[] distances, int[][] adjancy_lists)
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

        static Task Chunked_Count_Neighburs_With_Smaler_Distances_From_Each_Node(int[][] distances, int[][] adjancy_lists, int start, int end, int[][] result)
        {
            int last = Math.Min(end, adjancy_lists.Length);
            return new Task(() =>
            {
                for (int i1 = start; i1 < last; i1++)
                {
                    result[i1] = Count_Neighburs_With_Smaler_Distances_From_Single_Node(distances[i1], adjancy_lists);
                }
            });
        }
        static int[][] Count_Neighburs_With_Smaler_Distances_From_Each_Node(int[][] distances, int[][] adjancy_lists)
        {
            int batch_size = calculate_batch_size(distances.Length);
            int[][] result = new int[distances.Length][];
            List<Task> tasks = new List<Task>();
            for (int i1 = 0; i1 < distances.Length; i1 += batch_size)
            {
                Task task = Chunked_Count_Neighburs_With_Smaler_Distances_From_Each_Node(distances, adjancy_lists, i1, i1 + batch_size, result);
                task.Start();
                tasks.Add(task);
            }
            WaitAllTasks(tasks.ToArray());
            return result;
        }
        static int[] Count_Neighburs_With_Equal_Distances_From_Single_Node(int[] a1, int[][] adjancy_lists)
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

        static Task Chunk_Count_Neighburs_With_Equal_Distances_From_Single_Node(int[][] a1, int[][] adjancy_lists, int start, int end, int[][] result)
        {

            int last = Math.Min(end, adjancy_lists.Length);
            return new Task(() =>
            {
                for (int i1 = start; i1 < last; i1++)
                {
                    result[i1] = Count_Neighburs_With_Equal_Distances_From_Single_Node(a1[i1], adjancy_lists);
                }
            });
        }
        static int[][] Count_Neighburs_With_Equal_Distances_From_Each_Node(int[][] a1, int[][] adjancy_lists)
        {
            int batch_size = calculate_batch_size(adjancy_lists.Length);
            int[][] result = new int[adjancy_lists.Length][];
            List<Task> tasks = new List<Task>();
            for (int i1 = 0; i1 < a1.Length; i1 += batch_size)
            {
                Task task = Chunk_Count_Neighburs_With_Equal_Distances_From_Single_Node(a1, adjancy_lists, i1, i1 + batch_size, result);
                task.Start();
                tasks.Add(task);
            }
            WaitAllTasks(tasks.ToArray());
            return result;
        }
        static int[] Count_Neighburs_With_Greater_Distances_From_Single_Node(int[] a1, int[][] adjancy_lists)
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

        static Task Chunked_Count_Neighburs_With_Greater_Distances_From_Single_Node(int[][] a1, int[][] adjancy_lists, int start, int end, int[][] result)
        {
            int last = Math.Min(end, adjancy_lists.Length);
            return new Task(() =>
            {
                for (int i1 = start; i1 < last; i1++)
                {
                    result[i1] = Count_Neighburs_With_Greater_Distances_From_Single_Node(a1[i1], adjancy_lists);
                }
            });
        }

        static int[][] Count_Neighburs_With_Greater_Distances_From_Each_Node(int[][] a1, int[][] adjancy_lists)
        {
            int batch_size = calculate_batch_size(adjancy_lists.Length);
            int[][] result = new int[a1.Length][];
            List<Task> tasks = new List<Task>();
            for (int i1 = 0; i1 < a1.Length; i1 += batch_size)
            {
                Task task = Chunked_Count_Neighburs_With_Greater_Distances_From_Single_Node(a1, adjancy_lists, i1, i1 + batch_size, result);
                task.Start();
                tasks.Add(task);
            }
            WaitAllTasks(tasks.ToArray());
            return result;
        }
        public static int[][] To_Adjency_Lists_From_Adjency_Matrix(bool[,] adjency_matrix)
        {
            int[][] adjancy_lists = new int[adjency_matrix.GetLength(0)][];
            int i1, i2;
            for (i1 = 0; i1 < adjency_matrix.GetLength(0); i1++)
            {
                List<int> L1 = new List<int>();
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
        public static bool[,] To_Adjancy_Matrix_From_Adjency_Lists(int[][] adjancy_lists)
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
        private static void Get_New_Marker_For_Single_Node(Mark_Int_List a1, int[][] v_b, int i1, int[] t1_b, int[] t2_b)
        {
            int i2;
            List<int> L1 = new List<int>();
            for (i2 = 0; i2 < v_b[i1].Length; i2++)
            {
                L1.Add(t1_b[v_b[i1][i2]]);
            }
            L1.Sort();
            t2_b[i1] = a1.Get_Marker(L1.ToArray());
        }

        private static int Get_New_Marker_For_Single_NodeV2(Mark_Int_List a1, int[] neigburs, int[] markers)
        {
            List<int> L1 = new List<int>();
            for (int i1 = 0; i1 < neigburs.Length; i1++)
            {
                L1.Add(markers[neigburs[i1]]);
            }
            L1.Sort();
            return a1.Get_Marker(L1.ToArray());
        }
        private static bool[,] Rearange_Matrix_According_To_Markers(bool[,] adjancy_matrix, int[] markers)
        {
            int[] copied_markers = markers.ToArray();
            int[] indexes = new int[copied_markers.Length];
            int i1;
            for (i1 = 0; i1 < copied_markers.Length; i1++)
            {
                indexes[i1] = i1;
            }
            Array.Sort(copied_markers, indexes);
            return Rearange_Matrix_According_To_Indexes(adjancy_matrix, indexes);
        }
        public static bool Graph_Isomorphism(int[][] adjancy_matrix_A, int[][] adjancy_matrinx_B)
        {
            if (adjancy_matrix_A.Length != adjancy_matrinx_B.Length)
            {
                return false;
            }
            int[] next_markers_A;
            int[] current_markers_A = Get_Primary_Markers(adjancy_matrix_A);
            int[] next_markers_B;
            int[] current_markers_B = Get_Primary_Markers(adjancy_matrinx_B);
            int[] p1;
            int[] p2;
            Mark_Int_List G2 = new Mark_Int_List();
            int i2;
            int r1, r2;
            bool should_break = true;
            for (r1 = 0; r1 < current_markers_A.Length; r1++)
            {
                for (r2 = 0; r2 < current_markers_A.Length; r2++)
                {
                    p1 = current_markers_A.ToArray();
                    p2 = current_markers_B.ToArray();
                    Array.Sort(p1);
                    Array.Sort(p2);
                    if (!p1.SequenceEqual(p2))
                    {
                        return false;
                    }
                    should_break = true;
                    for (i2 = 1; i2 < p1.Length; i2++)
                    {
                        if (p1[i2] == p1[i2 - 1])
                        {
                            should_break = false;
                            break;
                        }
                    }
                    if (should_break)
                    {
                        break;
                    }
                    should_break = true;
                    for (i2 = 1; i2 < p1.Length; i2++)
                    {
                        if (p2[i2] == p2[i2 - 1])
                        {
                            should_break = false;
                            break;
                        }
                    }
                    if (should_break)
                    {
                        break;
                    }
                    G2.Clear();
                    next_markers_A = Get_New_Marker_For_Each_NodeV2(G2, adjancy_matrix_A, current_markers_A);
                    next_markers_B = Get_New_Marker_For_Each_NodeV2(G2, adjancy_matrinx_B, current_markers_B);

                    next_markers_A = Replace_Markers_With_Indexes(G2.Get_All_Marker_Maping().ToArray(), next_markers_A);
                    next_markers_B = Replace_Markers_With_Indexes(G2.Get_All_Marker_Maping().ToArray(), next_markers_B);

                    if (current_markers_A.SequenceEqual(next_markers_A))
                    {
                        break;
                    }
                    if (current_markers_B.SequenceEqual(next_markers_B))
                    {
                        break;
                    }
                    current_markers_A = next_markers_A;
                    current_markers_B = next_markers_B;
                }
                if (should_break)
                {
                    break;
                }
                int lowest_duplicate = Find_Lowest_Duplicate(current_markers_A);
                Change_Marker(lowest_duplicate, current_markers_A);
                Change_Marker(lowest_duplicate, current_markers_B);
            }
            bool[,] k1 = Rearange_Matrix_According_To_Markers(To_Adjancy_Matrix_From_Adjency_Lists(adjancy_matrix_A), current_markers_A);
            bool[,] k2 = Rearange_Matrix_According_To_Markers(To_Adjancy_Matrix_From_Adjency_Lists(adjancy_matrinx_B), current_markers_B);
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

        public static bool All_Diferent(int[] current_markers)
        {
            int[] copied_markers = current_markers.ToArray();
            Array.Sort(copied_markers);
            for (int i1 = 1; i1 < copied_markers.Length; i1++)
            {
                if (copied_markers[i1] == copied_markers[i1 - 1])
                {
                    return false;
                }
            }
            return true;
        }
        public static int[] Get_Next_Markers(int[][] adjancy_list, int[] markers, Mark_Int_List G2)
        {
            int[] current_markers = markers;
            for (int i1 = 0; i1 < adjancy_list.Length; i1++)
            {
                if (All_Diferent(current_markers))
                {
                    return current_markers;
                }
                G2.Clear();
                int[] next_preliminary_markers = Get_New_Marker_For_Each_NodeV2(G2, adjancy_list, current_markers);
                int[] next_secondary_markers = Replace_Markers_With_Indexes(G2.Get_All_Marker_Maping().ToArray(), next_preliminary_markers);
                if (current_markers.SequenceEqual(next_secondary_markers))
                {
                    return current_markers;
                }
                current_markers = next_secondary_markers;
            }
            return current_markers;
        }

        public static int[] Break_Symetry(int[][] adjancy_list, int[] markers)
        {
            int[] current_markers = markers;
            Mark_Int_List G2 = new Mark_Int_List();
            for (int i1 = 0; i1 < adjancy_list.Length; i1++)
            {
                current_markers = Get_Next_Markers(adjancy_list, current_markers, G2);
                int duplicate_marker = Find_Lowest_Duplicate(current_markers);
                Change_Marker(duplicate_marker, current_markers);
            }
            return current_markers;
        }

        public static int[] Get_Complete_Markers(int[][] adjancy_list)
        {
            int[] primary_markers = Get_Primary_Markers(adjancy_list);
            int[] secondary_markers = Break_Symetry(adjancy_list, primary_markers);
            return secondary_markers;
        }

        public static int[] Get_Complete_Markers_V2(int[][] adjancy_list)
        {
            int[] primary_markers = Get_Primary_Markers(adjancy_list);
            int[] secondary_markers = Break_Symetry_V2(adjancy_list, primary_markers);
            return secondary_markers;
        }

        public static bool[,] Transform_From_Any_Isomorphism_To_Single_Same_Isomorphism(bool[,] adjancy_matrix)
        {
            if (!Is_Square_Matrix(adjancy_matrix))
            {
                throw new Exception("Adjancy matrix not square.");
            }
            int[][] adjancy_list = To_Adjency_Lists_From_Adjency_Matrix(adjancy_matrix);
            int[] markers = Get_Complete_Markers(adjancy_list);
            return Rearange_Matrix_According_To_Markers(adjancy_matrix, markers);
        }

        public static bool Graph_Isomorphism_V2(bool[,] A, bool[,] B)
        {
            bool[,] standardized_A = Transform_From_Any_Isomorphism_To_Single_Same_Isomorphism_V2(A);
            bool[,] standardized_B = Transform_From_Any_Isomorphism_To_Single_Same_Isomorphism_V2(B);
            return Compare_Matrices(standardized_A, standardized_B);
        }


        public static bool Compare_Matrices(bool[,] A, bool[,] B)
        {
            if (!Is_Square_Matrix(A)) throw new Exception("A is not square matrix;");
            if (!Is_Square_Matrix(B)) throw new Exception("B is not square matrix;");
            if (A.GetLength(0) != B.GetLength(0)) return false;
            for (int i1 = 0; i1 < A.GetLength(0); i1++)
            {
                for (int i2 = 0; i2 < A.GetLength(1); i2++)
                {
                    if (A[i1, i2] != B[i1, i2])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool[,] Transform_From_Any_Isomorphism_To_Single_Same_Isomorphism_V2(bool[,] adjancy_matrix)
        {
            if (!Is_Square_Matrix(adjancy_matrix))
            {
                throw new Exception("adjancy_matrix not square;");
            }
            int[][] adjancy_list = To_Adjency_Lists_From_Adjency_Matrix(adjancy_matrix);
            int[] markers = Get_Complete_Markers_V2(adjancy_list);
            return Rearange_Matrix_According_To_Markers(adjancy_matrix, markers);
        }
        private static void Get_New_Marker_For_Each_Node(Mark_Int_List G2, int[][] v_b1, int[] t1_b1, int[] t2_b1)
        {
            for (int i1 = 0; i1 < t1_b1.GetLength(0); i1++)
            {
                Get_New_Marker_For_Single_Node(G2, v_b1, i1, t1_b1, t2_b1);
            }
        }

        private static int[] Get_New_Marker_For_Each_NodeV2(Mark_Int_List G2, int[][] adjancy_list, int[] markers)
        {
            int[] new_markers = new int[markers.Length];
            for (int i1 = 0; i1 < markers.GetLength(0); i1++)
            {
                new_markers[i1] = Get_New_Marker_For_Single_NodeV2(G2, adjancy_list[i1], markers);
            }
            return new_markers;
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



        public static int[] Break_Symetry_V2(int[][] adjancy_list, int[] markers)
        {
            int minus_counter = -1;
            int[] current_markers = markers;
            for (int i1 = 0; i1 < adjancy_list.Length; i1++)
            {
                if (Are_All_Diferent(current_markers))
                {
                    return current_markers;
                }
                current_markers = Create_New_Markers(adjancy_list, current_markers, ref minus_counter);
            }
            return current_markers;
        }

        public static bool Are_All_Diferent(int[] arr)
        {
            int[] copy = arr.ToArray();
            Array.Sort(copy);
            for (int i1 = 1; i1 < copy.Length; i1++)
            {
                if (copy[i1 - 1] == copy[i1])
                {
                    return false;
                }
            }
            return true;
        }

        public static int Count_Diferent(int[] arr)
        {
            int[] copy = arr.ToArray();
            Array.Sort(copy);
            int counter = 1;
            for (int i1 = 1; i1 < copy.Length; i1++)
            {
                if (copy[i1 - 1] != copy[i1])
                {
                    counter++;
                }
            }
            return counter;
        }

        public static int count_dif(int[] A, int[] B)
        {
            int counter = 0;
            for (int i1 = 1; i1 < A.Length; i1++)
            {
                if (A[i1] != B[i1]) counter++;
            }
            return counter;
        }
        public static void print_arr(int[] arr)
        {
            for (int i1 = 0; i1 < arr.Length; i1++)
            {
                Console.Write(arr[i1] + " ");
            }
        }

        public static int[] Create_New_Markers(int[][] adjancy_list, int[] old_markers, ref int minus_counter)
        {
            int duplicate = Find_Lowest_Duplicate(old_markers);
            int index = Array.IndexOf(old_markers, duplicate);



            HashSet<int> taken = new HashSet<int>();
            taken.Add(index);

            HashSet<int> is_in_previous_layers = new HashSet<int>();
            is_in_previous_layers.Add(index);
            int[] next_layer = new int[] { index };

            int[] new_markers = new int[old_markers.Length];
            new_markers[index] = minus_counter--;


            while (next_layer.Length != 0)
            {
                int[] current_layer = next_layer;
                next_layer = Take_Next_Layer(adjancy_list, current_layer, taken);
                Get_New_Markers(adjancy_list, next_layer, old_markers, new_markers, is_in_previous_layers);
                Mark_Is_In_Prevoius_Layers(next_layer, is_in_previous_layers);
            }
            return new_markers;
        }

        public static void Get_New_Markers(int[][] adjancy_list, int[] next_layer, int[] old_markers, int[] new_markers, HashSet<int> is_in_previous_layers)
        {
            Mark_Int_List marker = new Mark_Int_List();
            for (int i1 = 0; i1 < next_layer.Length; i1++)
            {
                int current_node = next_layer[i1];
                List<int> neigburs_markers = new List<int>();
                for (int i2 = 0; i2 < adjancy_list[current_node].Length; i2++)
                {
                    int neighbur = adjancy_list[current_node][i2];
                    if (is_in_previous_layers.Contains(neighbur))
                    {
                        neigburs_markers.Add(new_markers[neighbur]);
                    }
                    else
                    {
                        neigburs_markers.Add(old_markers[neighbur]);
                    }
                }
                int[] arr = neigburs_markers.ToArray();
                Array.Sort(arr);
                new_markers[current_node] = marker.Get_Marker(arr);
            }
            Partial_Replace_Markers_With_Indexes(next_layer, new_markers, marker);
        }

        //Replace_Markers_With_Indexes
        public static void Partial_Replace_Markers_With_Indexes(int[] next_layer, int[] new_markers, Mark_Int_List marker)
        {
            int max = new_markers.Max();
            int[] markers = marker.Get_All_Marker_Maping();
            Dictionary<int, int> dic = new Dictionary<int, int>();
            for (int i1 = 0; i1 < markers.Length; i1++)
            {
                dic[markers[i1]] = i1;
            }
            for (int i1 = 0; i1 < next_layer.Length; i1++)
            {
                int current_node = next_layer[i1];
                new_markers[current_node] = dic[new_markers[current_node]] + max;
            }
        }

        public static int[] Take_Next_Layer(int[][] adjancy_list, int[] current_layer, HashSet<int> taken)
        {
            List<int> preliminary = new List<int>();
            for (int i1 = 0; i1 < current_layer.Length; i1++)
            {
                int current_node = current_layer[i1];
                for (int i2 = 0; i2 < adjancy_list[current_node].Length; i2++)
                {
                    int neighbur = adjancy_list[current_node][i2];
                    if (!taken.Contains(neighbur))
                    {
                        taken.Add(neighbur);
                        preliminary.Add(neighbur);
                    }
                }
            }
            return preliminary.ToArray();
        }

        public static void Mark_Is_In_Prevoius_Layers(int[] current_layer, HashSet<int> got_new_markers)
        {
            for (int i1 = 0; i1 < current_layer.Length; i1++)
            {
                got_new_markers.Add(current_layer[i1]);
            }
        }
    }



}

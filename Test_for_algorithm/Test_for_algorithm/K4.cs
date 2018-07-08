using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace G1K
{
    public class MarkNodes
    {

        private static int[,] ReplaceMarkersWithIndexes(int[] markerMaping, int[,] array2D)
        {

            Dictionary<int, int> D1 = new Dictionary<int, int>();
            int i1,i2;
            for (i1 = 0; i1 < markerMaping.Length; i1++)
            {
                D1.Add(markerMaping[i1], i1);
            }
            int[,] c1 = new int[array2D.GetLength(0),array2D.GetLength(1)];
            int t1;
            for (i1 = 0; i1 < array2D.GetLength(0); i1++)
            {
                for (i2 = 0; i2 < array2D.GetLength(1); i2++)
                {
                    D1.TryGetValue(array2D[i1,i2], out t1);
                    array2D[i1,i2] = t1;
                }
            }
            return array2D;
        }


        private static int[] ReplaceMarkersWithIndexes(int[] markerMaping, int[] array1D)
        {

            Dictionary<int, int> D1 = new Dictionary<int, int>();
            int i1;
            for (i1 = 0; i1 < markerMaping.Length; i1++)
            {
                D1.Add(markerMaping[i1], i1);
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

        private static int[] CalculateDistancesFromNode_BFS(bool[,] adjancyMatrix, int indexOfStartingNode)
        {
            List<int> L1 = new List<int>();
            List<int> L2 = new List<int>();
            L1.Add(indexOfStartingNode);
            L2.Add(0);
            int[] shortest_distances = new int[adjancyMatrix.GetLength(0)];
            bool[] queued = new bool[adjancyMatrix.GetLength(0)];
            queued[indexOfStartingNode]=true;
            int current_node;
            int i1;
            int t1;
            while (L1.Count > 0)
            {
                current_node = L1.ElementAt(0);
                L1.RemoveAt(0);
                t1 = L2.ElementAt(0);
                L2.RemoveAt(0);
                for (i1 = 0; i1 < adjancyMatrix.GetLength(0); i1++)
                {
                    if (adjancyMatrix[current_node, i1])
                    {
                        if (!queued[i1])
                        {
                            L1.Add(i1);
                            L2.Add(t1 + 1);
                            queued[i1] = true;
                            shortest_distances[i1] = t1 + 1;
                        }
                    }

                }




            }
            return shortest_distances;
        }


        static int[][] CalculateDistencesFromEachNode(bool[,] adjancyMatrix)
        {
            int[][] R1 = new int[adjancyMatrix.GetLength(0)][];
            int i1;
            for (i1 = 0; i1 < R1.GetLength(0); i1++)
            {
                R1[i1] = CalculateDistancesFromNode_BFS(adjancyMatrix, i1);
            }

            return R1;
        }

        public static int[] GetNodeMarkers(bool[,] adjancyMatrix, Mark_Int_List markIntList)
        {
            int[][][] array3D = new int[4][][];
            array3D[0] = CalculateDistencesFromEachNode(adjancyMatrix);
            array3D[1] = CountNeighbursWithSmalerDistancesForEachNode(array3D[0], adjancyMatrix);
            array3D[2] = CountNeighbursWithEqualDistancesForEachNode(array3D[0], adjancyMatrix);
            array3D[3] = CountNeighbursWithGreaterDistancesForEachNode(array3D[0], adjancyMatrix);
            int[] handleIntArray;
            int[] array1D;
            int i1, i2, i3;
            int[,] array2D = new int[array3D[0].Length, array3D[0].Length];

            for (i1 = 0; i1 < array3D[0].Length; i1++)
            {
                for (i2 = 0; i2 < array3D[0][0].Length; i2++)
                {
                    handleIntArray = new int[array3D.Length];
                    for (i3 = 0; i3 < array3D.Length; i3++)
                    {
                        handleIntArray[i3] = array3D[i3][i1][i2];
                    }
                    array2D[i1, i2] = markIntList.Get_Marker(handleIntArray.ToList());
                }
            }
            array2D = ReplaceMarkersWithIndexes(markIntList.Get_All_Marker_Maping(),array2D);
            array1D = new int[array2D.GetLength(0)];
            for (i1 = 0; i1 < array2D.GetLength(0); i1++)
            {
                handleIntArray = new int[array2D.GetLength(1)];
                for (i2 = 0; i2 < array2D.GetLength(1); i2++)
                {
                    handleIntArray[i2] = array2D[i1, i2];

                }
                Array.Sort(handleIntArray);
                array1D[i1] = markIntList.Get_Marker(handleIntArray.ToList());
            }
            array1D = ReplaceMarkersWithIndexes(markIntList.Get_All_Marker_Maping(), array1D);
            return array1D;

        }

        public static int[] GetNodeMarkersNoMarkerReplacement(bool[,] adjancyMatrix,Mark_Int_List markIntList)
        {
            int[][][] array3D = new int[4][][];
            array3D[0] = CalculateDistencesFromEachNode(adjancyMatrix);
            array3D[1] = CountNeighbursWithSmalerDistancesForEachNode(array3D[0], adjancyMatrix);
            array3D[2] = CountNeighbursWithEqualDistancesForEachNode(array3D[0], adjancyMatrix);
            array3D[3] = CountNeighbursWithGreaterDistancesForEachNode(array3D[0], adjancyMatrix);
            int[] t1;
            int[] array1D;
            int i1, i2, i3;
            int[,] array2D = new int[array3D[0].Length, array3D[0].Length];

            for(i1=0;i1<array3D[0].Length;i1++)
            {
                for(i2 = 0; i2 < array3D[0][0].Length; i2++)
                {
                    t1 = new int[array3D.Length];
                    for(i3=0;i3<array3D.Length;i3++)
                    {
                        t1[i3] = array3D[i3][i1][i2];
                    }
                    array2D[i1,i2]=markIntList.Get_Marker(t1.ToList());
                 }
            }
            array1D = new int[array2D.GetLength(0)];
            for (i1 = 0; i1 <array2D.GetLength(0); i1++)
            {
                t1 = new int[array2D.GetLength(1)];
                for (i2 = 0; i2 < array2D.GetLength(1); i2++)
                {
                    t1[i2] = array2D[i1, i2];

                }
                Array.Sort(t1);
                array1D[i1] = markIntList.Get_Marker(t1.ToList());
            }
           
            return array1D;
               
        }

        static int[] CountNeighbursWithSmalerDistances(int[] a1, bool[,] adjancyMatrix)
        { 
            int[] R1 = new int[a1.Length];
            int i1, i2;
            for(i1=0;i1<adjancyMatrix.GetLength(0);i1++)
            {
                for(i2=0;i2<adjancyMatrix.GetLength(0);i2++)
                {
                    if (adjancyMatrix[i1, i2])
                    {
                        if (a1[i1] < a1[i2])
                        {
                            R1[i1] += 1;

                        }
                    }
                }
            }
            return R1;
        }

        static int[][] CountNeighbursWithSmalerDistancesForEachNode(int[][] a1, bool[,] adjancyMatrix)
        {
            int[][] R1 = new int[a1.Length][];
            int i1;
            for(i1=0;i1<a1.Length;i1++)
            {
                R1[i1] = CountNeighbursWithSmalerDistances(a1[i1],adjancyMatrix);
            }
            return R1;
        }


        static int[] CountNeighbursWithEqualDistances(int[] a1, bool[,] adjancyMatrix)
        {
            int[] R1 = new int[a1.Length];
            int i1, i2;
            for (i1 = 0; i1 < adjancyMatrix.GetLength(0); i1++)
            {
                for (i2 = 0; i2 < adjancyMatrix.GetLength(0); i2++)
                {
                    if (adjancyMatrix[i1, i2])
                    {
                        if (a1[i1] == a1[i2])
                        {
                            R1[i1] += 1;

                        }
                    }
                }
            }
            return R1;
        }

        static int[][] CountNeighbursWithEqualDistancesForEachNode(int[][] a1, bool[,] adjancyMatrix)
        {
            int[][] R1 = new int[a1.Length][];
            int i1;
            for (i1 = 0; i1 < a1.Length; i1++)
            {
                R1[i1] = CountNeighbursWithEqualDistances(a1[i1], adjancyMatrix);
            }
            return R1;
        }


        static int[] CountNeighbursWithGreaterDistances(int[] a1, bool[,] adjancyMatrix)
        {
            int[] R1 = new int[a1.Length];
            int i1, i2;
            for (i1 = 0; i1 < adjancyMatrix.GetLength(0); i1++)
            {
                for (i2 = 0; i2 < adjancyMatrix.GetLength(0); i2++)
                {
                    if (adjancyMatrix[i1, i2])
                    {
                        if (a1[i1] > a1[i2])
                        {
                            R1[i1] += 1;

                        }
                    }
                }
            }
            return R1;
        }

        static int[][] CountNeighbursWithGreaterDistancesForEachNode(int[][] a1, bool[,] adjancyMatrix)
        {
            int[][] R1 = new int[a1.Length][];
            int i1;
            for (i1 = 0; i1 < a1.Length; i1++)
            {
                R1[i1] = CountNeighbursWithGreaterDistances(a1[i1], adjancyMatrix);
            }
            return R1;
        }

    }
}

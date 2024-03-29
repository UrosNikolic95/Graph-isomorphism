﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace GraphFunctions
{
    public class Mark_Int_List
    {
        private int Mark_Counter;
        private List<El> list_Of_Arrays = new List<El>();
        public Mark_Int_List()
        {
            Mark_Counter = 0;
        }
        public Mark_Int_List(int start = 0)
        {
            Mark_Counter = start;
        }
        public void Clear()
        {
            Mark_Counter = 0;
            list_Of_Arrays.Clear();
        }
        public int Get_Marker(int a1)
        {
            return list_Of_Arrays.ElementAt(a1).Mark;
        }
        public int[] Get_Int_Array(int a1)
        {
            return list_Of_Arrays.ElementAt(a1).list.ToArray();
        }
        public int Get_Length()
        {
            return list_Of_Arrays.Count;
        }
        public int[] Get_All_Marker_Maping()
        {
            int[] G1 = new int[list_Of_Arrays.Count];
            int i1;
            for (i1 = 0; i1 < list_Of_Arrays.Count; i1++)
            {
                G1[i1] = list_Of_Arrays.ElementAt(i1).Mark;
            }
            return G1;
        }
        public int[][] Get_All_Int_Arrays()
        {
            int[][] G1 = new int[list_Of_Arrays.Count][];
            int i1;
            for (i1 = 0; i1 < list_Of_Arrays.Count; i1++)
            {
                G1[i1] = list_Of_Arrays.ElementAt(i1).list.ToArray();
            }
            return G1;
        }
        private char Compare_Int_Arrays(int[] a1, int[] a2)
        {
            if (a1.Length < a2.Length)
            {
                return 'l';
            }
            if (a1.Length > a2.Length)
            {
                return 'g';
            }
            int i1;
            for (i1 = 0; i1 < a1.Length; i1++)
            {
                if (a1[i1] < a2[i1])
                {
                    return 'l';
                }
                if (a1[i1] > a2[i1])
                {
                    return 'g';
                }
            }
            return 'e';
        }
        private bool Already_Seen(int[] list, out int index)
        {//n*log(base:2,n)  binary search
            index = 0;
            int min, max, mid;
            char ch_a;
            min = 0;
            max = list_Of_Arrays.Count;
            mid = max;
            while (min != max)
            {
                mid = (min + max) / 2;
                //n
                ch_a = Compare_Int_Arrays(list_Of_Arrays.ElementAt(mid).list, list);
                if (ch_a == 'g')
                {
                    max = mid;
                }
                else if (ch_a == 'l')
                {
                    min = mid + 1;
                }
                if (ch_a == 'e')
                {
                    index = mid;
                    return true;
                }
            }
            index = min;
            return false;
        }
        public int Get_Marker(int[] L)
        {//n*log(base:2,n)
            int t2;
            if (!Already_Seen(L, out t2))//n*log(base:2,n)
            {
                El d1 = new El();
                d1.Mark = Mark_Counter;
                d1.list = L;
                list_Of_Arrays.Insert(t2, d1);
                Mark_Counter++;
            }
            return list_Of_Arrays.ElementAt(t2).Mark;
        }
    }
    public class El
    {
        public int[] list = new int[0];
        public int Mark;
    }
}

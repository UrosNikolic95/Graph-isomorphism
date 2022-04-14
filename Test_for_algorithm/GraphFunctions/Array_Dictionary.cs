



using System.Diagnostics.CodeAnalysis;

namespace GraphFunctions
{
    public class Array_Dictionary
    {
        public Array_Dictionary(int start_from)
        {
            counter = start_from;
        }
        private SortedList<int[], int> list = new SortedList<int[], int>(new ArrComp());
        int counter = 0;
        public int getMarker(int[] arr)
        {
            if (!list.ContainsKey(arr))
            {
                list[arr] = counter++;
            }
            return list[arr];
        }

        public int getIndex(int[] arr)
        {
            return list.IndexOfKey(arr);
        }

    }

    class ArrComp : IComparer<int[]>
    {
        public int Compare(int[]? x, int[]? y)
        {
            if (x?.Length > y?.Length) return 1;
            if (x?.Length < y?.Length) return -1;
            for (int i1 = 0; i1 < x?.Length; i1++)
            {
                if (x[i1] > y?[i1])
                {
                    return 1;
                }
                if (x[i1] < y?[i1])
                {
                    return -1;
                }
            }
            return 0;
        }


    }

    class ArrEqu : IEqualityComparer<int[]>
    {
        public bool Equals(int[]? x, int[]? y)
        {
            if (x == null) return false;
            if (y == null) return false;
            return x.SequenceEqual(y);
        }

        public int GetHashCode([DisallowNull] int[] obj)
        {
            int bits = 32;
            int hash = 0;
            for (int i1 = 0; i1 < obj.Length; i1++)
            {
                hash ^= obj[i1];
                hash = hash << 8 | hash >> bits - 8; //rotate by 8 bits
            }
            return hash;
        }
    }
}




using System.Diagnostics.CodeAnalysis;

namespace GraphFunctions
{
    public class Array_Dictionary
    {
        private Dictionary<int[], int> dic = new Dictionary<int[], int>(new ArrComp());
        int counter = 0;
        public int getMarker(int[] arr)
        {
            if (!dic.ContainsKey(arr))
            {
                dic[arr] = counter++;
            }
            return dic[arr];
        }

    }

    class ArrComp : IEqualityComparer<int[]>
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
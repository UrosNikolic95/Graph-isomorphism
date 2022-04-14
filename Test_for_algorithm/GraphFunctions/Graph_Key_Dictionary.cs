namespace GraphFunctions
{
    public class Graph_Key_Dictionary<Type>
    {
        Dictionary<int[], Type> dictionary = new Dictionary<int[], Type>(new Graph_Comparer());
        public Type this[bool[,] key]
        {
            get
            {
                return dictionary[
                    Graph_Functions.Transform_Bool_Matrix_To_Int_Array(
                        Graph_Functions.Transform_From_Any_Isomorphism_To_Single_Same_Isomorphism(key))];
            }
            set
            {
                dictionary[
                 Graph_Functions.Transform_Bool_Matrix_To_Int_Array(
                     Graph_Functions.Transform_From_Any_Isomorphism_To_Single_Same_Isomorphism(key))] = value;
            }
        }
        public int Count
        {
            get
            {
                return dictionary.Count;
            }
        }
        public ICollection<bool[,]> Keys
        {
            get
            {
                int[][] A = dictionary.Keys.ToArray();
                bool[][,] B = new bool[A.Length][,];
                for (int i1 = 0; i1 < A.Length; i1++)
                {
                    B[i1] = Graph_Functions.Transform_Int_Array_To_Bool_Matrix(A[i1]);
                }
                return B;
            }
        }
        public ICollection<Type> Values
        {
            get
            {
                return dictionary.Values;
            }
        }
        public void Add(bool[,] key, Type value)
        {
            dictionary.Add(
                 Graph_Functions.Transform_Bool_Matrix_To_Int_Array(
                        Graph_Functions.Transform_From_Any_Isomorphism_To_Single_Same_Isomorphism(key))
            , value);
        }
        public void Clear()
        {
            dictionary.Clear();
        }
        public bool ContainsKey(bool[,] key)
        {
            return dictionary.ContainsKey(
                 Graph_Functions.Transform_Bool_Matrix_To_Int_Array(
                        Graph_Functions.Transform_From_Any_Isomorphism_To_Single_Same_Isomorphism(key)));
        }
        public bool Remove(bool[,] key)
        {
            return dictionary.Remove(Graph_Functions.Transform_Bool_Matrix_To_Int_Array(
                         Graph_Functions.Transform_From_Any_Isomorphism_To_Single_Same_Isomorphism(key)));
        }
        public bool TryGetValue(bool[,] key, out Type? value)
        {
            return dictionary.TryGetValue(
                Graph_Functions.Transform_Bool_Matrix_To_Int_Array(
                        Graph_Functions.Transform_From_Any_Isomorphism_To_Single_Same_Isomorphism(key)),
               out value);
        }
        class Graph_Comparer : IEqualityComparer<int[]>
        {
            public bool Equals(int[] x, int[] y)
            {
                return x.SequenceEqual(y);
            }
            public int GetHashCode(int[] obj)
            {
                int hash = 0;
                int i1;
                for (i1 = 0; i1 < obj.Length; i1++)
                {
                    hash ^= obj[i1];
                }
                return hash;
            }
        }
    }
}

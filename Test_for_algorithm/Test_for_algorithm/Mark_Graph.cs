using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UN95;

namespace G1K
{
    public class Mark_Graph
    {
        private Mark_Int_List mrk_int_list;


        public Mark_Graph()
        {
            mrk_int_list = new Mark_Int_List();
        }

        public void Clear()
        {
            mrk_int_list.Clear();
        }

        public int F01(bool[,] a1)
        {
           return mrk_int_list.Get_Marker(
               Graph_Functions.Transform_Bool_Matrix_To_Int_Array(
                   Graph_Functions.Transform_From_Any_Isomorphism_To_Single_Same_Isomorphism(a1)).ToList());
        }

        public bool[,] F06(int a1)
        {
           return Graph_Functions.Transform_Int_Array_To_Bool_Matrix(mrk_int_list.Get_Int_Array(a1));
        }


       
       
    }
}

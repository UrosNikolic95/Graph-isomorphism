using ClassLibrary1;

namespace TestingConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            /// Class1.Test_1_isomorphic_samples(number_of_nodes)
            // Class1.Test_1_isomorphic_samples(500);
            // Class1.Test_1_isomorphic_samples(750);
            // Class1.Test_1_isomorphic_samples(1000);
            TestFunctions.Test_1_isomorphic_samples();
            //    Class1.Test_2_NOT_isomorphic_samples(250);
            //   Class1.Test_2_NOT_isomorphic_samples(500);
            //   Class1.Test_2_NOT_isomorphic_samples(750);
            //   Class1.Test_2_NOT_isomorphic_samples(1000);
            //   Class1.input_test_3 input_T3 = new Class1.input_test_3();
            //   input_T3.are_isomorphic = true;
            //   input_T3.starting_number_of_nodes = 200;
            //   input_T3.incresement_of_nodes = 0;
            //   input_T3.repeatings_for_each_case =5;
            //    input_T3.number_of_increasments = 5;
            //  Class1.output_test_3 output_T3 = null;
            //Class1.Test_3(input_T3, out output_T3);
            //  output_T3.print();
            // Class1.Test_7();
            TestFunctions.Test_4();
            TestFunctions.Test_5();
            TestFunctions.Test_6();
            TestFunctions.Test_8();
            TestFunctions.Test_Graph_Key_Dictionary();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UN95;

namespace Test_For_Algorithm
{
    
    public class Class3
    {
        
        



        public static bool[,] F2()
        {
            int t3;
            int n1 = 200;

 
   
            Random r = new Random();
            bool[,] b1 = new bool[n1, n1];
    
            int i1;
          
        
            for (i1 = 1; i1 < n1; i1++)//tree
            {
                t3 = r.Next() % i1;
   
                b1[i1, t3] = true;
                b1[t3, i1] = true;
               
              
    
                
            }

            return b1;
        }

        static public bool[,] F1()//make non directional regular graph 
        {
            int t1,t2,t3,t4,t5;
            int n1 = 20;//
            int n2 = 3;//number of neigburs of each node
            int n3 = 10;//levels
            List<int> L1=new List<int>();
            Random r = new Random();
            bool[,] b1 = new bool[n1, n1];
            bool[,] b2 = new bool[n1*n3,n1*n3];
            int[] v1 = new int[n1];
            int i1,i2,i3;
            for (i1=0;i1< n1;i1++)
            {
                v1[i1] = n2;
            }
            L1.Add(0);
            for (i1 = 1; i1 < n1; i1++)
            {
                t3 = r.Next() % L1.Count;
                t1 = L1.ElementAt(t3);
                b1[i1, t1] = true;
                b1[t1, i1] = true;
                v1[i1]--;
                v1[t1]--;
                if(v1[t1]<1)
                {
                    L1.RemoveAt(t3);
                }
                if(v1[i1]>0)
                {
                    L1.Add(i1);
                }
            }

            
            t1 = L1.Count;
            for(i1=0;(i1< t1)&&(L1.Count!=0);i1++)
            {
                t2 = L1.ElementAt(0);
                L1.RemoveAt(0);
                t3 = v1[t2];
                for(i2=0;(i2< t3)&&(L1.Count != 0);i2++)
                {
                    t4 = r.Next() % L1.Count;
                    t5 = L1.ElementAt(t4);
                    b1[t2, t5] = true;
                    b1[t5, t2] = true;
                    v1[t2]--;
                    v1[t5]--;
                    if (v1[t5] < 1)
                    {
                        L1.RemoveAt(t4);
                    }
                }
            }
            

            int[,] c1 = new int[n1,n3];
            t1 = 0;
            for(i1=0;i1< n1;i1++)
            {
                for(i2=0;i2< n3;i2++)
                {
                    c1[i1, i2] = t1;
                    t1++;
                }
            }
            for(i1=0;i1< n1;i1++)
            {
                for(i2=i1+1;i2< n1;i2++)
                {
                    if(b1[i1,i2])
                    {
                        List<int> p1 = new List<int>();
                        for ( i3=0;i3<n3;i3++)
                        {
                            p1.Add(c1[i1,i3]);
                        } 
                        for( i3=0;i3< n3;i3++)
                        {
                            t3 = r.Next() % p1.Count;
                            t1 = p1.ElementAt(t3);
                            p1.RemoveAt(t3);
                            t2 = c1[i2,i3];
                            b2[t2, t1] = true;
                            b2[t1, t2] = true;
                        }
                    }
                }
            }
            return b2;

        }















    }
}

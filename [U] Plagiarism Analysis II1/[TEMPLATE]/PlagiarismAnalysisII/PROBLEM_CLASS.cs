using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem
{
    public static class PROBLEM_CLASS
    {

        #region YOUR CODE IS HERE
        struct ver
        {
            public string vertices;
            public float wight;
        };

        //Your Code is Here:
        //==================
        /// <summary>
        /// Given an UNDIRECTED Graph of matching pairs with their similarity scores (%), find the component with max average similarity score & return its average & corresponding IDs
        /// </summary>
        /// <param name="edges">array of matching pairs and their similarity score</param>
        /// <param name="maxAvgScore">return param1: max average score </param>
        /// <param name="IDs">return param2: IDs of the component with max average similarity score</param>


        public static void RequiredFunction(Tuple<string, string, float>[] edges, ref float maxAvgScore, ref List<string> IDs)
        {
            //REMOVE THIS LINE BEFORE START CODING
            //throw new NotImplementedException();
            Dictionary<string, List<ver>> d = new Dictionary<string, List<ver>>();
            List<string> li = new List<string>();
            Dictionary<string, bool> visited2 = new Dictionary<string, bool>();




            ver v = new ver();

            for (int i = 0; i < edges.Length; i++)
            {

                if (!d.ContainsKey(edges[i].Item1))
                {
                    d[edges[i].Item1] = new List<ver>();
                    li.Add(edges[i].Item1); 
                    visited2[edges[i].Item1] = false;

                }
                if (!d.ContainsKey(edges[i].Item2))
                {
                    d[edges[i].Item2] = new List<ver>();
                    li.Add(edges[i].Item2);
                    visited2[edges[i].Item2] = false;

                }
                v.wight = edges[i].Item3;
                v.vertices = edges[i].Item2;
                d[edges[i].Item1].Add(v);
                v.vertices = edges[i].Item1;
                d[edges[i].Item2].Add(v);


            }
            float[] maxw = new float[li.Count];
            int x = 0;
            List<string>[] l = new List<string>[li.Count];
            Queue<string> q = new Queue<string>();
            float m = -1, c = 0;
            int index = -1;




            foreach (var i in li)
            {

                if (visited2[i] == true)
                {
                    continue;
                }
                else
                {
                    l[x] = new List<string>();
                    l[x].Add(i);
                    q.Enqueue(i);
                    visited2[i] = true;
                }



                while (q.Count != 0)
                {



                    foreach (var it in d[q.Dequeue()])
                    {

                        maxw[x] += it.wight;
                        c++;
                        if (visited2[it.vertices] == true)
                            continue;
                        else
                        {

                            l[x].Add(it.vertices);

                            visited2[it.vertices] = true;
                            q.Enqueue(it.vertices);


                        }

                    }

                }





                if (c != 0)
                    maxw[x] /= c;
                if (maxw[x] > m)
                {
                    m = maxw[x];
                    index = x;
                }
                x++;
                c = 0;

            }

            IDs = l[index];
            maxAvgScore = maxw[index];
        }

        #endregion
    }
}
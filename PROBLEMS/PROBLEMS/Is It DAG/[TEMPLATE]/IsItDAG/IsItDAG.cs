using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem
{
    public static class IsItDAG
    {
        #region YOUR CODE IS HERE
        //Your Code is Here:
        //==================
        public static bool IsDAG(string[] vertices, KeyValuePair<string, string> [] edges)
        {
            //REMOVE THIS LINE BEFORE START CODING
            // throw new NotImplementedException();
            Dictionary<string, List<string>> d = new Dictionary<string, List<string>>();
            Dictionary<string, bool> v = new Dictionary<string, bool>();
            Queue<string> q = new Queue<string>();
            long x = 0;
            foreach (var i in vertices)
            {
                d[i] = new List<string>();
                v[i] = false;
            }
            foreach (var i in edges)
            {

                    d[i.Key].Add(i.Value);
            }

            foreach (var i in vertices)
            {
                if (v[i])
                    break;
                q.Enqueue(i);
                 x = 0;
                while (q.Count != 0)
                {
                    string s = q.Dequeue();
                    v[s] = true;
                        foreach (var it in d[s])
                        {
                    
                          
                            x++;
                           
                           if(v[it])
                            q.Enqueue(it);
                        }
                    if(x> vertices.Length)
                    {
                        return false;
                    }
                    
                }

            }
            return true;
        }
   
        #endregion
    }
}

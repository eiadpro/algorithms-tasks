using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Problem
{
    // *****************************************
    // DON'T CHANGE CLASS OR FUNCTION NAME
    // YOU CAN ADD FUNCTIONS IF YOU NEED TO
    // *****************************************
    public static class PROBLEM_CLASS
    {
        #region YOUR CODE IS HERE
        //Your Code is Here:
        //==================
        /// Fill this function to calculate the minimum imbalance of items
        /// </summary>
        /// <param name="items">array of integers (items' weights)</param>
        /// <param name="N">chambers count (half of the items count) </param>
        /// <returns>the minimum imbalance</returns>
        /// 
        public static int solve(int[] items, int N) 
        { 
            int r = 0; 
            double m = 0; 
            int x; 
            for (int i = 0; i < items.Length; i++) 
            { 
                m += items[i]; 
            } 
            m /= N; 
            x = (int)Math.Round(m); 
            for (int i = 0; i < N; i++) 
            { 
                if (items.Length % 2 == 0) 
                { 
                    r += Math.Abs(x - (items[i] + items[items.Length - 1 - i])); 
                } 
                else 
                { 
                    if (i == N - 1) 
                    { 
                        break; 
                    } 
                    r += Math.Abs(x - (items[i] + items[items.Length - 2 - i])); 
                } 
            } 
            if (items.Length % 2 != 0) 
                r += Math.Abs(x - items[items.Length - 1]); return r; 
        }
        public static int RequiredFunction(int[] items, int N) 
        { 
            Array.Sort(items); 
            return solve(items, N); 
        }
    }
    #endregion }
}



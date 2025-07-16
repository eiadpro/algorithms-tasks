using System;
using System.Collections.Generic;
using System.Linq;
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
        static public bool BS(int n, int[] arr, int start, int end)
        {
            int x = (start + end) / 2;
            if (n == arr[x])
                return true;
            else if (start >= end)
            {
                return false;
            }
            else if (n > arr[x])
            {
                return BS(n, arr, x + 1, end);
            }
            else
            {
                return BS(n, arr, start, x - 1);
            }

        }

        enum SOLUTION_TYPE { NAIVE, EFFICIENT };
        static SOLUTION_TYPE solType = SOLUTION_TYPE.EFFICIENT;

        //Your Code is Here:
        //==================
        /// <summary>
        /// Find common elements between the given arrays (if any) 
        /// If not found, return an empty array (i.e. new int[] { })
        /// </summary>
        /// <param name="arr1">1st array </param>
        /// <param name="arr2">2nd array </param>
        /// <returns>array of common element (if any) or empty array if no common elements. </returns>
        static public int[] RequiredFuntion(int[] arr1, int[] arr2)
        {

            //REMOVE THIS LINE BEFORE START CODING
            // throw new NotImplementedException();
            Queue<int> q = new Queue<int>();

            if ((arr2.Length != 0) && (arr1.Length != 0))
            {
                Array.Sort(arr1);
                for (int i = 0; i < arr2.Length; i++)
                {
                    if (BS(arr2[i], arr1, 0, arr1.Length - 1))
                    {
                        q.Enqueue(arr2[i]);
                    }

                }
            }
            int[] arr = new int[q.Count];
            for (int i = 0; i < q.Count; i++)
            {
                arr[i] = q.ElementAt(i);
            }
            return arr;
        }


        #endregion
    }
}
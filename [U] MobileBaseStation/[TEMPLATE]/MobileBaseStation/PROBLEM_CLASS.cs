using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
		/// <summary>
		/// This function calculates the minimum number of base stations required to ensure full coverage along a river. 
		/// </summary>
		/// <param name="N">Size of the array of houses</param>
		/// <param name="houses">Zero Based  array of coordinates of the houses on x axis </param>
		/// <returns>The min number of stations </returns>
		static public int RequiredFunction(int N, int[] houses)
		{
			//REMOVE THIS LINE BEFORE START CODING
			//throw new NotImplementedException();
			if (N <= 0)
			{
				return 0;
			}

			int count = 1, check = houses[0] + 8;
			for (int i = 1; i < N; i++)
			{
				if (houses[i] > check)
				{
					count++;
					check = houses[i] + 8;
				}
				else if (check >= houses[N - 1])
				{
					break;
				}
			}
			return count;
		}
		#endregion
	}
}
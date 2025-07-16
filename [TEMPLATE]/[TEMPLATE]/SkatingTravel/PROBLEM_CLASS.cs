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
        //Your Code is Here:
        //==================
        /// <summary>
        /// Ali Baba decides to go on a skating travel in the alpine mountain. He has stolen a pair of skis and a trail map listing 
        /// the mountain’s surfaces and slopes (n in total), and he wants to ski from surface S to surface T where a treasure is exists. 
        /// </summary>
        /// <param name="vertices">array of surfaces and their elevations </param>
        /// <param name="edges">array of trails and their lengths </param>
        /// <param name="startVertex">name of the start surface to begin from it</param>
        /// <returns>the minimum valid distance from source “S” to target “T”.</returns>
        public static int RequiredFunction(Dictionary<string, int> vertices, Dictionary<KeyValuePair<string, string>, int> edges, string startVertex)
        {
            //REMOVE THIS LINE BEFORE START CODING
            //throw new NotImplementedException();
            Queue<string> queue = new Queue<string>();

            Dictionary<string, int> distances = new Dictionary<string, int>();
            foreach (var vertex in vertices.Keys)
            {
                distances[vertex] = int.MaxValue;
            }
            distances[startVertex] = 0;

            
            queue.Enqueue(startVertex);

            while (queue.Count > 0)
            {
            
                string currentVertex = queue.Dequeue();

            
                foreach (var edge in edges.Keys)
                {
                    string neighbor;
                    if (edge.Key == currentVertex) 
                    {
                        neighbor = edge.Value;
                    }
                    else if (edge.Value == currentVertex) 
                    {
                        neighbor = edge.Key;
                    }
                    else
                    {
                        continue; 
                    }

                    
                    if (vertices[neighbor] < vertices[currentVertex])
                    {
                        int newDistance = distances[currentVertex] + edges[edge];

                       
                        if (newDistance < distances[neighbor])
                        {
                            distances[neighbor] = newDistance;
                            queue.Enqueue(neighbor); 
                        }
                    }
                }
            }

            
            return distances["T"];

            #endregion
        }
    }
}
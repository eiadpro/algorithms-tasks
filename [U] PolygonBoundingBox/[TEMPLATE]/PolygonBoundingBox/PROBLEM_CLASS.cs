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

    #region Helper structures
    public struct BoundingBox
    {
        public double minX;
        public double minY;
        public double maxX;
        public double maxY;
    }

    public struct Point
    {
        public double X;
        public double Y;
    }
    #endregion

    public static class PROBLEM_CLASS
    {
        #region YOUR CODE IS HERE

        public static double minY(Point[] Points, int N, int start, int end, double min)
        {
            int x = (start + end) / 2;

            if (min> Points[x].Y)
            {
                min = Points[x].Y;
            }
            if (x < N-1)
            {
                if (min > Points[x + 1].Y)
                {
                    min = Points[x + 1].Y;
                }
            }
            if (x >= 1)
            {
                if (min > Points[x - 1].Y)
                {
                    min = Points[x - 1].Y;
                }
            }
            if (start >= end)
                return min;
            if (Points[0].Y < Points[N - 1].Y && Points[0].Y < Points[x + 1].Y)
                minY(Points, N, start, x - 1, min);
            else
            {
                minY(Points, N, x + 1, end, min);
            }

            return min;
        }
        public static double maxX(Point[] Points, int N, int start, int end, double max)
        {
            int x = (start + end) / 2;

            if (max < Points[x].X)
            {
                max = Points[x].X;
            }
            if (x < N - 1)
            {
                if (max < Points[x + 1].X)
                {
                    max = Points[x + 1].X;
                }
            }
            if (x >= 1)
            {
                if (max < Points[x - 1].X)
                {
                    max = Points[x - 1].X;
                }
            }
            if (start >= end)
                return max;
            if (Points[0].X > Points[N - 1].X && Points[0].X > Points[x + 1].X)
                maxX(Points, N, start, x - 1, max);
            else
            {
                maxX(Points, N, x + 1, end, max);
            }

            return max;
        }
        public static double maxY(Point[] Points, int N, int start, int end, double max)
        {
            int x = (start + end) / 2;

            if (max < Points[x].Y)
            {
                max = Points[x].Y;
            }
            if (x < N - 1)
            {
                if (max < Points[x + 1].Y)
                {
                    max = Points[x + 1].Y;
                }
            }
            if (x >= 1)
            {
                if (max < Points[x - 1].Y)
                {
                    max = Points[x - 1].Y;
                }
            }
            if (start >= end)
                return max;
            if (max > Points[N - 1].Y && Points[0].Y > Points[x + 1].Y)
                maxY(Points, N, start, x - 1, max);
            else
            {
                maxY(Points, N, x + 1, end, max);
            }

            return max;
        }
        //Your Code is Here:
        //==================
        /// <summary>
        /// This function shall find the bounding box of a given convex polygon in an efficient way (i.e. minX, maxX, minY, maxY)..
        /// </summary>
        /// <param name="Points">Array of the convex polygon points in counterclockwise order</param>
        /// <param name="N">The number of the polygon's points</param>
        /// <returns>BoundingBox object containing the values of the four points of the bounding box</returns>
        public static BoundingBox RequiredFunction(Point[] Points, int N)
        {
            //REMOVE THIS LINE BEFORE START CODING
            // throw new NotImplementedException();
            BoundingBox r;
            Point max;
            Point min;
            double minx;
            double fx = Points[0].X;
            double lx = Points[N - 1].X;
            if (fx > lx)
                minx = lx;
            else
                minx = fx;
            double miny;
            double fy = Points[0].Y;
            double ly = Points[N - 1].Y;
            if (fy > ly)
                miny = ly;
            else
                miny = fy;

            double maxx;
            double fx1 = Points[0].X;
            double lx1 = Points[N - 1].X;
            if (fx1 < lx1)
                maxx = lx1;
            else
                maxx = fx1;

            double maxy;
            double fy1 = Points[0].Y;
            double ly1 = Points[N - 1].Y;
            if (fy1 < ly1)
                maxy = ly1;
            else
                maxy = fy1;
            max.X = maxX(Points, N, 0, Points.Length - 1, maxx);
            max.Y = maxY(Points, N, 0, Points.Length - 1, maxy);
            min.X = Points[0].X;
            min.Y = minY(Points, N, 0, Points.Length - 1, miny);

            r.maxX = max.X;
            r.maxY = max.Y;
            r.minX = min.X;
            r.minY = min.Y;

            return r;
        }
        #endregion 
    }
}

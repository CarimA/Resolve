using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Resolve
{
    public class ShapePrimitives
    {
        /// <summary>
        /// Creates an approximation of a circle based on the numbed of n-sides.
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="radius"></param>
        /// <param name="sides"></param>
        /// <returns></returns>
        public static Polygon Circle(Vector2 origin, float radius, int sides)
        {
            List<Vector2> points = new List<Vector2>();

            float angle = (float)Math.PI * 2 / (float)sides;

            for (int i = 0; i < sides; i++)
            {
                points.Add(new Vector2(radius * (float)Math.Cos(angle * i), radius * (float)Math.Sin(angle * i)));
            }

            return new Polygon(origin, points);
        }

        public static Polygon Rectangle(Vector2 min, Vector2 max)
        {
            return Rectangle(min.X, min.Y, max.X, max.Y);
        }

        public static Polygon Rectangle(float aX, float aY, float bX, float bY)
        {
            List<Vector2> points = new List<Vector2>();
            points.Add(new Vector2(aX, aY));
            points.Add(new Vector2(bX, aY));
            points.Add(new Vector2(bX, bY));
            points.Add(new Vector2(aX, bY));
            return new Polygon(points.First(), points);
        }
    }
}

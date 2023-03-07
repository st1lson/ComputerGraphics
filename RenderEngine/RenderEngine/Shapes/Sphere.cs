﻿using RenderEngine.Basic;
using RenderEngine.Interfaces;

namespace RenderEngine.Shapes
{
    public record Sphere(Vector3 Orig, float Radius) : IShape
    {
        public Vector3? Intersects(Ray ray)
        {
            var k = ray.Orig - Orig;

            var a = Vector3.Dot(ray.Dir, ray.Dir);
            var b = 2 * Vector3.Dot(ray.Dir, k);
            var c = Vector3.Dot(k, k) - Radius * Radius;

            var D = b * b - 4 * a * c;

            if (D < 0) 
            {
                return null;
            }

            var t1 = (float)(-b - Math.Sqrt(D)) / (2 * a);
            
            if(t1 >= 0)
            {
                return ray.GetPoint(t1);
            }

            var t2 = (float)(-b + Math.Sqrt(D)) / (2 * a);

            return t2 > 0 ? ray.GetPoint(t2) : null;
        }

        public Vector3 GetNormal(Vector3 intersectionPoint)
        {
            return intersectionPoint - Orig;
        }
    }
}

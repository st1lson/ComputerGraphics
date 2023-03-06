using RenderEngine.Basic;
using RenderEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderEngine.Shapes
{
    public record Sphere : IShape
    {
        public Vector3 Orig { get; init; }
        public float Radius{ get; init; }

        public Sphere(Vector3 orig, float radius)
        {
            Orig = orig;
            Radius = radius;
        }

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

        public Vector3 GetNormal(Vector3? intersectionPoint)
        {
            if(intersectionPoint == null)
            {
                return new Vector3(0, 0, 0);
            }

            return (Vector3)intersectionPoint - Orig;
        }
    }
}

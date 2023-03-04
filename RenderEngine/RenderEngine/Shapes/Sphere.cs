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

        public float Intersects(Ray ray)
        {
            var k = ray.Orig - Orig;

            var a = Vector3.Dot(ray.Dir, ray.Dir);
            var b = 2 * Vector3.Dot(ray.Dir, k);
            var c = Vector3.Dot(k, k) - Radius * Radius;

            var D = b * b - 4 * a * c;

            if (D < 0) 
            {
                return -1;
            }

            return (float)(-b - Math.Sqrt(D)) / (2 * a);
        }
    }
}

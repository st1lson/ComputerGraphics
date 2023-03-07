using RenderEngine.Interfaces;
using RenderEngine.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderEngine.Shapes
{
    public record Disk : IShape
    {
        public Vector3 Orig { get; init; }
        public float Radius { get; init; }
        public Vector3 Normal { get; init; }

        public Disk(Vector3 orig, float radius, Vector3 normal)
        {
            Orig = orig;
            Radius = radius;
            Normal = normal;
        }
        public Vector3? Intersects(Ray ray)
        {
            const float tolerance = 1e-6f;
            float t = 0;
            bool isPlaneIntersect = false;
            // plane intersection
            float denom = Vector3.Dot(Normal, ray.Dir);
            if (Math.Abs(denom) > tolerance)
            {
                Vector3 rayOrigDiff = Orig - ray.Orig;
                t = Vector3.Dot(rayOrigDiff, Normal) / denom;
                if (t >= 0)
                    isPlaneIntersect = true;
            }
            if (!isPlaneIntersect)
                return null;
            // check intersect with disk
            Vector3 vectorIntersect = ray.GetPoint(t);
            Vector3 distance = vectorIntersect - Orig;
            float distanceValue = Vector3.Dot(distance, distance);

            return distanceValue <= Radius * Radius ? vectorIntersect : null;
        }

        public Vector3 GetNormal(Vector3? intersectionPoint)
        {
            return Normal;
        }
    }
}

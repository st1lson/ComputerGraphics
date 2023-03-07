using RenderEngine.Interfaces;
using RenderEngine.Basic;

namespace RenderEngine.Shapes
{
    public record Disk(Vector3 Orig, float Radius, Vector3 Normal) : IShape
    {
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

        public Vector3 GetNormal(Vector3 intersectionPoint)
        {
            return Normal;
        }
    }
}

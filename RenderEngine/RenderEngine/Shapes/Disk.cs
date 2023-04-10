using RenderEngine.Interfaces;
using RenderEngine.Basic;
using RenderEngine.Transformer;

namespace RenderEngine.Shapes;

public class Disk : IShape
{
    public Vector3 Orig { get; private set; }

    public float Radius { get; private set; }

    public Vector3 Normal { get; private set;}

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
            {
                isPlaneIntersect = true;
            }
        }

        if (!isPlaneIntersect)
        {
            return null;
        }

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

    public void Transform(Transform transform)
    {

        float D = -(Normal.X * Orig.X + Normal.Y * Orig.Y + Normal.Z * Orig.Z);
        float z = -(Normal.X + Normal.Y + D) / Normal.Z;
        Vector3 vectorRadius = new Vector3(1, 1, z).Normalize()*Radius;
        vectorRadius = vectorRadius.TransformAsDirection(transform);

        Radius = vectorRadius.Abs();
        Normal = Normal.TransformAsDirection(transform);
        Orig = Orig.Transform(transform);
    }
}
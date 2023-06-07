using RenderEngine.Basic;
using RenderEngine.Interfaces;
using RenderEngine.Optimizers;
using RenderEngine.Transformer;

namespace RenderEngine.Shapes;

public class Sphere : IShape
{
    public Vector3 Orig { get; private set; }

    public float Radius { get; private set; }

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

        var d = b * b - 4 * a * c;

        if (d < 0)
        {
            return null;
        }

        var t1 = (float)(-b - Math.Sqrt(d)) / (2 * a);
        if (t1 >= 0)
        {
            return ray.GetPoint(t1);
        }

        var t2 = (float)(-b + Math.Sqrt(d)) / (2 * a);

        return t2 > 0 ? ray.GetPoint(t2) : null;
    }

    public Vector3 GetNormal(Vector3 intersectionPoint)
    {
        return (intersectionPoint - Orig).Normalize();
    }

    public Vector3 GetInterpolatedNormal(Vector3 intersectionPoint)
    {
        return (intersectionPoint - Orig).Normalize();
    }

    public void Transform(Transform transform)
    {
        Vector3 vectorRadius = Orig.Normalize() * Radius;
        vectorRadius = vectorRadius.TransformAsDirection(transform);

        Radius = vectorRadius.Abs();
        Orig = Orig.Transform(transform);
    }

    public bool IsInsideBox(BoundingBox box)
    {
        throw new NotImplementedException();
    }
}
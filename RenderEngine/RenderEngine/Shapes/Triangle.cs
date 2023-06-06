using RenderEngine.Basic;
using RenderEngine.Interfaces;
using RenderEngine.Transformer;

namespace RenderEngine.Shapes;

public class Triangle : IShape
{
    public Vector3 V0 { get; private set; }

    public Vector3 V1 { get; private set; }

    public Vector3 V2 { get; private set; }

    public Vector3 N0 { get; private set; }

    public Vector3 N1 { get; private set; }

    public Vector3 N2 { get; private set; }

    public Triangle(Vector3 v0, Vector3 v1, Vector3 v2)
    {
        V0 = v0;
        V1 = v1;
        V2 = v2;
    }

    public Triangle(Vector3 v0, Vector3 v1, Vector3 v2, Vector3 n0, Vector3 n1, Vector3 n2)
    {
        V0 = v0;
        V1 = v1;
        V2 = v2;
        N0 = n0;
        N1 = n1;
        N2 = n2;
    }

    public Vector3? Intersects(Ray ray)
    {
        const double tolerance = 1e-5d;

        var edge1 = V1 - V0;
        var edge2 = V2 - V0;

        var pvec = Vector3.Cross(ray.Dir, edge2);

        var det = Vector3.Dot(edge1, pvec);

        if (det > -tolerance && det < tolerance)
        {
            return null;
        }

        var invDet = 1f / det;

        var tvec = ray.Orig - V0;

        var u = Vector3.Dot(tvec, pvec) * invDet;

        if (u < 0 || u > 1)
        {
            return null;
        }

        var qvec = Vector3.Cross(tvec, edge1);

        var v = Vector3.Dot(ray.Dir, qvec) * invDet;

        if (v < 0 || u + v > 1)
        {
            return null;
        }

        var t = Vector3.Dot(edge2, qvec) * invDet;

        return t > tolerance ? ray.GetPoint(t) : null;
    }

    public Vector3 GetNormal(Vector3 intersectionPoint)
    {
        var edge1 = V1 - V0;
        var edge2 = V2 - V0;

        return Vector3.Cross(edge1, edge2).Normalize();
    }

    public Vector3 GetInterpolatedNormal(Vector3 intersectionPoint)
    {
        Vector3 barycentric = GetBarycentricCoordinates(intersectionPoint);
        Vector3 interpolatedNormal = (N0 * barycentric.X) +
                                     (N1 * barycentric.Y) +
                                     (N2 * barycentric.Z);

        interpolatedNormal = interpolatedNormal.Normalize();
        return interpolatedNormal;
    }

    public Vector3 GetBarycentricCoordinates(Vector3 point)
    {
        Vector3 edge1 = V1 - V0;
        Vector3 edge2 = V2 - V0;
        Vector3 edgep = point - V0;

        float d00 = Vector3.Dot(edge1, edge1);
        float d01 = Vector3.Dot(edge1, edge2);
        float d11 = Vector3.Dot(edge2, edge2);
        float d20 = Vector3.Dot(edgep, edge1);
        float d21 = Vector3.Dot(edgep, edge2);

        float denom = d00 * d11 - d01 * d01;

        float v = (d11 * d20 - d01 * d21) / denom;
        float w = (d00 * d21 - d01 * d20) / denom;
        float u = 1.0f - v - w;

        return new Vector3(u, v, w);

    }

    public void Transform(Transform transform)
    {
        V0 = V0.Transform(transform);
        V1 = V1.Transform(transform);
        V2 = V2.Transform(transform);
    }
}
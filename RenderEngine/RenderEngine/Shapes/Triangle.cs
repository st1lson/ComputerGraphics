using RenderEngine.Basic;
using RenderEngine.Interfaces;
using RenderEngine.Optimizers;
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

    public bool IsInsideBox(BoundingBox box)
    {
        Vector3 c = (box.Min + box.Max) * 0.5f;
        float e0 = (box.Max.X - box.Min.X) * 0.5f;
        float e1 = (box.Max.Y - box.Min.Y) * 0.5f;
        float e2 = (box.Max.Z - box.Min.Z) * 0.5f;

        Vector3 v0 = V0 - c;
        Vector3 v1 = V1 - c;
        Vector3 v2 = V2 - c;

        Vector3 f0 = V1 - V0;
        Vector3 f1 = V2 - V1;
        Vector3 f2 = V0 - V2;

        Vector3 a00 = new Vector3(0, -f0.Z, f0.Y);

        float p0 = Vector3.Dot(v0, a00);
        float p1 = Vector3.Dot(v1, a00);
        float p2 = Vector3.Dot(v2, a00);
        float r = e1 * Math.Abs(f0.Z) + e2 * Math.Abs(f0.Y);
        if (Math.Max(-Math.Max(p2, Math.Max(p0, p1)), Math.Min(Math.Min(p0, p1), p2)) > r)
        {
            return false;
        }

        Vector3 a01 = new Vector3(0, -f1.Z, f1.Y);

        p0 = Vector3.Dot(v0, a01);
        p1 = Vector3.Dot(v1, a01);
        p2 = Vector3.Dot(v2, a01);
        r = e1 * Math.Abs(f1.Z) + e2 * Math.Abs(f1.Y);
        if (Math.Max(-Math.Max(p0, Math.Max(p1, p2)), Math.Min(Math.Min(p0, p1), p2)) > r)
        {
            return false;
        }


        Vector3 a02 = new Vector3(0, -f2.Z, f2.Y);

        p0 = Vector3.Dot(v0, a02);
        p1 = Vector3.Dot(v1, a02);
        p2 = Vector3.Dot(v2, a02);
        r = e1 * Math.Abs(f2.Z) + e2 * Math.Abs(f2.Y);
        if (Math.Max(-Math.Max(p0, Math.Max(p1, p2)), Math.Min(Math.Min(p0, p1), p2)) > r)
        {
            return false;
        }

        Vector3 a10 = new Vector3(f0.Z, 0, -f0.X);

        p0 = Vector3.Dot(v0, a10);
        p1 = Vector3.Dot(v1, a10);
        p2 = Vector3.Dot(v2, a10);
        r = e0 * Math.Abs(f0.Z) + e2 * Math.Abs(f0.X);
        if (Math.Max(-Math.Max(p0, Math.Max(p1, p2)), Math.Min(Math.Min(p0, p1), p2)) > r)
        {
            return false;
        }

        Vector3 a11 = new Vector3(f1.Z, 0, -f1.X);

        p0 = Vector3.Dot(v0, a11);
        p1 = Vector3.Dot(v1, a11);
        p2 = Vector3.Dot(v2, a11);
        r = e0 * Math.Abs(f1.Z) + e2 * Math.Abs(f1.X);
        if (Math.Max(-Math.Max(p0, Math.Max(p1, p2)), Math.Min(Math.Min(p0, p1), p2)) > r)
        {
            return false;
        }

        Vector3 a12 = new Vector3(f2.Z, 0, -f2.X);

        p0 = Vector3.Dot(v0, a12);
        p1 = Vector3.Dot(v1, a12);
        p2 = Vector3.Dot(v2, a12);
        r = e0 * Math.Abs(f2.Z) + e2 * Math.Abs(f2.X);
        if (Math.Max(-Math.Max(p0, Math.Max(p1, p2)), Math.Min(Math.Min(p0, p1), p2)) > r)
        {
            return false;
        }

        Vector3 a20 = new Vector3(-f0.Y, f0.X, 0);

        p0 = Vector3.Dot(v0, a20);
        p1 = Vector3.Dot(v1, a20);
        p2 = Vector3.Dot(v2, a20);
        r = e0 * Math.Abs(f0.Y) + e1 * Math.Abs(f0.X);
        if (Math.Max(-Math.Max(p0, Math.Max(p1, p2)), Math.Min(Math.Min(p0, p1), p2)) > r)
        {
            return false;
        }

        Vector3 a21 = new Vector3(-f1.Y, f1.X, 0);

        p0 = Vector3.Dot(v0, a21);
        p1 = Vector3.Dot(v1, a21);
        p2 = Vector3.Dot(v2, a21);
        r = e0 * Math.Abs(f1.Y) + e1 * Math.Abs(f1.X);
        if (Math.Max(-Math.Max(p0, Math.Max(p1, p2)), Math.Min(Math.Min(p0, p1), p2)) > r)
        {
            return false;
        }

        Vector3 a22 = new Vector3(-f2.Y, f2.X, 0);

        p0 = Vector3.Dot(v0, a22);
        p1 = Vector3.Dot(v1, a22);
        p2 = Vector3.Dot(v2, a22);
        r = e0 * Math.Abs(f2.Y) + e1 * Math.Abs(f2.X);
        if (Math.Max(-Math.Max(p0, Math.Max(p1, p2)), Math.Min(Math.Min(p0, p1), p2)) > r)
        {
            return false;
        }


        if (Math.Max(v0.X, Math.Max(v1.X, v2.X)) < -e0 || Math.Min(v0.X, Math.Min(v1.X, v2.X)) > e0)
        {
            return false;
        }

        if (Math.Max(v0.Y, Math.Max(v1.Y, v2.Y)) < -e1 || Math.Min(v0.Y, Math.Min(v1.Y, v2.Y)) > e1)
        {
            return false;
        }

        if (Math.Max(v0.Z, Math.Max(v1.Z, v2.Z)) < -e2 || Math.Min(v0.Z, Math.Min(v1.Z, v2.Z)) > e2)
        {
            return false;
        }

        Vector3 plane_normal = Vector3.Cross(f0, f1);
        float plane_distance = Vector3.Dot(plane_normal, V0);

        r = e0 * Math.Abs(plane_normal.X) + e1 * Math.Abs(plane_normal.Y) +
            e2 * Math.Abs(plane_normal.Z);

        return true;
    }
}
using RenderEngine.Basic;
using RenderEngine.Optimizers;
using RenderEngine.Transformer;

namespace RenderEngine.Interfaces;

public interface IShape
{
    Vector3? Intersects(Ray ray);

    Vector3 GetNormal(Vector3 intersectionPoint);

    Vector3 GetInterpolatedNormal(Vector3 intersectionPoint);

    bool IsInsideBox(BoundingBox box);

    void Transform(Transform transform);
}
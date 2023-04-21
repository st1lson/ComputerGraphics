using RenderEngine.Basic;
using RenderEngine.Transformer;

namespace RenderEngine.Interfaces;

public interface IShape
{
    Vector3? Intersects(Ray ray);

    Vector3 GetNormal(Vector3 intersectionPoint);

    void Transform(Transform transform);
}
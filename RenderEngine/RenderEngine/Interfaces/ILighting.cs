using RenderEngine.Basic;
using RenderEngine.Models;
using RenderEngine.Transformer;

namespace RenderEngine.Interfaces;

public interface ILighting
{
    Pixel Color { get; set; }

    float Strength { get; set; }

    Pixel GetLight(IShape shape, IReadOnlyList<IShape> shapes, Vector3 intersectionPoint, Vector3 cameraPos, IOptimizer optimizer);

    void Transform(Transform transform);
}
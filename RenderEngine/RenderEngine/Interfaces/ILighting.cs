using RenderEngine.Basic;
using RenderEngine.Models;
using RenderEngine.Transformer;

namespace RenderEngine.Interfaces;

public interface ILighting
{
    Pixel Color { get; set; }

    Pixel GetLight(IShape shape, IReadOnlyList<IShape> shapes, Vector3 intersectionPoint, Vector3 cameraPos);

    void Transform(Transform transform);
}
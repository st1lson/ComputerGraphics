using RenderEngine.Basic;

namespace RenderEngine.Interfaces;

public interface ILighting
{
    Vector3 LightDir { get; set; }

    float GetLight(IShape shape, IReadOnlyList<IShape> shapes, Vector3 intersectionPoint, Vector3 cameraPos);
}
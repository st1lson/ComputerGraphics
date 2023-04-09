using RenderEngine.Basic;
using RenderEngine.Models;

namespace RenderEngine.Interfaces;

public interface ILighting
{
    Pixel Color { get; set; }

    Vector3 LightDir { get; set; }

    Pixel GetLight(IShape shape, IReadOnlyList<IShape> shapes, Vector3 intersectionPoint, Vector3 cameraPos);
}
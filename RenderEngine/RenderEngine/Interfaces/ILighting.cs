using RenderEngine.Basic;

namespace RenderEngine.Interfaces;

public interface ILighting
{
    Vector3 RayLight { get; set; }

    float GetLight(IShape shape, Vector3 intersectionPoint, Vector3 cameraPos);
}
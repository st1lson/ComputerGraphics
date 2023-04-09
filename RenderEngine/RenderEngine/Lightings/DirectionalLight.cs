using RenderEngine.Basic;
using RenderEngine.Interfaces;

namespace RenderEngine.Lightings;

public class DirectionalLight : ILighting
{
    public Vector3 RayLight { get; set; }

    public DirectionalLight(Vector3 rayLight)
    {
        RayLight = -rayLight.Normalize();
    }

    public float GetLight(IShape shape, Vector3 intersectionPoint, Vector3 cameraPos)
    {
        Vector3 normal = shape.GetNormal(intersectionPoint).Normalize();
        float cosA = Vector3.Dot(normal, RayLight);
        float cosB = Vector3.Dot(normal, cameraPos - intersectionPoint);

        if ((cosA > 0 && cosB < 0) || (cosA < 0 && cosB < 0)) 
        {
            normal = -normal;
        }

        return Vector3.Dot(normal, RayLight);
    }
}
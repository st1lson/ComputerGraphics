using RenderEngine.Basic;
using RenderEngine.Interfaces;

namespace RenderEngine.Lightings;

public class DirectionalLight : ILighting
{
    public Vector3 LightDir { get; set; }

    public DirectionalLight(Vector3 rayLight)
    {
        LightDir = -rayLight.Normalize();
    }

    public float GetLight(IShape shape, IReadOnlyList<IShape> shapes, Vector3 intersectionPoint, Vector3 cameraPos)
    {
        Vector3 normal = shape.GetNormal(intersectionPoint).Normalize();
        float cosA = Vector3.Dot(normal, LightDir);
        float cosB = Vector3.Dot(normal, cameraPos - intersectionPoint);

        if ((cosA > 0.00001f && cosB < -0.00001f) || (cosA < -0.00001f && cosB < -0.00001f)) 
        {
            normal = -normal;
        }

        Ray rayLight = new Ray(intersectionPoint, LightDir);
        bool isShadowed = false;
        foreach (IShape otherShapes in shapes)
        {
            if (otherShapes == shape) 
            {
                continue;
            }

            var intersection = otherShapes.Intersects(rayLight);
            if (intersection != null) 
            {
                isShadowed = true;
                break;
            }
        }

        return isShadowed ? 0 : Vector3.Dot(normal, LightDir);
    }
}
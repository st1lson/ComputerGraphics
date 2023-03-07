using RenderEngine.Basic;
using RenderEngine.Interfaces;

namespace RenderEngine.Lightings
{
    public class DirectionalLight : ILighting
    {
        public Vector3 RayLight { get; set; }
        public DirectionalLight(Vector3 rayLight)
        {
            RayLight = -rayLight.Normalize();
        }

        public float GetLight(IShape shape, Vector3 intersectionPoint)
        {
            Vector3 normal = shape.GetNormal(intersectionPoint).Normalize();

            return Vector3.Dot(normal, RayLight);
        }
    }
}

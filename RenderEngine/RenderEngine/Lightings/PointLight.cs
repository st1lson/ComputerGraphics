using RenderEngine.Basic;
using RenderEngine.Interfaces;
using RenderEngine.Models;
using RenderEngine.Transformer;

namespace RenderEngine.Lightings
{
    public class PointLight : ILighting
    {
        public Vector3 LightPos { get; set; }

        public Pixel Color { get; set; } = new Pixel(255);

        public float Strength { get; set; } = 1;

        private const float Threshold = 0.00001f;

        public PointLight(Vector3 posLight)
        {
            LightPos = posLight;
        }

        public PointLight(Vector3 posLight, Pixel color, float strength)
        {
            LightPos = posLight;
            Color = color;
            Strength = strength;
        }

        public Pixel GetLight(IShape shape, IReadOnlyList<IShape> shapes, Vector3 intersectionPoint, Vector3 cameraPos)
        {
            Vector3 normal = shape.GetNormal(intersectionPoint).Normalize();
            Vector3 lightDir = LightPos - intersectionPoint;
            float cosA = Vector3.Dot(normal, lightDir);
            float cosB = Vector3.Dot(normal, cameraPos - intersectionPoint);

            if ((cosA > Threshold && cosB < -Threshold) || (cosA < -Threshold && cosB < -Threshold))
            {
                normal = -normal;
            }

            Ray rayLight = new Ray(intersectionPoint, lightDir);
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

            var coefficient = isShadowed ? 0 : Math.Max(Vector3.Dot(normal, lightDir), 0);

            return Color * (new Vector3(coefficient) * Strength);
        }

        public void Transform(Transform transform)
        {
            LightPos = LightPos.Transform(transform);
        }
    }
}

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

        private Vector3 ambientCoef = new Vector3(0.2f);

        private Vector3 diffuseCoef = new Vector3(0.5f);

        private Vector3 specularCoef = new Vector3(0.5f);

        private float shininess = 2;

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

        public PointLight(Vector3 posLight, Pixel color, float strength, Vector3 ambient, Vector3 diffuse, Vector3 specular, float shine)
        {
            LightPos = posLight;
            Color = color;
            Strength = strength;
            ambientCoef = ambient;
            diffuseCoef = diffuse;
            specularCoef = specular;
            shininess = shine;
        }

        public Pixel GetLight(IShape shape, IReadOnlyList<IShape> shapes, Vector3 intersectionPoint, Vector3 cameraPos)
        {
            Vector3 normal = shape.GetNormal(intersectionPoint).Normalize();
            Vector3 lightDir = (LightPos - intersectionPoint).Normalize();
            Vector3 viewVector = (cameraPos - intersectionPoint).Normalize();
            float cosA = Vector3.Dot(normal, lightDir);
            float cosB = Vector3.Dot(normal, viewVector);

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

            float diffuseFactor = Math.Max(Vector3.Dot(normal, lightDir), 0);
            
            Vector3 diffuseComponent = diffuseCoef * diffuseFactor;

            Vector3 reflectionVector = -lightDir - normal * Vector3.Dot(-lightDir, normal) * 2;
            float specularFactor = (float)Math.Pow(Math.Max(Vector3.Dot(reflectionVector, viewVector), 0), shininess);
            Vector3 specularComponent = specularCoef * specularFactor;

            Vector3 finalColor = isShadowed ? Vector3.Zero : (ambientCoef + diffuseComponent + specularComponent) * Strength;
            finalColor = Vector3.Clamp(finalColor, Vector3.Zero, new Vector3(1));

            return Color * finalColor;
        }

        public void Transform(Transform transform)
        {
            LightPos = LightPos.Transform(transform);
        }
    }
}

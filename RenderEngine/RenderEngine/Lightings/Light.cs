using RenderEngine.Basic;
using RenderEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderEngine.Lightings
{
    public class Light : ILighting
    {
        public Vector3 RayLight { get; set; }
        public Light(Vector3 rayLight)
        {
            RayLight = rayLight;
        }

        public float GetLight(IShape shape, Vector3? intersectionPoint)
        {
            Vector3 normal = shape.GetNormal(intersectionPoint);
            return Vector3.Dot(normal, RayLight);
        }
    }
}

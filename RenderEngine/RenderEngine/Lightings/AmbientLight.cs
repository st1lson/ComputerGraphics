using RenderEngine.Basic;
using RenderEngine.Interfaces;
using RenderEngine.Models;
using RenderEngine.Transformer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderEngine.Lightings
{
    public class AmbientLight : ILighting
    {
        public Pixel Color { get; set; } = new Pixel(255);

        public float Strength { get; set; } = 1;

        private const float Threshold = 0.00001f;

        public AmbientLight(Pixel color, float stength)
        {
            Color = color;
            Strength = stength;
        }

        public Pixel GetLight(IShape shape, IReadOnlyList<IShape> shapes, Vector3 intersectionPoint, Vector3 cameraPos, IOptimizer optimizer)
        {
            var coefficient = 1;

            return Color * (new Vector3(coefficient) * Strength);
        }

        public void Transform(Transform transform)
        {
        }
    }
}

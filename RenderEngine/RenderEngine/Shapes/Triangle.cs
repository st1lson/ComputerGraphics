using System;
using RenderEngine.Basic;
using RenderEngine.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderEngine.Shapes
{
    public record Triangle : IShape
    {
        public Point3 v0 { get; init; }
        public Point3 v1 { get; init; }
        public Point3 v2 { get; init; }

        public Vector3? Intersects(Ray ray)
        {
            var p = v0 - v1;
            return null;
        }
    }
}

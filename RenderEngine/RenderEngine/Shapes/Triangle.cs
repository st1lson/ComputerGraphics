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
        public Vector3 v0 { get; init; }
        public Vector3 v1 { get; init; }
        public Vector3 v2 { get; init; }

        public Triangle(Vector3 v0, Vector3 v1, Vector3 v2)
        {
            this.v0 = v0;
            this.v1 = v1;
            this.v1 = v1;
        }
        public Vector3? Intersects(Ray ray)
        {
            const double tolerance = 1e-5d;

            var edge1 = v1- v0;
            var edge2 = v2 - v0;

            var pvec = Vector3.Cross(ray.Dir, edge2);

            var det = Vector3.Dot(edge1, pvec);

            if (det > -tolerance && det < tolerance)
            {
                return null;
            }

            var invDet = 1d / det;

            var tvec = ray.Orig - v0;

            var u = Vector3.Dot(tvec, pvec) * invDet;

            if (u < 0 || u > 1)
            {
                return null;
            }

            var qvec = Vector3.Cross(tvec, edge1);

            var v = Vector3.Dot(ray.Dir, qvec) * invDet;

            if (v < 0 || u + v > 1)
            {
                return null;
            }

            var t = Vector3.Dot(edge2, qvec) * invDet;

            return new Vector3((float)t, (float)u, (float)v);
        }
    }
}

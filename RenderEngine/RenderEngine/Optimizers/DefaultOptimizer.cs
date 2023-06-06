using RenderEngine.Basic;
using RenderEngine.Core.Scenes;
using RenderEngine.Core;
using RenderEngine.Interfaces;
using RenderEngine.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderEngine.Optimizers
{
    public class DefaultOptimizer : IOptimizer
    {
        private IReadOnlyList<IShape> _shapes = null!;

        public void Build(IReadOnlyList<IShape> shapes)
        {
            _shapes = shapes;
        }

        public (Vector3?, IShape?) GetIntersection(Ray ray, Vector3 cameraOrig)
        {
            Vector3? intersectionPoint = null;
            float minSquareDistance = float.MaxValue;
            IShape? saveShape = null;
            foreach (var shape in _shapes)
            {
                var intersection = shape.Intersects(ray);
                if (intersection == null)
                {
                    continue;
                }

                var squareDist = Vector3.Dot(intersection.Value - cameraOrig,
                    intersection.Value - cameraOrig);
                if (squareDist < minSquareDistance)
                {
                    intersectionPoint = intersection;
                    minSquareDistance = squareDist;
                    saveShape = shape;
                }
            }

            return (intersectionPoint, saveShape);
        }
    }
}

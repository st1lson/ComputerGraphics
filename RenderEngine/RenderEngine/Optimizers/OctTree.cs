using RenderEngine.Basic;
using RenderEngine.Core;
using RenderEngine.Interfaces;
using RenderEngine.Shapes;

namespace RenderEngine.Optimizers
{
    public class OctTree : IOptimizer
    {
        private const float MinSize = .0005f;
        private readonly BoundingBox _region;
        private readonly OctTree[] _children;
        private List<IShape> _shapes;

        public OctTree(BoundingBox max_region)
        {
            _region = max_region;
            _children = new OctTree[8];
        }

        private OctTree(BoundingBox max_region, List<IShape> shapes)
        {
            _region = max_region;
            _children = new OctTree[8];
            Build(shapes);
        }

        public void Build(IReadOnlyList<IShape> shapes)
        {
            _shapes = new(shapes);

            if (_shapes.Count <= 10)
            {
                return;
            }

            Vector3 dimensions = _region.Max - _region.Min;

            if (dimensions.Abs() <= MinSize)
            {
                return;
            }

            Vector3 half = dimensions / 2;
            Vector3 center = _region.Min + half;

            BoundingBox[] child_regions = new BoundingBox[8];
            child_regions[0] = new BoundingBox(_region.Min, center);
            child_regions[1] = new BoundingBox(new Vector3(center.X, _region.Min.Y, _region.Min.Z),
                new Vector3(_region.Max.X, center.Y, center.Z));
            child_regions[2] = new BoundingBox(new Vector3(center.X, _region.Min.Y, center.Z),
                new Vector3(_region.Max.X, center.Y, _region.Max.Z));
            child_regions[3] = new BoundingBox(new Vector3(_region.Min.X, _region.Min.Y, center.Z),
                new Vector3(center.X, center.Y, _region.Max.Z));
            child_regions[4] = new BoundingBox(new Vector3(_region.Min.X, center.Y, _region.Min.Z),
                new Vector3(center.X, _region.Max.Y, center.Z));
            child_regions[5] = new BoundingBox(new Vector3(center.X, center.Y, _region.Min.Z),
                new Vector3(_region.Max.X, _region.Max.Y, center.Z));
            child_regions[6] = new BoundingBox(center, _region.Max);
            child_regions[7] = new BoundingBox(new Vector3(_region.Min.X, center.Y, center.Z),
                new Vector3(center.X, _region.Max.Y, _region.Max.Z));

            List<IShape>[] _regionsElements = new List<IShape>[8];
            for (int i = 0; i < 8; i++)
            {
                _regionsElements[i] = new List<IShape>();
            }

            List<IShape> nextElements = new();

            foreach (IShape shape in _shapes)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (shape.IsInsideBox(child_regions[i]))
                    {
                        _regionsElements[i].Add(shape);
                    }
                }
            }

            _shapes.Clear();

            for (int i = 0; i < 8; i++)
            {
                if (_regionsElements[i].Count != 0)
                {
                    _children[i] = CreateNode(child_regions[i], _regionsElements[i]);
                }
            }
        }

        public (Vector3?, IShape?) GetIntersection(Ray ray, Vector3 cameraOrig)
        {
            bool all_childrenNull = true;

            foreach (OctTree child in _children)
            {
                if (child is not null)
                {
                    all_childrenNull = false;
                }
            }

            if (all_childrenNull && _shapes.Count == 0)
            {
                return (null, null);
            }

            Vector3? minIntersectedPoint = null;
            float minSquareDistance = float.MaxValue;
            IShape? saveShape = null;
            foreach (IShape shape in _shapes)
            {
                Vector3? intersectedPoint = shape.Intersects(ray);

                if (intersectedPoint == null)
                {
                    continue;
                }

                var squareDist = Vector3.Dot(intersectedPoint.Value - cameraOrig,
                    intersectedPoint.Value - cameraOrig);

                if (squareDist < minSquareDistance)
                {
                    minIntersectedPoint = intersectedPoint.Value;
                    minSquareDistance = squareDist;
                    saveShape = shape;
                }
            }

            for (int i = 0; i < 8; i++)
            {
                if (_children[i] != null && _children[i]._region.Intersects(ray))
                {
                    (Vector3? intersectedPoint, IShape? shape) = _children[i].GetIntersection(ray, cameraOrig);

                    if (intersectedPoint == null)
                    {
                        continue;
                    }

                    var squareDist = Vector3.Dot(intersectedPoint.Value - cameraOrig,
                        intersectedPoint.Value - cameraOrig);

                    if (squareDist < minSquareDistance)
                    {
                        minIntersectedPoint = intersectedPoint.Value;
                        minSquareDistance = squareDist;
                        saveShape = shape;
                    }
                }
            }

            return (minIntersectedPoint, saveShape);
        }

        private OctTree CreateNode(BoundingBox _region, List<IShape> shapes)
        {
            OctTree tree = new(_region, shapes);
            return tree;
        }
    }
}

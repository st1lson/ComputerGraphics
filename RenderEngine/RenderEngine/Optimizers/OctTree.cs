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
        private List<Triangle> _triangles;

        public OctTree(BoundingBox max_region)
        {
            _region = max_region;
            _children = new OctTree[8];
        }

        private OctTree(BoundingBox max_region, List<Triangle> triangle)
        {
            //_triangles = triangle;
            _region = max_region;
            _children = new OctTree[8];
            Build(triangle);
        }

        public void Build(IReadOnlyList<IShape> Triangles)
        {
            if (Triangles.Any(x=>x is not Triangle))
            {
                throw new Exception("Triangles not found");
            }

            _triangles = new(Triangles.Cast<Triangle>().ToList());

            if (_triangles.Count <= 10)
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

            List<Triangle>[] _regionsElements = new List<Triangle>[8];
            for (int i = 0; i < 8; i++)
            {
                _regionsElements[i] = new List<Triangle>();
            }

            List<Triangle> nextElements = new();

            foreach (Triangle triangle in _triangles)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (IsFaceInsideBox(triangle, child_regions[i]))
                    {
                        _regionsElements[i].Add(triangle);
                    }
                }
            }

            _triangles.Clear();

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

            if (all_childrenNull && _triangles.Count == 0)
            {
                return (null, null);
            }

            Vector3? minIntersectedPoint = null;
            float minSquareDistance = float.MaxValue;
            IShape? saveShape = null;
            foreach (Triangle triangle in _triangles)
            {
                Vector3? intersectedPoint = triangle.Intersects(ray);

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
                    saveShape = triangle;
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

        private OctTree CreateNode(BoundingBox _region, List<Triangle> _triangle)
        {
            OctTree tree = new(_region, _triangle);
            return tree;
        }

        private static bool IsFaceInsideBox(Triangle f, BoundingBox box)//book - Real-Time Collision Detection
        {
            Vector3 c = (box.Min + box.Max) * 0.5f;
            float e0 = (box.Max.X - box.Min.X) * 0.5f;
            float e1 = (box.Max.Y - box.Min.Y) * 0.5f;
            float e2 = (box.Max.Z - box.Min.Z) * 0.5f;

            Vector3 v0 = f.V0 - c;
            Vector3 v1 = f.V1 - c;
            Vector3 v2 = f.V2 - c;

            Vector3 f0 = f.V1 - f.V0;
            Vector3 f1 = f.V2 - f.V1;
            Vector3 f2 = f.V0 - f.V2;

            Vector3 a00 = new Vector3(0, -f0.Z, f0.Y);

            float p0 = Vector3.Dot(v0, a00);
            float p1 = Vector3.Dot(v1, a00);
            float p2 = Vector3.Dot(v2, a00);
            float r = e1 * Math.Abs(f0.Z) + e2 * Math.Abs(f0.Y);
            if (Math.Max(-Math.Max(p2, Math.Max(p0, p1)), Math.Min(Math.Min(p0, p1), p2)) > r)
            {
                return false;
            }

            Vector3 a01 = new Vector3(0, -f1.Z, f1.Y);

            p0 = Vector3.Dot(v0, a01);
            p1 = Vector3.Dot(v1, a01);
            p2 = Vector3.Dot(v2, a01);
            r = e1 * Math.Abs(f1.Z) + e2 * Math.Abs(f1.Y);
            if (Math.Max(-Math.Max(p0, Math.Max(p1, p2)), Math.Min(Math.Min(p0, p1), p2)) > r)
            {
                return false;
            }


            Vector3 a02 = new Vector3(0, -f2.Z, f2.Y);

            p0 = Vector3.Dot(v0, a02);
            p1 = Vector3.Dot(v1, a02);
            p2 = Vector3.Dot(v2, a02);
            r = e1 * Math.Abs(f2.Z) + e2 * Math.Abs(f2.Y);
            if (Math.Max(-Math.Max(p0, Math.Max(p1, p2)), Math.Min(Math.Min(p0, p1), p2)) > r)
            {
                return false;
            }

            Vector3 a10 = new Vector3(f0.Z, 0, -f0.X);

            p0 = Vector3.Dot(v0, a10);
            p1 = Vector3.Dot(v1, a10);
            p2 = Vector3.Dot(v2, a10);
            r = e0 * Math.Abs(f0.Z) + e2 * Math.Abs(f0.X);
            if (Math.Max(-Math.Max(p0, Math.Max(p1, p2)), Math.Min(Math.Min(p0, p1), p2)) > r)
            {
                return false;
            }

            Vector3 a11 = new Vector3(f1.Z, 0, -f1.X);

            p0 = Vector3.Dot(v0, a11);
            p1 = Vector3.Dot(v1, a11);
            p2 = Vector3.Dot(v2, a11);
            r = e0 * Math.Abs(f1.Z) + e2 * Math.Abs(f1.X);
            if (Math.Max(-Math.Max(p0, Math.Max(p1, p2)), Math.Min(Math.Min(p0, p1), p2)) > r)
            {
                return false;
            }

            Vector3 a12 = new Vector3(f2.Z, 0, -f2.X);

            p0 = Vector3.Dot(v0, a12);
            p1 = Vector3.Dot(v1, a12);
            p2 = Vector3.Dot(v2, a12);
            r = e0 * Math.Abs(f2.Z) + e2 * Math.Abs(f2.X);
            if (Math.Max(-Math.Max(p0, Math.Max(p1, p2)), Math.Min(Math.Min(p0, p1), p2)) > r)
            {
                return false;
            }

            Vector3 a20 = new Vector3(-f0.Y, f0.X, 0);

            p0 = Vector3.Dot(v0, a20);
            p1 = Vector3.Dot(v1, a20);
            p2 = Vector3.Dot(v2, a20);
            r = e0 * Math.Abs(f0.Y) + e1 * Math.Abs(f0.X);
            if (Math.Max(-Math.Max(p0, Math.Max(p1, p2)), Math.Min(Math.Min(p0, p1), p2)) > r)
            {
                return false;
            }

            Vector3 a21 = new Vector3(-f1.Y, f1.X, 0);

            p0 = Vector3.Dot(v0, a21);
            p1 = Vector3.Dot(v1, a21);
            p2 = Vector3.Dot(v2, a21);
            r = e0 * Math.Abs(f1.Y) + e1 * Math.Abs(f1.X);
            if (Math.Max(-Math.Max(p0, Math.Max(p1, p2)), Math.Min(Math.Min(p0, p1), p2)) > r)
            {
                return false;
            }

            Vector3 a22 = new Vector3(-f2.Y, f2.X, 0);

            p0 = Vector3.Dot(v0, a22);
            p1 = Vector3.Dot(v1, a22);
            p2 = Vector3.Dot(v2, a22);
            r = e0 * Math.Abs(f2.Y) + e1 * Math.Abs(f2.X);
            if (Math.Max(-Math.Max(p0, Math.Max(p1, p2)), Math.Min(Math.Min(p0, p1), p2)) > r)
            {
                return false;
            }


            if (Math.Max(v0.X, Math.Max(v1.X, v2.X)) < -e0 || Math.Min(v0.X, Math.Min(v1.X, v2.X)) > e0)
            {
                return false;
            }

            if (Math.Max(v0.Y, Math.Max(v1.Y, v2.Y)) < -e1 || Math.Min(v0.Y, Math.Min(v1.Y, v2.Y)) > e1)
            {
                return false;
            }

            if (Math.Max(v0.Z, Math.Max(v1.Z, v2.Z)) < -e2 || Math.Min(v0.Z, Math.Min(v1.Z, v2.Z)) > e2)
            {
                return false;
            }

            Vector3 plane_normal = Vector3.Cross(f0, f1);
            float plane_distance = Vector3.Dot(plane_normal, f.V0);

            r = e0 * Math.Abs(plane_normal.X) + e1 * Math.Abs(plane_normal.Y) +
                e2 * Math.Abs(plane_normal.Z);

            return true;
        }
    }
}

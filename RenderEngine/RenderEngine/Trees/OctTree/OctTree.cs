using RenderEngine.Basic;
using RenderEngine.Shapes;

namespace RenderEngine.Trees.OctTree
{
    internal class OctTree
    {
        private const float MinSize = .0005f;
        public BoundingBox Region { get; init; }
        public OctTree[] Children { get; init; }
        public List<Triangle> Triangles { get; init; }

        public OctTree(BoundingBox maxRegion, IEnumerable<Triangle> triangles)
        {
            Region = maxRegion;
            Triangles = triangles.ToList();
            Children = new OctTree[8];
        }

        private void BuildTree()
        {
            if (Triangles.Count <= 10)
            {
                return;
            }

            Vector3 dimensions = Region.Max - Region.Min;

            if (dimensions.Abs() <= MinSize)
            {
                return;
            }

            Vector3 half = dimensions / 2;
            Vector3 center = Region.Min + half;

            BoundingBox[] childRegions = new BoundingBox[8];
            childRegions[0] = new BoundingBox(Region.Min, center);
            childRegions[1] = new BoundingBox(new Vector3(center.X, Region.Min.Y, Region.Min.Z),
                new Vector3(Region.Max.X, center.Y, center.Z));
            childRegions[2] = new BoundingBox(new Vector3(center.X, Region.Min.Y, center.Z),
                new Vector3(Region.Max.X, center.Y, Region.Max.Z));
            childRegions[3] = new BoundingBox(new Vector3(Region.Min.X, Region.Min.Y, center.Z),
                new Vector3(center.X, center.Y, Region.Max.Z));
            childRegions[4] = new BoundingBox(new Vector3(Region.Min.X, center.Y, Region.Min.Z),
                new Vector3(center.X, Region.Max.Y, center.Z));
            childRegions[5] = new BoundingBox(new Vector3(center.X, center.Y, Region.Min.Z),
                new Vector3(Region.Max.X, Region.Max.Y, center.Z));
            childRegions[6] = new BoundingBox(center, Region.Max);
            childRegions[7] = new BoundingBox(new Vector3(Region.Min.X, center.Y, center.Z),
                new Vector3(center.X, Region.Max.Y, Region.Max.Z));

            List<Triangle>[] regionsElements = new List<Triangle>[8];
            for (int i = 0; i < 8; i++)
            {
                regionsElements[i] = new List<Triangle>();
            }

            List<Triangle> nextElements = new();

            foreach (Triangle triangle in Triangles)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (IsFaceInsideBox(triangle, childRegions[i]))
                    {
                        regionsElements[i].Add(triangle);
                    }
                }
            }

            Triangles.Clear();

            for (int i = 0; i < 8; i++)
            {
                if (regionsElements[i].Count != 0)
                {
                    Children[i] = CreateNode(childRegions[i], regionsElements[i]);
                }
            }
        }

        public List<Vector3>? GetIntersection(Ray ray)
        {
            bool allChildrenNull = true;

            foreach (OctTree child in Children)
            {
                if (child is not null)
                {
                    allChildrenNull = false;
                }
            }

            if (allChildrenNull && Triangles.Count == 0)
            {
                return null;
            }

            List<Vector3> intersectedPoints = new();

            foreach (Triangle triangle in Triangles)
            {
                Vector3? intersectedPoint = triangle.Intersects(ray);

                if (intersectedPoint != null)
                {
                    intersectedPoints.Add(intersectedPoint.Value);
                }
            }

            for (int i = 0; i < 8; i++)
            {
                if (Children[i] != null && Children[i].Region.Intersects(ray))
                {
                    List<Vector3>? intersections = Children[i].GetIntersection(ray);
                    if (intersections != null)
                    {
                        intersectedPoints.AddRange(intersections);
                    }
                }
            }

            return intersectedPoints;
        }

        private OctTree CreateNode(BoundingBox region, IEnumerable<Triangle> triangles)
        {
            OctTree tree = new(region, triangles);
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

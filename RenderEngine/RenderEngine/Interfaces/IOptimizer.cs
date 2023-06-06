using RenderEngine.Basic;

namespace RenderEngine.Interfaces
{
    public interface IOptimizer
    {
        (Vector3?, IShape?) GetIntersection(Ray ray, Vector3 cameraOrig);

        void Build(IReadOnlyList<IShape> shapes);
    }
}

using RenderEngine.Basic;

namespace RenderEngine.Interfaces
{
    public interface IShape
    {
        Vector3? Intersects(Ray ray);
    }
}

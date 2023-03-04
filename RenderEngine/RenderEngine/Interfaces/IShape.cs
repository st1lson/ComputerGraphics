using RenderEngine.Basic;

namespace RenderEngine.Interfaces
{
    public interface IShape
    {
        float Intersects(Ray ray);
    }
}

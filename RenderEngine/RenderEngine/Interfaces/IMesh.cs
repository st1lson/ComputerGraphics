using RenderEngine.Basic;

namespace RenderEngine.Interfaces;

public interface IMesh
{
    List<Vector3> Vertices { get; }

    List<IShape> Faces { get; }
}

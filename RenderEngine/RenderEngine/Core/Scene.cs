using RenderEngine.Interfaces;

namespace RenderEngine.Core;

public class Scene
{
    public IReadOnlyList<IShape> Shapes { get; init; }

    public IReadOnlyList<ILighting> Lighting { get; init; }

    public Scene(IReadOnlyList<IShape> shapes, IReadOnlyList<ILighting> lighting)
    {
        Shapes = shapes;
        Lighting = lighting;
    }
}
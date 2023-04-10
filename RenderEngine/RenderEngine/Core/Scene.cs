using RenderEngine.Interfaces;
using RenderEngine.Transformer;

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

    public void Transform(Transform transform)
    {
        foreach(var shape in Shapes)
        {
            shape.Transform(transform);
        }

        foreach (var light in Lighting)
        {
            light.Transform(transform);
        }

    }
}
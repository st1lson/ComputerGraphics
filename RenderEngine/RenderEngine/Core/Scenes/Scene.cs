using RenderEngine.Interfaces;
using RenderEngine.IO.Readers;
using RenderEngine.Transformer;

namespace RenderEngine.Core.Scenes;

public class Scene
{
    public IReadOnlyList<IShape> Shapes { get; init; }

    public IReadOnlyList<Transform>? Transforms { get; init; }

    public IReadOnlyList<ILighting> Lighting { get; init; }


    public Scene(IReadOnlyList<IShape> shapes, IReadOnlyList<ILighting> lighting, IReadOnlyList<Transform>? transforms = null)
    {
        Shapes = shapes;
        Lighting = lighting;
        Transforms = transforms;
    }
}
using RenderEngine.Basic;
using RenderEngine.Cli.CommandLineCommands;
using RenderEngine.Cli.IO.Readers;
using RenderEngine.Cli.IO.Writers;
using RenderEngine.Core;
using RenderEngine.DependencyInjection;
using RenderEngine.Interfaces;
using RenderEngine.Lightings;
using RenderEngine.Transformer;

namespace RenderEngine.Cli;

internal sealed class RenderStartup : IService
{
    private readonly RenderCommand _command;

    [Service]
    private readonly ImageWriter _imageWriter = null!;

    [Service]
    private readonly ObjReader _objReader = null!;

    public RenderStartup(RenderCommand command)
    {
        _command = command;
    }

    public void Run()
    {
        Camera camera = new Camera(
            new Vector3(0, 1, 0),
            new Vector3(0, -1, 0),
            200,
            200,
            1,
            30
        );

        List<IShape> shapes = _objReader.Read(_command.SourceFile);

        Transform transform = new Transform(Transform.IdentityMatrix).Translate(new Vector3(0.5f, 0, 0));

        foreach (var shape in shapes)
        {
            shape.Transform(transform);
        }

        var lighting = new List<ILighting>
        {
            new DirectionalLight(new Vector3(0, -1, 0)),

            //new DirectionalLight(new Vector3(1, -1, 0), new Pixel(255, 0, 0)),
            //new DirectionalLight(new Vector3(-1, -1, 0), new Pixel(0, 0, 255))
        };

        var scene = new Scene(shapes, lighting);

        var renderer = new Renderer(camera, scene);
        var image = renderer.Render();

        _imageWriter.Write(image, _command);
    }
}

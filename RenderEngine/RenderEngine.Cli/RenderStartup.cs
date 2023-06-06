using RenderEngine.Basic;
using RenderEngine.Cli.CommandLineCommands;
using RenderEngine.Cli.Configurations;
using RenderEngine.Cli.IO.Readers;
using RenderEngine.Cli.IO.Writers;
using RenderEngine.Core;
using RenderEngine.Core.Scenes;
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
    private readonly SceneFactory _sceneFactory = null!;

    [Service]
    private readonly ObjReader _objReader = null!;

    public RenderStartup(RenderCommand command)
    {
        _command = command;
    }

    public void Run()
    {
        (Camera camera, Scene scene) = _sceneFactory.CreateScene(_command, _objReader);

        Renderer _renderer = new Renderer(camera, scene);

        var image = _renderer.Render();

        _imageWriter.Write(image, _command);
    }
}

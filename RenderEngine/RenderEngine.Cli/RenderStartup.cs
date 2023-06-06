using RenderEngine.Basic;
using RenderEngine.Cli.CommandLineCommands;
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

    private readonly Renderer _renderer;

    public RenderStartup(RenderCommand command, Renderer renderer)
    {
        _command = command;
        _renderer = renderer;
    }

    public void Run()
    {
        var image = _renderer.Render();

        _imageWriter.Write(image, _command);
    }
}

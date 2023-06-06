using CommandLine;
using RenderEngine.Basic;
using RenderEngine.Cli.CommandLineCommands;
using RenderEngine.Cli.Configurations;
using RenderEngine.Cli.IO.Readers;
using RenderEngine.Cli.IO.Writers;
using RenderEngine.Core;
using RenderEngine.Core.Scenes;
using RenderEngine.DependencyInjection;
using RenderEngine.ImageConverter.Factories;
using RenderEngine.Interfaces;
using RenderEngine.Lightings;
using RenderEngine.Transformer;

namespace RenderEngine.Cli;

internal class Program
{
    private static void Main(string[] args)
    {
        var result = Parser.Default.ParseArguments(args, typeof(RenderCommand), typeof(ConvertCommand))
            .MapResult(
                (RenderCommand command) => Render(command),
                (ConvertCommand command) => Convert(command),
                _ => 1
            );

        Environment.Exit(result);
    }

    private static int Render(RenderCommand command)
    {
        var builder = new ContainerBuilder();

        (Camera camera, Scene scene) = new SceneFactory(new ObjReader()).CreateScene(command);

        Renderer renderer = new Renderer(camera, scene);

        builder
            .AddSingleton<ImageWriter>()
            .AddSingleton<RenderStartup>(() => new RenderStartup(command, renderer));

        using var container = builder.Build();

        container.GetService<RenderStartup>().Run();

        return 0;
    }

    private static int Convert(ConvertCommand command)
    {
        var builder = new ContainerBuilder();

        builder
            .AddSingleton<PluginFactory>()
            .AddSingleton<ConverterApp>(() => new ConverterApp(command));

        using var container = builder.Build();

        container.GetService<ConverterApp>().Run();

        return 0;
    }
}
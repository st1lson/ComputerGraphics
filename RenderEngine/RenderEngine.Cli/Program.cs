using CommandLine;
using RenderEngine.Basic;
using RenderEngine.Cli.CommandLineCommands;
using RenderEngine.Cli.IO.Readers;
using RenderEngine.Cli.IO.Writers;
using RenderEngine.Core;
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

        builder
            .AddSingleton<ObjReader>()
            .AddSingleton<ImageWriter>()
            .AddSingleton<RenderStartup>(() => new RenderStartup(command));

        using var container = builder.Build();

        container.GetService<RenderStartup>().Run();

        return 0;
    }

    private static int Convert(ConvertCommand command)
    {
        var pluginFactory = new PluginFactory();

        using var stream = File.Open(command.SourceFile, FileMode.Open);
        var reader = pluginFactory.GetImageReader(stream);

        var bitmap = reader.Read(stream);

        var writer = pluginFactory.GetImageWriter(command.OutputFormat);

        writer.Write(bitmap, command.OutputFile);

        return 0;
    }
}
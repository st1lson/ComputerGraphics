using CommandLine;
using RenderEngine.Basic;
using RenderEngine.Cli.CommandLineCommands;
using RenderEngine.Cli.IO.Writers;
using RenderEngine.Core;
using RenderEngine.ImageConverter.Factories;
using RenderEngine.Interfaces;
using RenderEngine.Lightings;
using RenderEngine.Shapes;

namespace RenderEngine.Cli;

internal class Program
{
    private static void Main(string[] args)
    {
        var result = Parser.Default.ParseArguments(args, typeof(RenderCommand), typeof(ConvertCommand))
            .MapResult(
                (RenderCommand command) => Render(command),
                (ConvertCommand command) => Convert(command),
                _ => throw new Exception("Wrong command")
            );

        Environment.Exit(result);
    }

    private static int Render(RenderCommand command)
    {
        Camera camera = new Camera(
            Vector3.Zero,
            new Vector3(0, 1, 0),
            1920,
            1080,
            1,
            30
        );

        List<IShape> shapes = new List<IShape>()
        {
            new Sphere(new Vector3(0, 5, -1.5f), 1),
            new Disk(new Vector3(1.5f, 5, 1.5f), 2, new Vector3(1, -1, 1)),
            new Triangle(new Vector3(0, 5, 0), new Vector3(2.5f, 5, 0), new Vector3(0, 5, 2.5f))
        };

        List<ILighting> lighting = new List<ILighting>
        {
            new DirectionalLight(new Vector3(0, 1, 0))
        };

        Scene scene = new Scene(shapes, lighting);

        Renderer renderer = new Renderer(camera, scene);
        var image = renderer.Render();
        var writer = new ImageWriter();
        writer.Serialize(image);

        return 0;
    }

    private static int Convert(ConvertCommand command)
    {
        var pluginFactory = new PluginFactory();

        var reader = pluginFactory.GetImageReader(command.SourceFormat);

        var bitmap = reader.Read(command.SourceFile);

        var writer = pluginFactory.GetImageWriter(command.OutputFormat);

        writer.Write(bitmap, command.OutputFile);

        return 0;
    }
}
using CommandLine;
using RenderEngine.Basic;
using RenderEngine.Cli.CommandLineCommands;
using RenderEngine.Cli.IO.Readers;
using RenderEngine.Cli.IO.Writers;
using RenderEngine.Core;
using RenderEngine.ImageConverter.Factories;
using RenderEngine.Interfaces;
using RenderEngine.Lightings;
using RenderEngine.Models;
using RenderEngine.Trees;
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
        Camera camera = new Camera(
            new Vector3(0, 1, 0),
            new Vector3(0, -1, 0),
            1920,
            1080,
            1,
            30
        );

        List<IShape> shapes = new ObjReader().Read(command.SourceFile);

        Transform transform = new Transform(Transform.IdentityMatrix).Translate(new Vector3(0f, 0, 0));

        foreach(var shape in shapes)
        {
            shape.Transform(transform);
        }

        var lighting = new List<ILighting>
        {
            //new DirectionalLight(new Vector3(0, -1, 0)),
            //new AmbientLight(new Pixel(255), 0.1f),
            new PointLight(new Vector3(0, 1, 0)),
            //new PointLight(new Vector3(0, 1, 0), new Pixel(255, 255, 255), 0.5f),
            //new AmbientLight(new Pixel(255, 0, 0), 0.1f)

            //new DirectionalLight(new Vector3(1, -1, 0), new Pixel(255, 0, 0)),
            //new DirectionalLight(new Vector3(-1, -1, 0), new Pixel(0, 0, 255))
        };

        var scene = new Scene(shapes, lighting);
        IOptimizer optimizer = new OctTree(new BoundingBox(new Vector3(-30, -30, -30), new Vector3(30, 30, 30)));
        optimizer.Build(scene.Shapes);
        var renderer = new TreeRenderer(camera, scene, optimizer);
        var image = renderer.Render();
        var writer = new ImageWriter();
        writer.Write(image, command);

        return 0;
    }

    private static int Convert(ConvertCommand command)
    {
        try
        {
            var pluginFactory = new PluginFactory();

            using var stream = File.Open(command.SourceFile, FileMode.Open);
            var reader = pluginFactory.GetImageReader(stream);

            var bitmap = reader.Read(stream);

            var writer = pluginFactory.GetImageWriter(command.OutputFormat);

            writer.Write(bitmap, command.OutputFile);
            return 0;
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
            return 1;
        }
    }
}
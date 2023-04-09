using RenderEngine.Basic;
using RenderEngine.Console.IO.Readers;
using RenderEngine.Console.IO.Writers;
using RenderEngine.Core;
using RenderEngine.Interfaces;
using RenderEngine.Lightings;
using RenderEngine.Shapes;

namespace RenderEngine.Console;

internal class Program
{
    private static void Main(string[] args)
    {
        Camera camera = new Camera(
            new Vector3(0, 1, 0),
            new Vector3(0, -1, 0),
            300,
            200,
            1,
            30
        );

        List<IShape> shapes = new ObjReader().Read(@"D:\cow.obj")[1].Faces;
        //List<IShape> shapes = new List<IShape>()
        //{
        //    new Sphere(new Vector3(4, 10, 0), 1),
        //    new Disk(new Vector3(0, 10, 0), 3, new Vector3(-1, 1, -1)),
        //};

        List<ILighting> lightings = new List<ILighting>
        {
            new DirectionalLight(new Vector3(1, -1, 0))
        };

        Scene scene = new Scene(shapes, lightings);

        Renderer renderer = new Renderer(camera, scene);
        var image = renderer.Render();
        new ImageWriter().Serialize(image);
        //new ConsoleWriter().Serialize(image);
    }
}
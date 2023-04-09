using RenderEngine.Basic;
using RenderEngine.Core;
using RenderEngine.Interfaces;
using RenderEngine.IO;
using RenderEngine.Lightings;
using RenderEngine.Shapes;

namespace RenderEngine.Console;

internal class Program
{
    private static void Main(string[] args)
    {
        Camera camera = new Camera(
            new Vector3(0, 10, 0),
            new Vector3(0, -1, 0),
            200,
            200,
            1,
            30
        );

        List<IShape> shapes = new ObjReader().Read(@"D:\cow.obj")[1].Faces;

        List<ILighting> lightings = new List<ILighting>
        {
            new DirectionalLight(new Vector3(0, 1, 0))
        };

        Scene scene = new Scene(shapes, lightings);

        Renderer renderer = new Renderer(camera, scene);
        var image = renderer.Render();
        for (int i = 0; i < image.GetLength(0); i++)
        {
            for (int j = 0; j < image.GetLength(1); j++)
            {
                if (image[i, j] <= 0)
                {
                    System.Console.Write(" ");
                }
                else if (image[i, j] <= 0.2 && image[i, j] > 0)
                {
                    System.Console.Write(".");
                }
                else if (image[i, j] <= 0.5 && image[i, j] > 0.2)
                {
                    System.Console.Write("*");
                }
                else if (image[i, j] <= 0.8 && image[i, j] > 0.5)
                {
                    System.Console.Write("O");
                }
                else
                {
                    System.Console.Write("#");
                }
            }

            System.Console.WriteLine();
        }
    }
}
using RenderEngine.Basic;
using RenderEngine.Core;
using RenderEngine.Interfaces;
using RenderEngine.Shapes;

internal class Program
{
    private static void Main(string[] args)
    {
        Camera camera = new Camera(
                Vector3.Zero,
                new Vector3(0, 1, 0),
                20,
                20,
                1,
                30
            );

        List<IShape> shapes = new List<IShape>() { new Sphere(new Vector3(-3, 5, -3), 3) };

        //TODO: add some lighting
        List<ILighting> lightings = new List<ILighting>();

        Scene scene = new Scene(shapes, lightings);

        Renderer renderer = new Renderer(camera, scene);
        var image = renderer.Render();
        for (int i = 0; i < image.GetLength(0); i++)
        {
            for (int j = 0; j < image.GetLength(1); j++)
            {
                //TODO: add some lighting
                if (image[i, j] == 0)
                {
                    Console.Write(" ");
                    continue;
                }
                Console.Write("#");
            }
            Console.WriteLine();
        }
    }
}
using RenderEngine.ImageConverter.Enums;
using RenderEngine.ImageConverter.Factories;
using RenderEngine.Models;

namespace RenderEngine.Cli.IO.Writers;

public class ImageWriter : IWriter
{
    public void Serialize(Bitmap bitmap)
    {
        var factory = new PluginFactory();

        var writer = factory.GetImageWriter(ImageFormat.Bmp);

        writer.Write(bitmap, "test.bmp");
    }
}
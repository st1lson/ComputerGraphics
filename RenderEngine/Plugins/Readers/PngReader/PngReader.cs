using RenderEngine.ImageConverter.Interfaces;
using RenderEngine.ImageConverter.Models.Png;
using RenderEngine.Models;

namespace PngReader;

public class PngReader : IImageReader
{
    public PngHeader Header => default;

    public Bitmap Read(Stream source)
    {
        throw new NotImplementedException();
    }
}
using PngCommon;
using RenderEngine.ImageConverter.Interfaces;
using RenderEngine.Models;

namespace PngReader;

public class PngReader : IImageReader
{
    public PngHeader Header => default;

    public Bitmap Read(Stream source)
    {
        throw new NotImplementedException();
    }

    public bool Validate(Stream stream)
    {
        return false;
    }
}
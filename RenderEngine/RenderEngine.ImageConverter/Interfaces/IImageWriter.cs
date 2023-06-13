using RenderEngine.Models;

namespace RenderEngine.ImageConverter.Interfaces;

public interface IImageWriter : IPlugin
{
    string Format { get; }

    void Write(Bitmap bitmap, string path);
}
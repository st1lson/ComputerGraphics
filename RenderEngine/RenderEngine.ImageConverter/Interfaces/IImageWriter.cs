using RenderEngine.Models;

namespace RenderEngine.ImageConverter.Interfaces;

public interface IImageWriter : IPlugin
{
    void Write(Bitmap bitmap, string path);
}
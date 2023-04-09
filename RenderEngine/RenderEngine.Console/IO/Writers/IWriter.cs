using RenderEngine.Models;

namespace RenderEngine.Console.IO.Writers;

public interface IWriter
{
    void Serialize(Bitmap bitmap);
}
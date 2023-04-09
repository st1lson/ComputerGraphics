using RenderEngine.Models;

namespace RenderEngine.Cli.IO.Writers;

public interface IWriter
{
    void Serialize(Bitmap bitmap);
}
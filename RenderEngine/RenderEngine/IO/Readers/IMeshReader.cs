using RenderEngine.Interfaces;

namespace RenderEngine.IO.Readers;

public interface IMeshReader
{
    List<IShape> Read(string path);
}

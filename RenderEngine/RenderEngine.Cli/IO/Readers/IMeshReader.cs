using RenderEngine.Interfaces;

namespace RenderEngine.Cli.IO.Readers;

public interface IMeshReader
{
    List<IShape> Read(string path);
}

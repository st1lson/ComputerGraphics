using RenderEngine.Interfaces;

namespace RenderEngine.Cli.IO.Readers;

public interface IMeshReader
{
    List<IMesh> Read(string path);
}

using RenderEngine.Interfaces;

namespace RenderEngine.Console.IO.Readers;

public interface IMeshReader
{
    List<IMesh> Read(string path);
}

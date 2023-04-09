using RenderEngine.Models;

namespace RenderEngine.Interfaces;

public interface IMeshReader
{
    List<IMesh> Read(string path);
}

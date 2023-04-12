using RenderEngine.Cli.CommandLineCommands;
using RenderEngine.Models;

namespace RenderEngine.Cli.IO.Writers;

internal interface IWriter
{
    void Write(Bitmap bitmap, RenderCommand? command = null);
}
using RenderEngine.Cli.CommandLineCommands;
using RenderEngine.ImageConverter.Factories;
using RenderEngine.Models;

namespace RenderEngine.Cli.IO.Writers;

internal sealed class ImageWriter
{
    public void Write(Bitmap bitmap, RenderCommand command)
    {
        var factory = new PluginFactory();

        var writer = factory.GetImageWriter(command.OutputFormat);

        writer.Write(bitmap, command.OutputFile);
    }
}
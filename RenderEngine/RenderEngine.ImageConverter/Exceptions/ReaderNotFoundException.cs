namespace RenderEngine.ImageConverter.Exceptions;

public sealed class ReaderNotFoundException : PluginNotFoundExceptions
{
    public ReaderNotFoundException() : base("Failed to find a suitable reader plugin")
    {
    }
}
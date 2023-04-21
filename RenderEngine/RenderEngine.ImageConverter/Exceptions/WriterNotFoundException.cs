namespace RenderEngine.ImageConverter.Exceptions;

public sealed class WriterNotFoundException : PluginNotFoundExceptions
{
    public WriterNotFoundException() : base("Failed to find a suitable writer plugin")
    {
    }
}
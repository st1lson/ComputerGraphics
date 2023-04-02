namespace RenderEngine.ImageConverter.Exceptions;

public class PluginNotFoundExceptions : Exception
{
    public PluginNotFoundExceptions(string message) : base(message)
    {
    }

    public PluginNotFoundExceptions() : base("Failed to find a plugin")
    {
    }
}
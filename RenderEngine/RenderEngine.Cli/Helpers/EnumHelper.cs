using RenderEngine.ImageConverter.Enums;

namespace RenderEngine.Cli.Helpers;

internal static class EnumHelper
{
    internal static ImageFormat StringToEnum(string? value)
    {
        return value switch
        {
            ".png" => ImageFormat.Png,
            ".bmp" => ImageFormat.Bmp,
            _ => ImageFormat.Unknown
        };
    }
}
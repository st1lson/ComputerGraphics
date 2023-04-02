using CommandLine;
using RenderEngine.ImageConverter.Enums;

namespace RenderEngine.ImageConverter.CLI.Models;

public class CommandLineArgs
{
    [Option('s', Required = true, HelpText = "Set source file to convert")]
    public string SourceFile { get; set; } = null!;

    public ImageFormat SourceFormat => StringToEnum(Path.GetExtension(SourceFile));

    [Option('d', Required = true, HelpText = "Set destination file")]
    public string OutputFile { get; set; } = null!;

    public ImageFormat OutputFormat => StringToEnum(Path.GetExtension(OutputFile));

    private static ImageFormat StringToEnum(string? value)
    {
        return value switch
        {
            ".png" => ImageFormat.Png,
            ".bmp" => ImageFormat.Bmp,
            _ => ImageFormat.Unknown
        };
    }
}
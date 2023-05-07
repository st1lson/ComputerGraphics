using CommandLine;

namespace RenderEngine.Cli.CommandLineCommands;

[Verb("render", HelpText = "Renders an image from an obj file")]
internal sealed class RenderCommand
{
    [Option('s', "source", Required = true, HelpText = "A path to the source file")]
    public string SourceFile { get; set; } = null!;

    [Option('d', "destination", Required = true, HelpText = "A path to the destination file")]
    public string OutputFile { get; set; } = null!;

    public string OutputFormat => Path.GetExtension(OutputFile);
}
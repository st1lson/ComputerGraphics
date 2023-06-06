﻿using CommandLine;

namespace RenderEngine.Cli.CommandLineCommands;

[Verb("render", HelpText = "Renders an image from an obj file")]
public sealed class RenderCommand
{
    [Option('s', "source", HelpText = "A path to the source file")]
    public string SourceFile { get; set; } = null!;

    [Option("scene", HelpText = "A path to the source file")]
    public string Scene { get; set; } = "default";

    [Option('d', "destination", Required = true, HelpText = "A path to the destination file")]
    public string OutputFile { get; set; } = null!;

    public string OutputFormat => Path.GetExtension(OutputFile);
}
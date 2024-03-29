﻿using CommandLine;

namespace RenderEngine.Cli.CommandLineCommands;

[Verb("convert", HelpText = "Converts an image")]
public sealed class ConvertCommand
{
    [Option('s', "source", Required = true, HelpText = "A path to the source file")]
    public string SourceFile { get; set; } = null!;

    public string SourceFormat => Path.GetExtension(SourceFile);

    [Option('d', "destination", Required = true, HelpText = "A path to the destination file")]
    public string OutputFile { get; set; } = null!;

    public string OutputFormat => Path.GetExtension(OutputFile);
}
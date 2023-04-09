﻿using CommandLine;
using RenderEngine.Cli.Helpers;
using RenderEngine.ImageConverter.Enums;

namespace RenderEngine.Cli.CommandLineCommands;

[Verb("render", HelpText = "Renders an image from an obj file")]
internal sealed class RenderCommand
{
    [Option('s', "source", Required = true, HelpText = "Set source file to convert")]
    public string SourceFile { get; set; } = null!;

    [Option('d', "destination", Required = true, HelpText = "Set destination file")]
    public string OutputFile { get; set; } = null!;

    public ImageFormat OutputFormat => EnumHelper.StringToEnum(Path.GetExtension(OutputFile));
}
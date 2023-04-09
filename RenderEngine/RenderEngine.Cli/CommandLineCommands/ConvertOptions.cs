﻿using CommandLine;
using RenderEngine.Cli.Helpers;
using RenderEngine.ImageConverter.Enums;

namespace RenderEngine.Cli.CommandLineCommands;

[Verb("convert", HelpText = "Converts an image")]
internal sealed class ConvertCommand
{
    [Option('s', "source", Required = true, HelpText = "Set source file to convert")]
    public string SourceFile { get; set; } = null!;

    public ImageFormat SourceFormat => EnumHelper.StringToEnum(Path.GetExtension(SourceFile));

    [Option('d', "destination", Required = true, HelpText = "Set destination file")]
    public string OutputFile { get; set; } = null!;

    public ImageFormat OutputFormat => EnumHelper.StringToEnum(Path.GetExtension(OutputFile));
}
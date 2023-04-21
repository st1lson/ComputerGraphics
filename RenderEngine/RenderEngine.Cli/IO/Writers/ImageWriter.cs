﻿using RenderEngine.Cli.CommandLineCommands;
using RenderEngine.ImageConverter.Factories;
using RenderEngine.Models;

namespace RenderEngine.Cli.IO.Writers;

internal sealed class ImageWriter : IWriter
{
    public void Write(Bitmap bitmap, RenderCommand? command = null)
    {
        if (command == null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        var factory = new PluginFactory();

        var writer = factory.GetImageWriter(command.OutputFormat);

        writer.Write(bitmap, command.OutputFile);
    }
}
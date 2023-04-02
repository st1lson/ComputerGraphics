﻿using System.Reflection;
using RenderEngine.ImageConverter.Enums;
using RenderEngine.ImageConverter.Exceptions;
using RenderEngine.ImageConverter.Interfaces;

namespace RenderEngine.ImageConverter.Factories;

public sealed class PluginFactory
{
    private const string DefaultPluginsDirectory = "";
    private const string PluginExtension = ".dll";
    private const string PluginSearchPattern = $"*{PluginExtension}";

    private const string WriterKey = "Writer";
    private const string ReaderKey = "Reader";

    public string PluginsPath { get; }

    private readonly IList<Assembly> _pluginAssemblies;

    public PluginFactory(string? pluginsDirectory = null)
    {
        PluginsPath = pluginsDirectory ?? DefaultPluginsDirectory;

        var directoryInfo = new DirectoryInfo(PluginsPath);

        _pluginAssemblies = Directory.GetFiles(
                directoryInfo.FullName,
                PluginSearchPattern,
                SearchOption.AllDirectories)
            .Where(plugin => (plugin.EndsWith(AddExtension(WriterKey)) || plugin.EndsWith(AddExtension(ReaderKey))))
            .Select(Assembly.LoadFrom)
            .ToList();
    }

    public IImageReader GetImageReader(ImageFormat format)
    {
        if (format == ImageFormat.Unknown)
            throw new ReaderNotFoundException();

        foreach (var pluginAssembly in _pluginAssemblies)
        {
            foreach (var type in pluginAssembly.GetTypes())
            {
                if (!typeof(IImageReader).IsAssignableFrom(type)) continue;

                return (IImageReader)Activator.CreateInstance(type)!;
            }
        }

        throw new PluginNotFoundExceptions();
    }

    public IImageWriter GetImageWriter(ImageFormat format)
    {
        if (format == ImageFormat.Unknown)
            throw new WriterNotFoundException();

        foreach (var pluginAssembly in _pluginAssemblies)
        {
            foreach (var type in pluginAssembly.GetTypes())
            {
                if (!typeof(IImageWriter).IsAssignableFrom(type)) continue;

                return (IImageWriter)Activator.CreateInstance(type)!;
            }
        }

        throw new PluginNotFoundExceptions();
    }

    private static string AddExtension(string file)
        => Path.ChangeExtension(file, PluginExtension);
}
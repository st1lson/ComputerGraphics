using System.Reflection;
using RenderEngine.ImageConverter.Exceptions;
using RenderEngine.ImageConverter.Interfaces;

namespace RenderEngine.ImageConverter.Factories;

public sealed class PluginFactory
{
    private const string DefaultPluginsDirectory = @"../../../../Plugins";
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
                SearchOption.TopDirectoryOnly)
            .Where(plugin => (plugin.EndsWith(AddExtension(WriterKey)) || plugin.EndsWith(AddExtension(ReaderKey))))
            .Select(Assembly.LoadFrom)
            .ToList();
    }

    public IImageReader GetImageReader(Stream stream)
    {
        foreach (var pluginAssembly in _pluginAssemblies)
        {
            foreach (var type in pluginAssembly.GetTypes())
            {
                if (!typeof(IImageReader).IsAssignableFrom(type))
                    continue;

                var instance = (IImageReader)Activator.CreateInstance(type)!;

                if (!instance.Validate(stream)) continue;

                return instance;
            }
        }

        throw new ReaderNotFoundException();
    }

    public IImageWriter GetImageWriter(string format)
    {
        foreach (var pluginAssembly in _pluginAssemblies)
        {
            foreach (var type in pluginAssembly.GetTypes())
            {
                if (!typeof(IImageWriter).IsAssignableFrom(type)) continue;

                var instance = (IImageWriter)Activator.CreateInstance(type)!;
                if (!instance.Format.Equals(format, StringComparison.InvariantCultureIgnoreCase))
                    continue;

                return instance;
            }
        }

        throw new WriterNotFoundException();
    }

    private static string AddExtension(string file)
        => Path.ChangeExtension(file, PluginExtension);
}
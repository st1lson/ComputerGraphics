using CommandLine;
using RenderEngine.ImageConverter.CLI.Models;
using RenderEngine.ImageConverter.Factories;

var result = Parser.Default.ParseArguments<CommandLineArgs>(args).Value;

if (result == null)
    throw new ArgumentException(nameof(result));

var pluginFactory = new PluginFactory();

var reader = pluginFactory.GetImageReader(result.SourceFormat);

var bitmap = reader.Read(result.SourceFile);
using RenderEngine.Cli.CommandLineCommands;
using RenderEngine.DependencyInjection;
using RenderEngine.ImageConverter.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderEngine.Cli
{
    public class ConverterApp
    {
        [Service]
        private readonly PluginFactory _pluginFactory = null!;

        private readonly ConvertCommand _command = null!;

        public ConverterApp(ConvertCommand command) 
        {
            _command = command;
        }

        public void Run() 
        {
            using var stream = File.Open(_command.SourceFile, FileMode.Open);
            var reader = _pluginFactory.GetImageReader(stream);
            var bitmap = reader.Read(stream);

            var writer = _pluginFactory.GetImageWriter(_command.OutputFormat);

            writer.Write(bitmap, _command.OutputFile);
        }
    }
}

using RenderEngine.ImageConverter.Interfaces;
using RenderEngine.ImageConverter.Models;
using RenderEngine.ImageConverter.Models.Png;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Text;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace PngReader
{
    public class PngReader : IImageReader
    {
        public PngHeader Header { get; private set; }

        public Bitmap Read(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}

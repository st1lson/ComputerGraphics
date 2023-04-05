using RenderEngine.ImageConverter.Interfaces;
using RenderEngine.ImageConverter.Models;
using RenderEngine.ImageConverter.Models.Png;

namespace PngReader
{
    public class PngReader : IImageReader
    {
        public PngHeader Header { get; private set; }

        public Bitmap Read(string filePath)
        {
            using BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open));
            reader.ReadUInt32();

            Header = new PngHeader
            {
                Width = reader.ReadUInt32(),
                Height = reader.ReadUInt32(),
                BitDepth = reader.ReadByte(),
                ColorType = reader.ReadByte(),
                CompressionMethod = reader.ReadByte(),
                FilterMethod = reader.ReadByte(),
                InterlaceMethod = reader.ReadByte()

            };

            int rowPadding = 3 - ((int)Header.Width * 3 - 1) % 4;
            int colorTableSize = (int)(Header.DataOffset + (Header.BitsPerPixel <= 8 ? (1 << Header.BitsPerPixel) * 4 : 0) - 54);
            reader.ReadBytes(colorTableSize);

            var bitmap = new Bitmap(Header.Height, Header.Width);

            for (uint y = 0; y < Header.Height; y++)
            {
                for (uint x = 0; x < Header.Width; x++)
                {
                    bitmap[y, x] = new Pixel(reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
                }
                reader.ReadBytes(rowPadding);
            }

            return bitmap;
        }
    }
}

using RenderEngine.ImageConverter.Interfaces;
using RenderEngine.ImageConverter.Models.Bmp;
using RenderEngine.Models;
using System.Reflection.PortableExecutable;

namespace BmpReader
{
    public class BmpReader : IImageReader
    {
        public BmpHeader Header { get; private set; }

        public Bitmap Read(string filePath)
        {
            using BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open));

            Header = new BmpHeader
            {
                Signature = reader.ReadUInt16(),
                FileSize = reader.ReadUInt32(),
                Reserved = reader.ReadUInt32(),
                DataOffset = reader.ReadUInt32(),
                HeaderSize = reader.ReadUInt32(),
                Width = reader.ReadUInt32(),
                Height = reader.ReadUInt32(),
                Planes = reader.ReadUInt16(),
                BitsPerPixel = reader.ReadUInt16(),
                Compression = reader.ReadUInt32(),
                ImageSize = reader.ReadUInt32(),
                XResolution = reader.ReadUInt32(),
                YResolution = reader.ReadUInt32(),
                ColorsUsed = reader.ReadUInt32(),
                ColorsImportant = reader.ReadUInt32()
            };

            int rowPadding = 3 - ((int)Header.Width * 3 - 1) % 4;
            int colorTableSize = (int)(Header.DataOffset + (Header.BitsPerPixel <= 8 ? (1 << Header.BitsPerPixel) * 4 : 0) - 54);
            reader.ReadBytes(colorTableSize);

            var bitmap = new Bitmap(Header.Height, Header.Width);

            for (uint y = Header.Height - 1; y < Header.Height; y--)
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

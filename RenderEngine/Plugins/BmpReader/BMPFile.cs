using RenderEngine.ImageConverter.Interfaces;

namespace BmpReader
{
    public class BMPFile : IImageReader
    {
        public BMPHeader Header { get; }
        public Bitmap Bitmap { get; }
        public BMPFile(string filePath)
        {
            using BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open));

            Header = new BMPHeader()
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

            Bitmap = new Bitmap(Header.Height, Header.Width);

            for (uint y = 0; y < Header.Height; y++)
            {
                for (uint x = 0; x < Header.Width; x++)
                {
                    Bitmap[y, x] = new Pixel(reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
                }
                reader.ReadBytes(rowPadding);
            }
        }
    }
}

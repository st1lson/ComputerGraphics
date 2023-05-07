using RenderEngine.ImageConverter.Interfaces;
using RenderEngine.ImageConverter.Models.Bmp;
using RenderEngine.Models;

namespace BmpWriter;

public class BmpWriter : IImageWriter
{
    public string Format => ".bmp";

    public void Write(Bitmap bitmap, string path)
    {
        uint rowPadding = 3 - (bitmap.Width * 3 - 1) % 4;
        uint imageSize = bitmap.Width * bitmap.Height * 3 + rowPadding * bitmap.Height;
        uint fileSize = imageSize + 54;

        var header = new BmpHeader()
        {
            Signature = 0x4D42,
            Reserved = 0,
            FileSize = fileSize,
            DataOffset = 54,
            HeaderSize = 40,
            Width = bitmap.Width,
            Height = bitmap.Height,
            Planes = 1,
            BitsPerPixel = 24,
            Compression = 0,
            ImageSize = imageSize,
            XResolution = 0,
            YResolution = 0,
            ColorsUsed = 0,
            ColorsImportant = 0
        };


        using FileStream stream = new FileStream(path, FileMode.Create);
        using BinaryWriter writer = new BinaryWriter(stream);
        writer.Write(header.Signature);
        writer.Write(header.FileSize);
        writer.Write(header.Reserved);
        writer.Write(header.DataOffset);
        writer.Write(header.HeaderSize);
        writer.Write(header.Width);
        writer.Write(header.Height);
        writer.Write(header.Planes);
        writer.Write(header.BitsPerPixel);
        writer.Write(header.Compression);
        writer.Write(header.ImageSize);
        writer.Write(header.XResolution);
        writer.Write(header.YResolution);
        writer.Write(header.ColorsUsed);
        writer.Write(header.ColorsImportant);
        for (uint y = header.Height - 1; y < header.Height; y--)
        {
            for (uint x = 0; x < header.Width; x++)
            {
                writer.Write((byte)bitmap[y, x].B);
                writer.Write((byte)bitmap[y, x].G);
                writer.Write((byte)bitmap[y, x].R);
            }
            for (int j = 0; j < rowPadding; j++)
            {
                writer.Write(Convert.ToByte(0));
            }
        }
    }
}
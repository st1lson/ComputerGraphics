using RenderEngine.ImageConverter.Interfaces;
using RenderEngine.ImageConverter.Models;
using RenderEngine.ImageConverter.Models.Png;
using System.IO;
using System.Reflection.PortableExecutable;

namespace PngWriter
{
    public class PngWriter : IImageWriter
    {
        public void Write(Bitmap bitmap, string path)
        {
            //var pngData = new byte[bitmap.Height * (bitmap.Width * 4 + 1) + 12];

            //var header = new PngHeader()
            //{
            //    Signature = BitConverter.ToUInt64(new byte[] { 137, 80, 78, 71, 13, 10, 26, 10 }, 0);,
            //    Reserved = 0,
            //    FileSize = fileSize,
            //    DataOffset = 54,
            //    HeaderSize = 40,
            //    Width = bitmap.Width,
            //    Height = bitmap.Height,
            //    Planes = 1,
            //    BitsPerPixel = 24,
            //    Compression = 0,
            //    ImageSize = imageSize,
            //    XResolution = 0,
            //    YResolution = 0,
            //    ColorsUsed = 0,
            //    ColorsImportant = 0
            //};


            //using FileStream stream = new FileStream(path, FileMode.Create);
            //using BinaryWriter writer = new BinaryWriter(stream);
            //writer.Write(header.Signature);
            //writer.Write(header.FileSize);
            //writer.Write(header.Reserved);
            //writer.Write(header.DataOffset);
            //writer.Write(header.HeaderSize);
            //writer.Write(header.Width);
            //writer.Write(header.Height);
            //writer.Write(header.Planes);
            //writer.Write(header.BitsPerPixel);
            //writer.Write(header.Compression);
            //writer.Write(header.ImageSize);
            //writer.Write(header.XResolution);
            //writer.Write(header.YResolution);
            //writer.Write(header.ColorsUsed);
            //writer.Write(header.ColorsImportant);
            //for (uint y = 0; y < header.Height; y++)
            //{
            //    for (uint x = 0; x < header.Width; x++)
            //    {
            //        writer.Write((byte)bitmap[y, x].R);
            //        writer.Write((byte)bitmap[y, x].G);
            //        writer.Write((byte)bitmap[y, x].B);
            //    }
            //    for (int j = 0; j < rowPadding; j++)
            //    {
            //        writer.Write(Convert.ToByte(0));
            //    }
            //}
        }
    }
}

﻿using BmpCommon;
using RenderEngine.ImageConverter.Interfaces;
using RenderEngine.Models;

namespace BmpReader;

public class BmpReader : IImageReader
{
    public BmpHeader Header { get; private set; }

    public Bitmap Read(Stream source)
    {
        Header = ReadHeader(source);

        int rowPadding = 3 - ((int)Header.Width * 3 - 1) % 4;
        int colorTableSize = (int)(Header.DataOffset + (Header.BitsPerPixel <= 8 ? (1 << Header.BitsPerPixel) * 4 : 0) - 54);
        using BinaryReader reader = new BinaryReader(source);
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

    private static BmpHeader ReadHeader(Stream stream)
    {
        BinaryReader reader = new BinaryReader(stream);

        var header =  new BmpHeader
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
        return header;
    }

    public bool Validate(Stream stream)
    {
        try
        {
            var header = ReadHeader(stream);

            if (header.Signature != 0x4D42)
                throw new ArgumentException("The file signature doesn't match the BMP signature.");
            if (header.Reserved != 0)
                throw new InvalidOperationException("Bad BMP format");
            if (header.Planes != 1)
                throw new InvalidOperationException("Bad BMP format");
            if (header.BitsPerPixel != 24)
                throw new InvalidOperationException("Only 24bit BMPs are supported");
            if (header.Compression != 0)
                throw new InvalidOperationException("Only BMPs without compression are supported");

            return true;
        }
        catch (Exception ex)
        {
            //Console.WriteLine(ex.Message);
            return false;
        }
        finally
        {
            stream.Seek(0, SeekOrigin.Begin);
        }
    }
}
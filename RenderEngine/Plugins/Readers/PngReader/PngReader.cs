using RenderEngine.ImageConverter.Interfaces;
using RenderEngine.ImageConverter.Models;
using RenderEngine.ImageConverter.Models.Png;
using System.Drawing;
using System.IO;
using System.IO.Compression;

namespace PngReader
{
    public class PngReader : IImageReader
    {
        public PngHeader Header { get; private set; }

        public Bitmap Read(string filePath)
        {
            using BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open));
            var signature = reader.ReadUInt64(); // read signature

            //IHDR
            reader.ReadUInt64(); //read chunk length and name (4 and 4 bytes)

            var width = ReverseBytes(reader.ReadUInt32()); // 4 bytes
            var height = ReverseBytes(reader.ReadUInt32()); // 4 bytes

            var bitsPerChannel = (uint)reader.ReadByte(); // 1 byte
            var colorType = (uint)reader.ReadByte(); // 1 byte
            var compressionMethod = (uint)reader.ReadByte(); // 1 byte
            var filtherMethod = (uint)reader.ReadByte(); // 1 byte
            var interlaceMethod = (uint)reader.ReadByte(); // 1 byte
            reader.ReadUInt32(); // CRC 4 bytes

            List<byte[]> bytesOfIDAT = new();

            string nameOfChunk = "";

            while(nameOfChunk != "IEND")
            {
                var chunkLength = ReverseBytes(reader.ReadUInt32());
                nameOfChunk = BitConverter.ToString(BitConverter.GetBytes(ReverseBytes(reader.ReadUInt32())));

                var bytesOfData = reader.ReadBytes((int)chunkLength);
                Array.Reverse(bytesOfData);

                if (nameOfChunk == "IDAT")
                {
                    bytesOfIDAT.Add(bytesOfData);
                }

                reader.ReadUInt32(); // CRC 4 bytes
            }

            Bitmap bitmap = new ((uint)width, (uint)height);

            foreach (var byteOdIdat in bytesOfIDAT)
            {
                using (var compressedStream = new MemoryStream(byteOdIdat))
                using (var zlibStream = new DeflateStream(compressedStream, CompressionMode.Decompress))
                using (var decompressedStream = new MemoryStream())
                {
                    zlibStream.CopyTo(decompressedStream);
                    var decompressedData = decompressedStream.ToArray();

                    // create a bitmap object

                    // set the pixel values from the decompressed data
                    var index = 0;
                    for (var y = 0; y < (int)height; y++)
                    {
                        for (var x = 0; x < (int)width; x++)
                        {
                            var r = decompressedData[index++];
                            var g = decompressedData[index++];
                            var b = decompressedData[index++];
                            bitmap[y, x] = new Pixel(r, g, b);
                        }
                    }
                }
            }

            return bitmap;
            
            


            //Header = new PngHeader
            //{
            //    Signature = reader.ReadUInt64(),
            //    Width = reader.ReadUInt32(),
            //    Height = reader.ReadUInt32(),
            //    BitDepth = reader.ReadByte(),
            //    ColorType = reader.ReadByte(),
            //    CompressionMethod = reader.ReadByte(),
            //    FilterMethod = reader.ReadByte(),
            //    InterlaceMethod = reader.ReadByte()

            //};

            //int rowPadding = 3 - ((int)Header.Width * 3 - 1) % 4;
            //int colorTableSize = (int)(Header.DataOffset + (Header.BitsPerPixel <= 8 ? (1 << Header.BitsPerPixel) * 4 : 0) - 54);
            //reader.ReadBytes(colorTableSize);

            //var bitmap = new Bitmap(height, width);

            //for (uint y = 0; y < height; y++)
            //{
            //    for (uint x = 0; x < width; x++)
            //    {
            //        bitmap[y, x] = new Pixel(reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
            //        reader.ReadByte();
            //    }
            //}

            //return bitmap;
        }

        private static ulong ReverseBytes(uint a)
        {
            byte[] bytes = BitConverter.GetBytes(a);
            Array.Reverse(bytes);
            switch (bytes.Length)
            {
                case 1:
                    return (uint)bytes[0];
                case 2:
                    return BitConverter.ToUInt16(bytes, 0);
                case 4:
                    return BitConverter.ToUInt32(bytes, 0);
                case 8:
                    return BitConverter.ToUInt64(bytes, 0);
                default:
                    throw new Exception();
            }
        }
    }
}

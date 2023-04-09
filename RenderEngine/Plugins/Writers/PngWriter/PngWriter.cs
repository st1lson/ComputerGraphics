using RenderEngine.ImageConverter.Interfaces;
using RenderEngine.ImageConverter.Models;
using RenderEngine.ImageConverter.Models.Png;
using System.IO;
using System.IO.Compression;
using System.Reflection.PortableExecutable;
using System.Text;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace PngWriter
{
    public class PngWriter : IImageWriter
    {
        public void Write(Bitmap bitmap, string path)
        {


            IHDRData ihdrData = new()
            {
                Width = BitConverter.GetBytes(bitmap.Width).Reverse().ToArray(),
                Height = BitConverter.GetBytes(bitmap.Height).Reverse().ToArray(),
                BitDepth = 8,
                ColorType = 2,
                CompressionMethod = 0,
                FilterMethod = 0,
                InterlaceMethod = 0
            };

            PngHeader header = new()
            {
                Signature = new byte[] { 137, 80, 78, 71, 13, 10, 26, 10 },
                IHDR = new()
                {
                    ChunkLength = BitConverter.GetBytes(13).Reverse().ToArray(),
                    ChunkName = Encoding.ASCII.GetBytes("IHDR"),
                    IHDRData = ihdrData,
                    CRC = GetCRC32(Encoding.ASCII.GetBytes("IHDR")
                    .Concat(ihdrData.Width)
                    .Concat(ihdrData.Height)
                    .Concat(new byte[] {ihdrData.BitDepth, ihdrData.ColorType, ihdrData.CompressionMethod, ihdrData.FilterMethod, ihdrData.InterlaceMethod}))

                },
                IDAT = CreateChunkIdatFromBitmap(bitmap),
                IEND = new byte[] { 0, 0, 0, 0, 73, 69, 78, 68, 173, 66, 96, 130 }
            };


            using FileStream stream = new FileStream(path, FileMode.Create);
            using BinaryWriter writer = new BinaryWriter(stream);
            //write signature
            writer.Write(header.Signature);

            //write IHDR
            writer.Write(header.IHDR.ChunkLength);
            writer.Write(header.IHDR.ChunkName);
            writer.Write(header.IHDR.IHDRData.Width);
            writer.Write(header.IHDR.IHDRData.Height);
            writer.Write(header.IHDR.IHDRData.BitDepth);
            writer.Write(header.IHDR.IHDRData.ColorType);
            writer.Write(header.IHDR.IHDRData.CompressionMethod);
            writer.Write(header.IHDR.IHDRData.FilterMethod);
            writer.Write(header.IHDR.IHDRData.InterlaceMethod);
            writer.Write(header.IHDR.CRC);

            //write IDAT
            writer.Write(header.IDAT.ChunkLength);
            writer.Write(header.IDAT.ChunkName);
            writer.Write(header.IDAT.DataImage);
            writer.Write(header.IDAT.CRC);

            //write IEND
            writer.Write(header.IEND);
        }

        private IDAT CreateChunkIdatFromBitmap(Bitmap bitmap)
        {
            byte[] decompressedData = new byte[bitmap.Width * bitmap.Height * 3 + bitmap.Height];
            int position = 0;

            for (int x = 0; x < bitmap.Height; x++)
            {
                decompressedData[position] = 0;
                position++;
                
                for (int y = 0; y < bitmap.Width; y++)
                {
                    decompressedData[position++] = Convert.ToByte(bitmap[x, y].R);
                    decompressedData[position++] = Convert.ToByte(bitmap[x, y].G);
                    decompressedData[position++] = Convert.ToByte(bitmap[x, y].B);
                }
            }

            using MemoryStream outputMemoryStream = new();
            using (DeflaterOutputStream deflaterOutputStream = new(outputMemoryStream))
            {
                deflaterOutputStream.Write(decompressedData, 0, decompressedData.Length);
            }

            byte[] compressedData = outputMemoryStream.ToArray().Reverse().ToArray();

            return new IDAT()
            {
                ChunkLength = BitConverter.GetBytes(compressedData.Length).Reverse().ToArray(),
                ChunkName = Encoding.ASCII.GetBytes("IDAT"),
                DataImage = compressedData,
                CRC = GetCRC32(Encoding.ASCII.GetBytes("IDAT").Concat(compressedData))
            };
        }

        //From stackoverflow https://ru.stackoverflow.com/questions/80320/%D0%9A%D0%B0%D0%BA-%D0%BF%D0%BE%D1%81%D1%87%D0%B8%D1%82%D0%B0%D1%82%D1%8C-crc-%D0%B2-c
        byte[] GetCRC32(IEnumerable<byte> bytes)
        {
            var crcTable = new uint[256];
            uint crc;

            for (uint i = 0; i < 256; i++)
            {
                crc = i;
                for (uint j = 0; j < 8; j++)
                    crc = (crc & 1) != 0 ? (crc >> 1) ^ 0xEDB88320 : crc >> 1;

                crcTable[i] = crc;
            }

            crc = bytes.Aggregate(0xFFFFFFFF, (current, s) => crcTable[(current ^ s) & 0xFF] ^ (current >> 8));

            crc ^= 0xFFFFFFFF;
            return BitConverter.GetBytes(crc);
        }
    } 
}

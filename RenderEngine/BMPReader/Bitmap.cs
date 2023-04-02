using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BmpReader
{
    public class Bitmap
    {
        Pixel[,] pixels;
        public uint Height { get; init; }
        public uint Width { get; init; }
        public Bitmap(uint height, uint width)
        {
            pixels = new Pixel[height, width];
            Height = height;
            Width = width;

        }

        public Pixel this[int y, int x]
        {
            get => pixels[y, x];
            set => pixels[y, x] = value;
        }

        public Pixel this[uint y, uint x]
        {
            get => pixels[y, x];
            set => pixels[y, x] = value;
        }
    }
}

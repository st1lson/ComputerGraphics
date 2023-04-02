using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BmpReader
{
    public struct Pixel
    {
        public readonly int R { get; init; }
        public readonly int G { get; init; }
        public readonly int B { get; init; }

        public Pixel(int r, int g, int b)
        {
            R = r;
            G = g;
            B = b;
        }
    }
}

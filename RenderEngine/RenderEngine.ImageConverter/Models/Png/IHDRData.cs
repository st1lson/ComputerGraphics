using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderEngine.ImageConverter.Models.Png
{
    public struct IHDRData
    {
        public byte[] Width { get; init; }
        public byte[] Height { get; init; }
        public byte BitDepth { get; init; }
        public byte ColorType { get; init; }
        public byte CompressionMethod { get; init; }
        public byte FilterMethod { get; init; }
        public byte InterlaceMethod { get; init; }
    }
}

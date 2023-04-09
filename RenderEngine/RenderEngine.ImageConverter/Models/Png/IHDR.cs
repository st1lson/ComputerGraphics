using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderEngine.ImageConverter.Models.Png
{
    public struct IHDR
    {
        public byte[] ChunkLength { get; init; }
        public byte[] ChunkName { get; init; }
        public IHDRData IHDRData { get; init; }
        public byte[] CRC { get; init; }
    }
}

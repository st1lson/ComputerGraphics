namespace RenderEngine.ImageConverter.Models.Png;

public struct PngHeader
{
    //public ulong Signature { get => BitConverter.ToUInt64(new byte[] { 137, 80, 78, 71, 13, 10, 26, 10 }, 0);}
    public ulong Signature { get; init; }
    //IHDR chunk
    //public uint IHDR = 0x49484452u;
    //public uint IDAT = 0x49444154u;
    //public uint IEND = 0x49454E44u;
    public uint IHDR { get; init; }
    public uint IDAT { get; init; }
    public uint IEND { get; init; }
    public uint Width { get; init; }
    public uint Height { get; init; }
    public byte BitDepth { get; init; }
    public byte ColorType { get; init; }
    public byte CompressionMethod { get; init; }
    public byte FilterMethod { get; init; }
    public byte InterlaceMethod { get; init; }


}
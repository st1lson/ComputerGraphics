namespace RenderEngine.ImageConverter.Models.Png;

public struct PngHeader
{
    //public ulong Signature { get => BitConverter.ToUInt64(new byte[] { 137, 80, 78, 71, 13, 10, 26, 10 }, 0);}
    public ulong Signature { get; init; }
    //IHDR chunk
    public uint Width { get; init; }
    public uint Height { get; init; }
    public byte BitDepth { get; init; }
    public byte ColorType { get; init; }
    public byte CompressionMethod { get; init; }
    public byte FilterMethod { get; init; }
    public byte InterlaceMethod { get; init; }


}
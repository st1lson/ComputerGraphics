namespace PngCommon;
public readonly struct IHDRData
{
    public byte[] Width { get; init; }
    public byte[] Height { get; init; }
    public byte BitDepth { get; init; }
    public byte ColorType { get; init; }
    public byte CompressionMethod { get; init; }
    public byte FilterMethod { get; init; }
    public byte InterlaceMethod { get; init; }
}

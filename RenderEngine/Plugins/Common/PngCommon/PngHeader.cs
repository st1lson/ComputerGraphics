namespace PngCommon;

public readonly struct PngHeader
{
    public byte[] Signature { get; init; }
    public IHDR IHDR { get; init; }
    public IDAT IDAT { get; init; }
    public byte[] IEND { get; init; }
}
namespace BmpCommon;

public struct BmpHeader
{
    public ushort Signature { get; init; }
    public uint FileSize { get; init; }
    public uint Reserved { get; init; }
    public uint DataOffset { get; init; }
    public uint HeaderSize { get; init; }
    public uint Width { get; init; }
    public uint Height { get; init; }
    public ushort Planes { get; init; }
    public ushort BitsPerPixel { get; init; }
    public uint Compression { get; init; }
    public uint ImageSize { get; init; }
    public uint XResolution { get; init; }
    public uint YResolution { get; init; }
    public uint ColorsUsed { get; init; }
    public uint ColorsImportant { get; init; }
}
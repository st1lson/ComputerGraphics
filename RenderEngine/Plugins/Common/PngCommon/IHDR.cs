namespace PngCommon;
public readonly struct IHDR
{
    public byte[] ChunkLength { get; init; }
    public byte[] ChunkName { get; init; }
    public IHDRData IHDRData { get; init; }
    public byte[] CRC { get; init; }
}
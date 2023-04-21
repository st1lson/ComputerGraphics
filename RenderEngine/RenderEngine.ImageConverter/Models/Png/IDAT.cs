namespace RenderEngine.ImageConverter.Models.Png;

public readonly struct IDAT
{
    public byte[] ChunkLength { get; init; }
    public byte[] ChunkName { get; init; }
    public byte[] DataImage { get; init; }
    public byte[] CRC { get; init; }
}
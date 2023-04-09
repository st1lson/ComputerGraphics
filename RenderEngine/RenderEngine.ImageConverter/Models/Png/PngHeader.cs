namespace RenderEngine.ImageConverter.Models.Png;

public struct PngHeader
{
    public byte[] Signature { get; init; }
    public IHDR IHDR { get; init; }
    public IDAT IDAT { get; init; }
    public byte[] IEND { get; init; }
}
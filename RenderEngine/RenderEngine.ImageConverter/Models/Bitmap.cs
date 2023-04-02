namespace RenderEngine.ImageConverter.Models;

public class Bitmap
{
    private readonly Pixel[,] _pixels;
    public uint Height { get; init; }
    public uint Width { get; init; }
    public Bitmap(uint height, uint width)
    {
        _pixels = new Pixel[height, width];
        Height = height;
        Width = width;
    }

    public Pixel this[int y, int x]
    {
        get => _pixels[y, x];
        set => _pixels[y, x] = value;
    }

    public Pixel this[uint y, uint x]
    {
        get => _pixels[y, x];
        set => _pixels[y, x] = value;
    }
}
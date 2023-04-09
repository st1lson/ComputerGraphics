using RenderEngine.Basic;

namespace RenderEngine.Models;

public readonly struct Pixel
{
    public readonly int R { get; init; }

    public readonly int G { get; init; }

    public readonly int B { get; init; }

    public Pixel(int r, int g, int b)
    {
        R = r;
        G = g;
        B = b;
    }

    public Pixel(int value)
    {
        R = value;
        G = value;
        B = value;
    }

    public static Pixel operator *(Pixel pixel, Vector3 multiplier)
    {
        return new Pixel((int)(pixel.R * multiplier.X), (int)(pixel.G * multiplier.Y), (int)(pixel.B * multiplier.Z));
    }

    public float GetNumeric()
    {
        return ((R + G + B) / 3f) / 255;
    }
}
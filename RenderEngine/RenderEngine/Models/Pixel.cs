using RenderEngine.Basic;

namespace RenderEngine.Models;

public readonly struct Pixel
{
    public int R { get; init; }

    public int G { get; init; }

    public int B { get; init; }

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

    public static Pixel operator +(Pixel lhs, Pixel rhs)
    {
        return new Pixel(Math.Min(lhs.R + rhs.R, 255), Math.Min(lhs.G + rhs.G, 255), Math.Min(lhs.B + rhs.B, 255));
    }

    public float GetNumeric()
    {
        return ((R + G + B) / 3f) / 255;
    }
}
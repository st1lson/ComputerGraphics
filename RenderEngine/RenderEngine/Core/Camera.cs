using RenderEngine.Basic;

namespace RenderEngine.Core;

public class Camera
{
    public Vector3 Orig { get; init; }

    public Vector3 Dir { get; init; }

    public uint PixelWidth { get; init; }

    public uint PixelHeight { get; init; }

    public int VerticalFOV { get; init; }

    public float FocalLength { get; init; }

    public Camera(Vector3 orig, Vector3 dir, uint pixelWidth, uint pixelHeight, float focalLength, int verticalFOV)
    {
        Orig = orig;
        Dir = dir;
        PixelWidth = pixelWidth;
        PixelHeight = pixelHeight;
        FocalLength = focalLength;
        VerticalFOV = verticalFOV;
    }
}
using RenderEngine.Basic;
using RenderEngine.Configurations;

namespace RenderEngine.Core;

public class Camera
{
    public Vector3 Orig { get; set; }

    public Vector3 Dir { get; set; }

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

    public Camera(CameraConfiguration configuration)
    {
        Orig = configuration.Orig;
        Dir = configuration.Dir;
        PixelWidth = configuration.PixelWidth;
        PixelHeight = configuration.PixelHeight;
        FocalLength = configuration.FocalLength;
        VerticalFOV = configuration.VerticalFOV;
    }
}
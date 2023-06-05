using RenderEngine.Basic;

namespace RenderEngine.Configurations;

public sealed record CameraConfiguration
{
    public static CameraConfiguration Default => new()
    {
        Orig = new Vector3(0, 1, 0),
        Dir = new Vector3(0, -1, 0),
        PixelWidth = 200,
        PixelHeight = 200,
        VerticalFOV = 1,
        FocalLength = 30
    };

    public Vector3 Orig { get; init; }
    public Vector3 Dir { get; init; }
    public uint PixelWidth { get; init; }
    public uint PixelHeight { get; init; }
    public int VerticalFOV { get; init; }
    public float FocalLength { get; init; }
}

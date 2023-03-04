using RenderEngine.Basic;

namespace RenderEngine.Core
{
    public class Camera
    {
        public Vector3 Orig { get; init; }
        public Vector3 Dir { get; init; }
        public int PixelWidth { get; init; }
        public int PixelHeight { get; init; }
        public float FocalLength { get; init; }
        public Camera(Vector3 orig, Vector3 dir, int pixelWidth, int pixelHeight, float focalLength)
        {
            Orig = orig;
            Dir = dir;
            PixelWidth = pixelWidth;
            PixelHeight = pixelHeight;
            FocalLength = focalLength;
        }
    }
}

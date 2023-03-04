using System.Drawing;

namespace RenderEngine.Basic;

public readonly struct Point3
{
    public float X { get; init; }

    public float Y { get; init; }

    public float Z { get; init; }

    public Point3(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static Point3 operator +(Point3 left, Vector3 right)
    {
        return new Point3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
    }

    public override string ToString()
    {
        return $"X: {X}; Y: {Y}; Z: {Z}";
    }
}
namespace RenderEngine.Basic;

public readonly struct Vector3
{
    public float X { get; init; }

    public float Y { get; init; }

    public float Z { get; init; }

    public Vector3(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static Vector3 operator +(Vector3 left, Vector3 right)
    {
        return new Vector3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
    }

    public static Vector3 operator -(Vector3 left, Vector3 right)
    {
        return new Vector3(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
    }

    public static Vector3 operator *(Vector3 left, float multiplier)
    {
        return new Vector3(left.X * multiplier, left.Y * multiplier, left.Z * multiplier);
    }

    public static Vector3 operator /(Vector3 left, float divider)
    {
        return new Vector3(left.X / divider, left.Y / divider, left.Z / divider);
    }

    public float Abs() => (float)Math.Sqrt(X * X + Y * Y + Z * Z);

    public Vector3 Normalize() => this / Abs();

    public static float Dot(Vector3 left, Vector3 right)
    {
        return left.X * right.X + left.Y * right.Y + left.Z * right.Z;
    }

    public static Vector3 Cross(Vector3 left, Vector3 right)
    {
        return new Vector3(
            left.Y * right.Z - left.Y * right.Z,
            left.Z * right.X - left.X * right.Z,
            left.X * right.Y - left.Y * right.X);
    }
}
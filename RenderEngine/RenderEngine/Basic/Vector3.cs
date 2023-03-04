namespace RenderEngine.Basic;

public readonly struct Vector3
{
    public float X { get; init; }

    public float Y { get; init; }

    public float Z { get; init; }

    public static readonly Vector3 Zero = new Vector3(0);

    public Vector3(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public Vector3(float value)
    {
        X = value;
        Y = value;
        Z = value;
    }

    public static Vector3 operator +(Vector3 left, Vector3 right)
    {
        return new Vector3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
    }

    public static Vector3 operator -(Vector3 vector)
    {
        return new Vector3(-vector.X, -vector.Y, -vector.Z);
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

    public static bool operator ==(Vector3 left, Vector3 right)
    {
        const double tolerance = 1e5;
        return Math.Abs(left.X - right.X + left.Y - right.Y + left.Z - right.Z) < tolerance;
    }

    public static bool operator !=(Vector3 left, Vector3 right)
    {
        return !(left == right);
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

    public bool Equals(Vector3 vector)
    {
        return X.Equals(vector.X) && Y.Equals(vector.Y) && Z.Equals(vector.Z);
    }

    public override bool Equals(object? obj)
    {
        return obj is Vector3 other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Z);
    }

    public override string ToString()
    {
        return $"X: {X}; Y: {Y}; Z: {Z}";
    }
}
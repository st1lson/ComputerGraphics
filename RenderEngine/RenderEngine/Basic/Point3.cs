namespace RenderEngine.Basic;

public readonly struct Point3
{
    public int X { get; init; }

    public int Y { get; init; }

    public int Z { get; init; }

    public Point3(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public override string ToString()
    {
        return $"X: {X}; Y: {Y}; Z: {Z}";
    }
}
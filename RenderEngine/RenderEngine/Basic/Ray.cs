namespace RenderEngine.Basic;

public readonly struct Ray
{
    public Point3 Orig { get; init; }

    public Vector3 Dir { get; init; }

    public Ray(Point3 orig, Vector3 dir)
    {
        Orig = orig;
        Dir = dir;
    }

    public override string ToString()
    {
        return $"Origin: {Orig}; Direction: {Dir}";
    }
}
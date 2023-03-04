namespace RenderEngine.Basic;

public readonly struct Ray
{
    public Vector3 Orig { get; init; }

    public Vector3 Dir { get; init; }

    public Ray(Vector3 orig, Vector3 dir)
    {
        Orig = orig;
        Dir = dir;
    }

    public override string ToString()
    {
        return $"Origin: {Orig}; Direction: {Dir}";
    }
}
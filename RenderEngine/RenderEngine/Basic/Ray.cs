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

    public Vector3 GetPoint(float t) 
    {
        return Orig + Dir * t;
    }

    public override string ToString()
    {
        return $"Origin: {Orig}; Direction: {Dir}";
    }
}
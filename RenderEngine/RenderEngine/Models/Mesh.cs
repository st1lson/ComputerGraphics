using RenderEngine.Basic;
using RenderEngine.Interfaces;
using System;

namespace RenderEngine.Models;

public class Mesh : IMesh
{
    public List<Vector3> Vertices { get; }

    public List<IShape> Faces { get; }

    public Mesh()
    {
        Vertices = new List<Vector3>();
        Faces = new List<IShape>();
    }
}

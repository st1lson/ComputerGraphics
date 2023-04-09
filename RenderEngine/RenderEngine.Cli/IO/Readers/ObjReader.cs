using System.Globalization;
using RenderEngine.Basic;
using RenderEngine.Interfaces;
using RenderEngine.Models;
using RenderEngine.Shapes;

namespace RenderEngine.Cli.IO.Readers;

public class ObjReader : IMeshReader
{
    public List<IMesh> Read(string path)
    {
        using StreamReader reader = new StreamReader(path);
        string? line;
        List<IMesh> meshes = new List<IMesh>();
        List<Vector3> vertices = new List<Vector3>();
        meshes.Add(new Mesh());
        while ((line = reader.ReadLine()) != null)
        {
            string[] parts = line.Split(' ');

            var currentMesh = meshes[^1];
            switch (parts[0])
            {
                case "g":
                    meshes.Add(new Mesh());
                    break;
                case "v":
                    float x = float.Parse(parts[1], CultureInfo.InvariantCulture);
                    float y = float.Parse(parts[2], CultureInfo.InvariantCulture);
                    float z = float.Parse(parts[3], CultureInfo.InvariantCulture);
                    Vector3 vertex = new Vector3(x, y, z);
                    vertices.Add(vertex);
                    currentMesh.Vertices.Add(vertex);
                    break;
                case "f":
                    List<int> vertixIndexes = new List<int>();
                    for (int i = 1; i < parts.Length; i++)
                    {
                        string[] indices = parts[i].Split('/');
                        int vertexIndex = int.Parse(indices[0]) - 1;
                        int uvIndex = indices.Length > 1 && !string.IsNullOrEmpty(indices[1]) ? int.Parse(indices[1]) - 1 : -1;
                        int normalIndex = indices.Length > 2 ? int.Parse(indices[2]) - 1 : -1;
                        vertixIndexes.Add(vertexIndex);
                    }

                    currentMesh.Faces.Add(new Triangle(vertices[vertixIndexes[0]], vertices[vertixIndexes[1]], vertices[vertixIndexes[2]]));
                    break;
            }
        }

        return meshes;
    }
}

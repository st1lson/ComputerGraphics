using System.Globalization;
using RenderEngine.Basic;
using RenderEngine.Interfaces;
using RenderEngine.Models;
using RenderEngine.Shapes;

namespace RenderEngine.Cli.IO.Readers;

public class ObjReader : IMeshReader
{
    public List<IShape> Read(string path)
    {
        using StreamReader reader = new StreamReader(path);
        string? line;
        List<IShape> faces = new List<IShape>();
        List<Vector3> vertices = new List<Vector3>();
        List<Vector3> normals = new List<Vector3>();
        while ((line = reader.ReadLine()) != null)
        {
            string[] parts = line.Split(' ');
            switch (parts[0])
            {
                case "v":
                    float x = float.Parse(parts[1], CultureInfo.InvariantCulture);
                    float y = float.Parse(parts[2], CultureInfo.InvariantCulture);
                    float z = float.Parse(parts[3], CultureInfo.InvariantCulture);
                    Vector3 vertex = new Vector3(x, y, z);
                    vertices.Add(vertex);
                    break;
                case "vn":
                    float nx = float.Parse(parts[1], CultureInfo.InvariantCulture);
                    float ny = float.Parse(parts[2], CultureInfo.InvariantCulture);
                    float nz = float.Parse(parts[3], CultureInfo.InvariantCulture);
                    Vector3 normal = new Vector3(nx, ny, nz);
                    normals.Add(normal);
                    break;
                case "f":
                    List<int> vertixIndexes = new List<int>();
                    List<int> normalIndexes = new List<int>();
                    for (int i = 1; i < parts.Length; i++)
                    {
                        string[] indices = parts[i].Split('/');
                        int vertexIndex = int.Parse(indices[0]) - 1;
                        int uvIndex = indices.Length > 1 && !string.IsNullOrEmpty(indices[1]) ? int.Parse(indices[1]) - 1 : -1;
                        int normalIndex = indices.Length > 2 ? int.Parse(indices[2]) - 1 : -1;
                        vertixIndexes.Add(vertexIndex);
                        normalIndexes.Add(normalIndex);
                        if (i >= 3)
                        {
                            faces.Add(new Triangle(vertices[vertixIndexes[0]], vertices[vertixIndexes[i - 2]], vertices[vertixIndexes[i - 1]], 
                                normals[normalIndexes[0]], normals[normalIndexes[i - 2]], normals[normalIndexes[i - 1]]));
                        }
                    }

                    break;
            }
        }

        return faces;
    }
}

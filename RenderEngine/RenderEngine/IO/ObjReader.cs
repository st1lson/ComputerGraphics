using RenderEngine.Basic;
using RenderEngine.Interfaces;
using RenderEngine.Models;
using RenderEngine.Shapes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderEngine.IO
{
    public class ObjReader : IMeshReader
    {
        public List<IMesh> Read(string path)
        {
            StreamReader reader = new StreamReader(path);
            string? line;
            List<IMesh> meshes = new List<IMesh>();
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
                        currentMesh.Vertices.Add(new Vector3(x, y, z));
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

                        currentMesh.Faces.Add(new Triangle(currentMesh.Vertices[vertixIndexes[0]], currentMesh.Vertices[vertixIndexes[1]], currentMesh.Vertices[vertixIndexes[2]]));
                        break;
                }
            }

            return meshes;
        }
    }
}

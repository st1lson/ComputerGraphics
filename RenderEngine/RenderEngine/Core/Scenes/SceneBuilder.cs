using RenderEngine.Interfaces;
using RenderEngine.IO.Readers;
using RenderEngine.Transformer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderEngine.Core.Scenes
{
    public class SceneBuilder : ISceneBuilder
    {
        private IMeshReader objReader;

        private List<Transform> transforms = new List<Transform>();

        private List<ILighting> lightings = new List<ILighting>();

        private List<string> objFilePaths = new List<string>();

        public SceneBuilder(IMeshReader meshReader)
        {
            objReader = meshReader;
        }

        public ISceneBuilder AddFileObjs(string objFile)
        {
            objFilePaths.Add(objFile);
            return this;
        }

        public ISceneBuilder AddLight(ILighting lighting)
        {
            lightings.Add(lighting);
            return this;
        }

        public ISceneBuilder AddTransforms(Transform transform)
        {
            transforms.Add(transform);
            return this;
        }

        public Scene Build()
        {
            List<IShape> shapes = new List<IShape>();
            if (objFilePaths != null)
            {
                foreach (var objFile in objFilePaths)
                {
                    shapes = shapes.Concat(objReader.Read(objFile)).ToList();
                }
            }

            if (transforms != null)
            {
                foreach (var shape in shapes)
                {
                    foreach (var transform in transforms)
                    {
                        shape.Transform(transform);
                    }
                }
            }

            return new Scene(shapes, lightings, transforms);
        }
    }
}

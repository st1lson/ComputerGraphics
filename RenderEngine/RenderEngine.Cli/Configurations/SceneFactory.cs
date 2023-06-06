using RenderEngine.Basic;
using RenderEngine.Cli.CommandLineCommands;
using RenderEngine.Configurations;
using RenderEngine.Core;
using RenderEngine.Core.Scenes;
using RenderEngine.IO.Readers;
using RenderEngine.Lightings;
using RenderEngine.Transformer;
using System.Xml.Linq;

namespace RenderEngine.Cli.Configurations
{
    public class SceneFactory
    {
        public (Camera camera, Scene scene) CreateScene(RenderCommand command, IMeshReader objReader) 
        {
            return command.Scene switch
            {
                "default" => Default(objReader, command.SourceFile),
                "cow-scene" => CowScene(objReader),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private (Camera camera, Scene scene) Default(IMeshReader objReader, string objFileName)
        {
            CameraConfiguration cameraConfig = CameraConfiguration.Default;
            Camera camera = new Camera(cameraConfig);
            Scene scene = new SceneBuilder(objReader)
                .AddFileObjs(objFileName)
                .AddLight(new DirectionalLight(new Vector3(0, -1, 0)))
                .Build();
            return (camera, scene);
        }

        private (Camera camera, Scene scene) CowScene(IMeshReader objReader)
        {
            CameraConfiguration cameraConfig = CameraConfiguration.Default with
            {
                PixelHeight = 150
            };
            Camera camera = new Camera(cameraConfig);
            Scene scene = new SceneBuilder(objReader)
                .AddFileObjs("D:\\cow.obj")
                .AddLight(new DirectionalLight(new Vector3(0, -1, 0)))
                .AddTransforms(new Transform(Transform.IdentityMatrix).Translate(new Vector3(0.5f, 0, 0)))
                .Build();
            return (camera, scene);
        }
    }
}

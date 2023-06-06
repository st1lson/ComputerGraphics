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
        private readonly IMeshReader objReader;

        public SceneFactory(IMeshReader meshReader)
        {
            objReader = meshReader;
        }

        public (Camera camera, Scene scene) CreateScene(RenderCommand command) 
        {
            return command.Scene switch
            {
                "default" => Default(command.SourceFile),
                "cow-scene" => CowScene(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private (Camera camera, Scene scene) Default(string objFileName)
        {
            CameraConfiguration cameraConfig = CameraConfiguration.Default;
            Camera camera = new Camera(cameraConfig);
            Scene scene = new SceneBuilder(objReader)
                .AddFileObjs(objFileName)
                .AddLight(new DirectionalLight(new Vector3(0, -1, 0)))
                .Build();
            return (camera, scene);
        }

        private (Camera camera, Scene scene) CowScene()
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

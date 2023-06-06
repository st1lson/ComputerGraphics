using RenderEngine.Basic;
using RenderEngine.Cli.CommandLineCommands;
using RenderEngine.Configurations;
using RenderEngine.Core;
using RenderEngine.Core.Scenes;
using RenderEngine.IO.Readers;
using RenderEngine.Lightings;
using RenderEngine.Models;
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
                "cow-fullhd" => CowSceneFullHDWithPointLight(objReader),
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

        private (Camera camera, Scene scene) CowSceneFullHDWithPointLight(IMeshReader objReader)
        {
            CameraConfiguration cameraConfig = CameraConfiguration.Default with
            {
                PixelHeight = 1080,
                PixelWidth = 1920
            };
            Camera camera = new Camera(cameraConfig);
            Scene scene = new SceneBuilder(objReader)
                .AddFileObjs("D:\\cow.obj")
                .AddLight(new PointLight(new Vector3(1, 1, 1), new Pixel(255, 0, 0), 1))
                .AddLight(new AmbientLight(new Pixel(255), 0.1f))
                .Build();
            return (camera, scene);
        }
    }
}

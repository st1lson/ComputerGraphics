using RenderEngine.Transformer;

namespace RenderEngine.Core
{
    public static class ExtensionCore
    {
        public static Scene Transform(this Scene scene, Transform transform)
        {
            foreach (var shape in scene.Shapes)
            {
                shape.Transform(transform);
            }

            foreach (var light in scene.Lighting)
            {
                light.Transform(transform);
            }

            return scene;
        }

        public static Camera Transform(this Camera camera, Transform transform)
        {
            camera.Orig = camera.Orig.Transform(transform);
            camera.Dir = camera.Dir.TransformAsDirection(transform);

            return camera;
        }
    }
}

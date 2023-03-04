using RenderEngine.Basic;

namespace RenderEngine.Core
{
    public class Renderer
    {
        public Camera Camera { get; init; }
        public Scene Scene { get; init; }

        public Renderer(Camera camera, Scene scene)
        {
            Camera = camera;
            Scene = scene;
        }

        public float[,] Render()
        {
            float[,] screen = new float[Camera.PixelHeight, Camera.PixelWidth];

            var verticalFovInRadians = Camera.VerticalFOV * (Math.PI / 180);
            //pixelDensity = pixels / (absolute size)
            float pixelDensity = (float)Camera.PixelHeight / 2 /
                                 ((float)Math.Tan(verticalFovInRadians) * Camera.FocalLength);

            Vector3 up = new Vector3(0, 0, 1 / pixelDensity);
            Vector3 right = new Vector3(1 / pixelDensity, 0, 0);

            Vector3 screenCenter = Camera.Orig + Camera.Dir * Camera.FocalLength;
            Vector3 screenOrig = screenCenter + up * Camera.PixelHeight / 2 - right * Camera.PixelWidth / 2;

            for (int i = 0; i < Camera.PixelHeight; i++)
            {
                for (int j = 0; j < Camera.PixelWidth; j++)
                {
                    var screenPoint = screenOrig - up * i + right * j;
                    var screenCameraRay = new Ray(Camera.Orig, screenPoint - Camera.Orig);
                    Vector3? intersectionPoint = null;
                    float minSquareDistance = float.MaxValue;
                    foreach (var shape in Scene.Shapes)
                    {
                        var intersection = shape.Intersects(screenCameraRay);
                        if (intersection == null)
                        {
                            continue;
                        }

                        var squareDist = Vector3.Dot(intersection.Value - Camera.Orig,
                            intersection.Value - Camera.Orig);
                        if (squareDist < minSquareDistance)
                        {
                            intersectionPoint = intersection;
                            minSquareDistance = squareDist;
                        }
                    }

                    if (intersectionPoint == null)
                    {
                        //TODO: something operation with light mb
                        screen[i, j] = 0;
                        continue;
                    }

                    //TODO: something operation with light mb
                    screen[i, j] = 1;
                }
            }

            return screen;
        }
    }
}

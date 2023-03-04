using RenderEngine.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void Render()
        {
            float[,] screen = new float[Camera.PixelHeight, Camera.PixelWidth];

            //pixelDensity = pixels / (absolute size)
            float pixelDensity = (Camera.PixelHeight / 2) / ((float)Math.Tan(Camera.VerticalFOV) * Camera.FocalLength);

            Vector3 up = new Vector3(0, 0, 1 / pixelDensity);
            Vector3 right = new Vector3(1 / pixelDensity, 0, 0);

            Vector3 ScreenCenter = Camera.Orig + Camera.Dir * Camera.FocalLength;
            Vector3 ScreenOrig = ScreenCenter - up * Camera.PixelHeight / 2 - right * Camera.PixelWidth / 2;

            for (int i = 0; i <= Camera.PixelHeight; i++)
            {
                for (int j = 0; j < Camera.PixelWidth; j++)
                {
                    var sceenPoint = ScreenOrig + up * i + right * j;
                    var screenCameraRay = new Ray(Camera.Orig, sceenPoint - Camera.Orig);
                    Vector3? intersectionPoint = null;
                    float minSquareDistance = float.MaxValue;
                    foreach (var shape in Scene.Shapes)
                    {
                        var intersaction = shape.Intersects(screenCameraRay);
                        if (intersaction == null) 
                        {
                            continue;
                        }

                        var squareDist = Vector3.Dot(intersaction! - Camera.Orig, intersaction! - Camera.Orig);
                        if (squareDist < minSquareDistance)
                        {
                            intersectionPoint = intersaction;
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
        }
    }
}

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

        }
    }
}

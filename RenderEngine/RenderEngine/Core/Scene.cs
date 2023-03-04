using RenderEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderEngine.Core
{
    public class Scene
    {
        public IReadOnlyList<IShape> Shapes { get; init; }
        public IReadOnlyList<ILighting> Lighting { get; init; }

        public Scene(List<IShape> shapes, List<ILighting> lighting)
        {
            Shapes = shapes;
            Lighting = lighting;
        }
    }
}

using RenderEngine.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderEngine.Interfaces
{
    public interface IMesh
    {
        public List<Vector3> Vertices { get; }

        public List<IShape> Faces { get; }
    }
}

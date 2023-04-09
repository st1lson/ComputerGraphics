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
        List<Vector3> Vertices { get; }

        List<IShape> Faces { get; }
    }
}

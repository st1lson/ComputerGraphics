using RenderEngine.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderEngine.Interfaces
{
    public interface ILighting
    {
        Vector3 RayLight { get; set; }
        float GetLight(IShape shape, Vector3? intersectionPoint);
    }
}

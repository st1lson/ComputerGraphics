using RenderEngine.Core;
using RenderEngine.Models;

namespace RenderEngine.Interfaces
{
    public interface IRenderer
    {
        IOptimizer Optimizer { get; init; }

        Bitmap Render();
    }
}

using RenderEngine.Core;
using RenderEngine.Models;

namespace RenderEngine.Interfaces
{
    public interface IRenderer
    {
        Camera Camera { get; init; }
        Scene Scene { get; init; }
        IOptimizer Optimizer { get; init; }

        Bitmap Render();
    }
}

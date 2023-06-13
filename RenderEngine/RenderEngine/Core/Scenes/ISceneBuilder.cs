using RenderEngine.Interfaces;
using RenderEngine.Transformer;

namespace RenderEngine.Core.Scenes
{
    public interface ISceneBuilder
    {
        ISceneBuilder AddLight(ILighting lighting);

        ISceneBuilder AddFileObjs(string objFile);

        ISceneBuilder AddTransforms(Transform transform);

        Scene Build();
    }
}

namespace RenderEngine.DependencyInjection;

public interface IContainer : IDisposable
{
    T GetService<T>();
}

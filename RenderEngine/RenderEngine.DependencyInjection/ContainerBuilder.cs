using RenderEngine.DependencyInjection.Enums;
using System.Collections.Immutable;

namespace RenderEngine.DependencyInjection;

public sealed class ContainerBuilder
{
    public ContainerBuilder() : this(new Dictionary<Type, ServiceDescription>())
    {
    }

    internal ContainerBuilder(Dictionary<Type, ServiceDescription> properties)
    {
        _properties = properties;
    }

    private readonly IDictionary<Type, ServiceDescription> _properties;

    public ContainerBuilder AddSingleton<T>(Func<T>? factory = null) where T : class
    {
        _properties.Add(
            typeof(T),
            new ServiceDescription(ServiceLifetime.Singleton, typeof(T), factory));

        return this;
    }

    public ContainerBuilder AddTransient<T>()
    {
        _properties.Add(
            typeof(T),
            new ServiceDescription(ServiceLifetime.Transient, typeof(T)));

        return this;
    }

    public IContainer Build() => new Container(_properties.ToImmutableDictionary());
}

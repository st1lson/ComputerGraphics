using RenderEngine.DependencyInjection.Enums;
using System.Reflection;

namespace RenderEngine.DependencyInjection;

public sealed class Container : IContainer
{
    private readonly IReadOnlyDictionary<Type, ServiceDescription> _properties;
    private readonly Dictionary<Type, object> _instances = new();

    internal Container(IReadOnlyDictionary<Type, ServiceDescription> properties)
    {
        _properties = properties;
        _properties.Values
            .Where(p => p.Lifetime == ServiceLifetime.Singleton)
            .Select(d => d.Type)
            .ToList()
            .ForEach(t => GetService(t));
    }

    public T GetService<T>()
    {
        return (T)GetService(typeof(T));
    }

    internal object GetService(Type type)
    {
        if (!_properties.TryGetValue(type, out var description))
        {
            throw new InvalidOperationException($"Service of type {type} is not registered");
        }

        if (description.Lifetime == ServiceLifetime.Singleton && _instances.TryGetValue(type, out var singleton))
        {
            return singleton;
        }

        var instance = Activator.CreateInstance(type) ?? throw new Exception();

        foreach (var member in type.GetMembers(BindingFlags.Public | BindingFlags.Instance))
        {
            if (member.GetCustomAttribute(typeof(ServiceAttribute)) == null)
            {
                continue;
            }

            if (member is PropertyInfo property)
            {
                var propertyType = property.PropertyType;
                var service = GetService(propertyType);
                property.SetValue(instance, service);
            }
        }

        _instances.Add(type, instance);

        return instance;
    }

    public void Dispose()
    {
        foreach (var instance in _instances.Values)
        {
            if (instance is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}

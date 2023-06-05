using RenderEngine.DependencyInjection.Enums;

namespace RenderEngine.DependencyInjection;

internal sealed record ServiceDescription(ServiceLifetime Lifetime, Type Type);

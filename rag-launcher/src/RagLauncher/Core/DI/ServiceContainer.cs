namespace RagLauncher.Core.DI;

internal sealed class ServiceContainer
{
    private readonly Dictionary<Type, object> _services = new();

    public void AddSingleton<T>(T instance)
        where T : class
    {
        _services[typeof(T)] = instance;
    }

    public T Get<T>()
        where T : class
    {
        return (T)_services[typeof(T)];
    }
}
namespace RagLauncher.HostedServices;

internal sealed class HostedServiceManager
{
    private readonly List<IHostedService> _services = new();

    public void Register(IHostedService service)
    {
        _services.Add(service);
    }

    public async Task StartAsync()
    {
        foreach (var service in _services)
        {
            await service.StartAsync();
        }
    }

    public async Task StopAsync()
    {
        foreach (var service in _services.AsEnumerable().Reverse())
        {
            await service.StopAsync();
        }
    }
}
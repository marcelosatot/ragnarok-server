namespace RagLauncher.Core.Hosting;

internal sealed class Host
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
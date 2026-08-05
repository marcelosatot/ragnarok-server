using RagLauncher.Core;
using RagLauncher.Core.DI;
using RagLauncher.Runtime;

namespace RagLauncher.HostedServices;

internal sealed class RuntimeHostedService : IHostedService
{
    private readonly ServiceContainer _services;
    private readonly LauncherContext _context;

    public RuntimeHostedService(
        ServiceContainer services,
        LauncherContext context)
    {
        _services = services;
        _context = context;
    }

    public Task StartAsync()
    {
        _ = _services
            .Get<RuntimeMonitor>()
            .StartAsync(_context.Runtime);

        return Task.CompletedTask;
    }

    public Task StopAsync()
    {
        return Task.CompletedTask;
    }
}
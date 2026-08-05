using RagLauncher.Core;
using RagLauncher.Core.DI;
using RagLauncher.Dashboard;

namespace RagLauncher.HostedServices;

internal sealed class DashboardHostedService : IHostedService
{
    private readonly ServiceContainer _services;
    private readonly LauncherContext _context;

    public DashboardHostedService(
        ServiceContainer services,
        LauncherContext context)
    {
        _services = services;
        _context = context;
    }

    public Task StartAsync()
    {
        _ = _services
            .Get<DashboardService>()
            .StartAsync(_context);

        return Task.CompletedTask;
    }

    public Task StopAsync()
    {
        return Task.CompletedTask;
    }
}
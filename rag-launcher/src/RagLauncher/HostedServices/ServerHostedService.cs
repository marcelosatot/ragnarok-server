using RagLauncher.Core;
using RagLauncher.Core.Hosting;
using RagLauncher.Servers;

namespace RagLauncher.HostedServices;

internal sealed class ServerHostedService : IHostedService
{
    private readonly LauncherContext _context;
    private readonly ServerManager _server = new();

    public ServerHostedService(LauncherContext context)
    {
        _context = context;
    }

    public async Task StartAsync()
    {
        await _server.StartAsync(_context.Configuration!);
    }

    public Task StopAsync()
    {
        _server.Stop();

        return Task.CompletedTask;
    }
}
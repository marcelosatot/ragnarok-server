using RagLauncher.Logging;
using RagLauncher.Models;

namespace RagLauncher.Core;

internal sealed class LauncherHost
{
    private readonly LauncherContext _context = new();
    private readonly LauncherServices _services = new();

    public async Task RunAsync()
    {
        Logger.Info("Starting Rag Launcher...");
        Logger.Line();

        await InitializeInfrastructure();

        await StartGameServers();

        StartBackgroundServices();

        Logger.Success("Launcher READY");
        Logger.Info("Press CTRL+C to stop.");

        await WaitForShutdown();
    }

    private async Task InitializeInfrastructure()
    {
        await _services.DatabaseManager.InitializeAsync(
            @"C:\Users\satom\Documents\ragnarok-server\mariadb");

        await _services.DatabaseInstaller.EnsureDatabaseAsync();

        await _services.AccountService.EnsureAdminAccountAsync();

        _context.Configuration = _services.ConfigurationService.Load();

        _services.ValidationService.Validate(_context.Configuration);
    }

    private async Task StartGameServers()
    {
        await _services.ServerManager.StartAsync(_context.Configuration!);
    }

    private void StartBackgroundServices()
    {
        _ = _services.RuntimeMonitor.StartAsync(_context.Runtime);

        _ = _services.Dashboard.StartAsync(_context);
    }

    private static async Task WaitForShutdown()
    {
        await Task.Delay(Timeout.Infinite);
    }
}
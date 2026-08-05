using RagLauncher.Core.Hosting;
using RagLauncher.Database;
using RagLauncher.Accounts;

namespace RagLauncher.HostedServices;

internal sealed class DatabaseHostedService : IHostedService
{
    private readonly DatabaseManager _manager = new();
    private readonly DatabaseInstaller _installer = new();
    private readonly AccountService _accounts = new();

    public async Task StartAsync()
    {
        await _manager.InitializeAsync(
            @"C:\Users\satom\Documents\ragnarok-server\mariadb");

        await _installer.EnsureDatabaseAsync();

        await _accounts.EnsureAdminAccountAsync();
    }

    public Task StopAsync()
    {
        return Task.CompletedTask;
    }
}
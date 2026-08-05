using RagLauncher.Accounts;
using RagLauncher.Core.DI;
using RagLauncher.Core.Environment;
using RagLauncher.Database;

namespace RagLauncher.HostedServices;

internal sealed class DatabaseHostedService : IHostedService
{
    private readonly ServiceContainer _services;

    public DatabaseHostedService(ServiceContainer services)
    {
        _services = services;
    }

    public async Task StartAsync()
    {
        var env = _services.Get<AppEnvironment>();

        await _services.Get<DatabaseManager>()
            .InitializeAsync(env.MariaDb);

        await _services.Get<DatabaseInstaller>()
            .EnsureDatabaseAsync();

        await _services.Get<AccountService>()
            .EnsureAdminAccountAsync();
    }

    public Task StopAsync()
    {
        return Task.CompletedTask;
    }
}
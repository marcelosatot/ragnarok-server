using RagLauncher.CommandLine;
using RagLauncher.Configuration;
using RagLauncher.Core.DI;
using RagLauncher.Dashboard;
using RagLauncher.Logging;
using RagLauncher.Runtime;
using RagLauncher.Accounts;
using RagLauncher.Database;
using RagLauncher.Servers;
using RagLauncher.Validation;
using RagLauncher.Core.Environment;

namespace RagLauncher.Core;

internal sealed class LauncherHost
{
    private readonly LauncherContext _context = new();

    private readonly ServiceContainer _services =
        Bootstrapper.Build();

    public async Task RunAsync()
    {
        Logger.Info("Starting Rag Launcher...");
        Logger.Line();

        await InitializeAsync();

        _ = _services
            .Get<RuntimeMonitor>()
            .StartAsync(_context.Runtime);

        _ = _services
            .Get<DashboardService>()
            .StartAsync(_context);

        _ = _services
            .Get<CommandHost>()
            .RunAsync();

        _ = _services
            .Get<ServerWatcher>()
            .RunAsync();

        Logger.Success("Launcher READY");
        Logger.Info("Press CTRL+C to stop.");

        await Task.Delay(Timeout.Infinite);
    }

    private async Task InitializeAsync()
    {
            await _services
                .Get<DatabaseManager>()
                .InitializeAsync(
                    AppPaths.MariaDb);

        await _services
            .Get<DatabaseInstaller>()
            .EnsureDatabaseAsync();

        await _services
            .Get<AccountService>()
            .EnsureAdminAccountAsync();

        var configuration =
            _services
                .Get<ConfigurationService>()
                .Load(
                    _services.Get<AppEnvironment>());

        _context.Configuration = configuration;

        _services
            .Get<ValidationService>()
            .Validate(configuration);

        await _services
            .Get<ServerManager>()
            .StartAsync(configuration);
    }
}
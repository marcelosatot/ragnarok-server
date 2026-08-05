using RagLauncher.Core.DI;
using RagLauncher.Logging;
using RagLauncher.HostedServices;
using RagLauncher.CommandLine;

namespace RagLauncher.Core;

internal sealed class LauncherHost
{
    private readonly LauncherContext _context = new();

    private readonly ServiceContainer _services =
        Bootstrapper.Build();


    private void RegisterHostedServices()
{
    var manager = _services.Get<HostedServiceManager>();

    manager.Register(
        new DatabaseHostedService(_services));

    manager.Register(
        new ConfigurationHostedService(
            _services,
            _context));

    manager.Register(
        new ServerHostedService(
            _services,
            _context));

    manager.Register(
        new RuntimeHostedService(
            _services,
            _context));

    manager.Register(
        new DashboardHostedService(
            _services,
            _context));
}

    public async Task RunAsync()
    {
        Logger.Info("Starting Rag Launcher...");
        Logger.Line();

        RegisterHostedServices();

        await _services
            .Get<HostedServiceManager>()
            .StartAsync();

        Logger.Success("Launcher READY");
        _ = _services
    .Get<CommandHost>()
    .RunAsync();
        Logger.Info("Press CTRL+C to stop.");

        _services.Get<EventLog>()
        .Add("Launcher started");
        

        await WaitForShutdown();
    }

    private static async Task WaitForShutdown()
    {
        await Task.Delay(Timeout.Infinite);
    }
}
using RagLauncher.Configuration;
using RagLauncher.Core;
using RagLauncher.Core.DI;
using RagLauncher.Core.Environment;
using RagLauncher.Validation;

namespace RagLauncher.HostedServices;

internal sealed class ConfigurationHostedService : IHostedService
{
    private readonly ServiceContainer _services;
    private readonly LauncherContext _context;

    public ConfigurationHostedService(
        ServiceContainer services,
        LauncherContext context)
    {
        _services = services;
        _context = context;
    }

    public Task StartAsync()
    {
        var env = _services.Get<AppEnvironment>();

        _context.Configuration =
            _services.Get<ConfigurationService>().Load(env);

        _services
            .Get<ValidationService>()
            .Validate(_context.Configuration);

        return Task.CompletedTask;
    }

    public Task StopAsync()
    {
        return Task.CompletedTask;
    }
}
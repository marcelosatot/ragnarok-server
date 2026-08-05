using RagLauncher.Accounts;
using RagLauncher.Configuration;
using RagLauncher.Core.Environment;
using RagLauncher.Database;
using RagLauncher.Dashboard;
using RagLauncher.HostedServices;
using RagLauncher.Logging;
using RagLauncher.Runtime;
using RagLauncher.Servers;
using RagLauncher.Validation;
using RagLauncher.CommandLine;


namespace RagLauncher.Core.DI;

internal static class Bootstrapper
{
    public static ServiceContainer Build()
    {
        var services = new ServiceContainer();

        var env = new AppEnvironment();

        services.AddSingleton(env);

        services.AddSingleton(new EventLog());

        services.AddSingleton(new DatabaseManager());

        services.AddSingleton(new DatabaseInstaller(env));

        services.AddSingleton(new AccountService());

        services.AddSingleton(new ConfigurationService());

        services.AddSingleton(new ValidationService());

        services.AddSingleton(new RuntimeMonitor());

        services.AddSingleton(new DashboardService());

        services.AddSingleton(new HostedServiceManager());

        services.AddSingleton(
        new CommandHost(
            services.Get<ServerManager>()));

        return services;
    }
}
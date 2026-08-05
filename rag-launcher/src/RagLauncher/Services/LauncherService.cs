using RagLauncher.Configuration;
using RagLauncher.Database;
using RagLauncher.Servers;
using RagLauncher.Validation;
using RagLauncher.Accounts;

namespace RagLauncher.Services;

internal class LauncherService
{
    public async Task Start()
    {
        Console.WriteLine("[Launcher] Starting...");
        Console.WriteLine();

        var database = new DatabaseManager();

        await database.InitializeAsync(
            @"C:\Users\satom\Documents\ragnarok-server\mariadb");

        var installer = new DatabaseInstaller();
        await installer.EnsureDatabaseAsync();

        var accountService = new AccountService();
        await accountService.EnsureAdminAccountAsync();

        var configurationService = new ConfigurationService();

        var configuration = configurationService.Load();

        Console.WriteLine($"Server Name : {configuration.ServerName}");
        Console.WriteLine($"Version     : {configuration.Version}");
        Console.WriteLine();

        var validation = new ValidationService();

        validation.Validate(configuration);

        Console.WriteLine();

        var serverManager = new ServerManager();

        await serverManager.StartAsync(configuration);

        Console.WriteLine();
        Console.WriteLine("[Launcher] READY");
        Console.WriteLine("[Launcher] Press CTRL+C to stop.");

        await Task.Delay(Timeout.Infinite);
    }
}
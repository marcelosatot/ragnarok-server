using RagLauncher.Configuration;
using RagLauncher.Database;
using RagLauncher.Processes;
using RagLauncher.Validation;

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

        var configurationService = new ConfigurationService();

        var configuration = configurationService.Load();

        Console.WriteLine($"Server Name : {configuration.ServerName}");
        Console.WriteLine($"Version     : {configuration.Version}");
        Console.WriteLine();

        var validation = new ValidationService();

        validation.Validate(configuration);

        Console.WriteLine();

        var processManager = new ProcessManager();

        await processManager.InitializeAsync(configuration);

        Console.WriteLine();

        Console.WriteLine("[Launcher] Ready!");
    }
}
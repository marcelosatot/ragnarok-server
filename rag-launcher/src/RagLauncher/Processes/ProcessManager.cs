using System.Diagnostics;
using RagLauncher.Models;
using RagLauncher.Runtime;

namespace RagLauncher.Processes;

internal class ProcessManager
{
    private readonly List<ServerProcess> _runningServers = new();

    public async Task InitializeAsync(ServerConfiguration configuration)
    {
        Console.WriteLine("[Process] Initializing...");
        Console.WriteLine();

        KillExistingServers();

        var workingDirectory = configuration.RathenaDirectory;

        await StartServer(
            new ServerDefinition
            {
                Executable = "login-server.exe",
                ReadyMessage = "ready"
            },
            workingDirectory);

        await StartServer(
            new ServerDefinition
            {
                Executable = "char-server.exe",
                ReadyMessage = "ready"
            },
            workingDirectory);

        await StartServer(
            new ServerDefinition
            {
                Executable = "map-server.exe",
                ReadyMessage = "ready"
            },
            workingDirectory);
    }

    private async Task StartServer(
        ServerDefinition definition,
        string workingDirectory)
    {
        Console.WriteLine($"[Process] Starting {definition.Executable}");

        var process = new ServerProcess(
            workingDirectory,
            definition.Executable,
            definition.ReadyMessage);

        _runningServers.Add(process);

        process.OutputReceived += line =>
            Console.WriteLine($"[{definition.Executable}] {line}");

        process.Start();

        await process.WaitUntilReadyAsync();

        Console.WriteLine($"[Process] {definition.Executable} READY");
        Console.WriteLine();
    }

    private static void KillExistingServers()
    {
        var names = new[]
        {
            "login-server",
            "char-server",
            "map-server"
        };

        foreach (var name in names)
        {
            foreach (var process in Process.GetProcessesByName(name))
            {
                try
                {
                    process.Kill(true);
                    process.WaitForExit();
                }
                catch
                {
                }
            }
        }
    }

    public void StopAll()
    {
        foreach (var process in _runningServers)
        {
            try
            {
                process.Stop();
            }
            catch
            {
            }
        }

        _runningServers.Clear();
    }
}
using System.Diagnostics;
using RagLauncher.Models;
using RagLauncher.Runtime;
using RagLauncher.Logging;

namespace RagLauncher.Processes;

internal sealed class ProcessManager
{
    private readonly Dictionary<string, ServerProcess> _servers = new();

    private string _workingDirectory = string.Empty;

    public async Task InitializeAsync(ServerConfiguration configuration)
    {
        Console.WriteLine("[Process] Initializing...");
        Console.WriteLine();

        KillExistingServers();

        _workingDirectory = configuration.RathenaDirectory;

        await StartServer(
            new ServerDefinition
            {
                Executable = "login-server.exe",
                ReadyMessage = "ready"
            });

        await StartServer(
            new ServerDefinition
            {
                Executable = "char-server.exe",
                ReadyMessage = "ready"
            });

        await StartServer(
            new ServerDefinition
            {
                Executable = "map-server.exe",
                ReadyMessage = "ready"
            });
    }

    private async Task StartServer(ServerDefinition definition)
    {
        Console.WriteLine($"[Process] Starting {definition.Executable}");

        var process = new ServerProcess(
            _workingDirectory,
            definition.Executable,
            definition.ReadyMessage);

        process.OutputReceived += line =>
            Console.WriteLine($"[{definition.Executable}] {line}");

        process.Start();

        await process.WaitUntilReadyAsync();

        _servers[definition.Executable] = process;

        Console.WriteLine($"[Process] {definition.Executable} READY");
        Console.WriteLine();
    }

    public bool IsRunning(string executable)
    {
        return Process.GetProcessesByName(
            Path.GetFileNameWithoutExtension(executable)).Any();
    }

    public bool IsLoginRunning() => IsRunning("login-server.exe");

    public bool IsCharRunning() => IsRunning("char-server.exe");

    public bool IsMapRunning() => IsRunning("map-server.exe");

    public async Task RestartAsync(string executable)
    {
        Stop(executable);

        await StartServer(new ServerDefinition
        {
            Executable = executable,
            ReadyMessage = "ready"
        });
    }

    public Task RestartLoginAsync()
        => RestartAsync("login-server.exe");

    public Task RestartCharAsync()
        => RestartAsync("char-server.exe");

    public Task RestartMapAsync()
        => RestartAsync("map-server.exe");

    public void Stop(string executable)
    {
        if (!_servers.TryGetValue(executable, out var process))
            return;

        process.Stop();

        _servers.Remove(executable);
    }

    public void StopAll()
    {
        foreach (var process in _servers.Values.ToList())
        {
            try
            {
                process.Stop();
            }
            catch
            {
            }
        }

        _servers.Clear();
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
}
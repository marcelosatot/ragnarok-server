using RagLauncher.Models;
using RagLauncher.Processes;

namespace RagLauncher.Servers;

internal sealed class ServerManager
{
    private readonly ProcessManager _processManager;

    public ServerManager()
    {
        _processManager = new ProcessManager();
    }

    public async Task StartAsync(ServerConfiguration configuration)
    {
        Console.WriteLine("[Server] Starting servers...");
        Console.WriteLine();

        await _processManager.InitializeAsync(configuration);

        Console.WriteLine();
        Console.WriteLine("[Server] All servers are ONLINE");
    }

    public void Stop()
    {
        _processManager.StopAll();
    }

    public bool LoginOnline()
    {
        return _processManager.IsLoginRunning();
    }

    public bool CharOnline()
    {
        return _processManager.IsCharRunning();
    }

    public bool MapOnline()
    {
        return _processManager.IsMapRunning();
    }

    public Task RestartLoginAsync()
    {
        return _processManager.RestartLoginAsync();
    }

    public Task RestartCharAsync()
    {
        return _processManager.RestartCharAsync();
    }

    public Task RestartMapAsync()
    {
        return _processManager.RestartMapAsync();
    }
}
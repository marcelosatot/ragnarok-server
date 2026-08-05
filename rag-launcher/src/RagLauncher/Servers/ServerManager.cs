using RagLauncher.Models;
using RagLauncher.Processes;

namespace RagLauncher.Servers;

internal class ServerManager
{
    private readonly ProcessManager _processManager = new();

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
}
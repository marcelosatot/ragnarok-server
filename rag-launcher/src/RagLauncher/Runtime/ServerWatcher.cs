using RagLauncher.Servers;

namespace RagLauncher.Runtime;

internal sealed class ServerWatcher
{
    private readonly ServerManager _serverManager;

    public ServerWatcher(ServerManager serverManager)
    {
        _serverManager = serverManager;
    }

    public async Task RunAsync()
    {
        while (true)
        {
            await CheckAsync();

            await Task.Delay(1000);
        }
    }

    private async Task CheckAsync()
    {
        if (!_serverManager.LoginOnline())
        {
            Console.WriteLine("[Watcher] Login Server OFFLINE");
            await _serverManager.RestartLoginAsync();
        }

        if (!_serverManager.CharOnline())
        {
            Console.WriteLine("[Watcher] Char Server OFFLINE");
            await _serverManager.RestartCharAsync();
        }

        if (!_serverManager.MapOnline())
        {
            Console.WriteLine("[Watcher] Map Server OFFLINE");
            await _serverManager.RestartMapAsync();
        }
    }
}
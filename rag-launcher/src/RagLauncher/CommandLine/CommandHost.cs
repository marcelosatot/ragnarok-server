using RagLauncher.Servers;

namespace RagLauncher.CommandLine;

internal sealed class CommandHost
{
    private readonly ServerManager _server;

    public CommandHost(ServerManager server)
    {
        _server = server;
    }

    public async Task RunAsync()
    {
        while (true)
        {
            Console.Write("> ");

            var input = Console.ReadLine()?.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(input))
                continue;

            switch (input)
            {
                case "help":
                    ShowHelp();
                    break;

                case "status":
                    ShowStatus();
                    break;

                case "restart login":
                    await _server.RestartLoginAsync();
                    break;

                case "restart char":
                    await _server.RestartCharAsync();
                    break;

                case "restart map":
                    await _server.RestartMapAsync();
                    break;

                case "exit":
                    return;

                case "stop":
                    _server.Stop();
                    break;

                default:
                    Console.WriteLine("Unknown command.");
                    break;
            }
        }
    }

    private void ShowStatus()
    {
        Console.WriteLine();

        Console.WriteLine($"Login : {(_server.LoginOnline() ? "ONLINE" : "OFFLINE")}");
        Console.WriteLine($"Char  : {(_server.CharOnline() ? "ONLINE" : "OFFLINE")}");
        Console.WriteLine($"Map   : {(_server.MapOnline() ? "ONLINE" : "OFFLINE")}");

        Console.WriteLine();
    }

    private static void ShowHelp()
{
    Console.WriteLine();

    Console.WriteLine("Available commands");
    Console.WriteLine("--------------------------------");

    Console.WriteLine("help            Show this help");
    Console.WriteLine("status          Show server status");
    Console.WriteLine("restart login   Restart Login Server");
    Console.WriteLine("restart char    Restart Char Server");
    Console.WriteLine("restart map     Restart Map Server");
    Console.WriteLine("stop            Stop all servers");
    Console.WriteLine("exit            Exit command mode");

    Console.WriteLine();
}
}
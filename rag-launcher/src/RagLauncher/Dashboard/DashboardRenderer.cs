using RagLauncher.Core;
using RagLauncher.Logging;

namespace RagLauncher.Dashboard;

internal sealed class DashboardRenderer
{
    public void Render(LauncherContext context)
    {
        Console.Clear();

        Logger.Title("Rag Launcher");

        Console.WriteLine();

        Console.WriteLine("Server Status");
        Console.WriteLine("-----------------------------");

        Print("Login", context.Runtime.State.LoginOnline);
        Print("Char ", context.Runtime.State.CharOnline);
        Print("Map  ", context.Runtime.State.MapOnline);

        Console.WriteLine();

        Console.WriteLine("Memory");
        Console.WriteLine("-----------------------------");

        Console.WriteLine($"Login : {context.Runtime.State.LoginMemory} MB");
        Console.WriteLine($"Char  : {context.Runtime.State.CharMemory} MB");
        Console.WriteLine($"Map   : {context.Runtime.State.MapMemory} MB");

        Console.WriteLine();

        Console.WriteLine("Players");
        Console.WriteLine("-----------------------------");

        Console.WriteLine($"Online : {context.Runtime.State.PlayersOnline}");

        Console.WriteLine();

        Console.WriteLine("Uptime");
        Console.WriteLine("-----------------------------");

        Console.WriteLine(DateTime.Now - context.Runtime.State.StartedAt);

        Console.WriteLine();

        Console.WriteLine("CTRL+C - Exit");
    }

    private static void Print(string server, bool online)
    {
        Console.Write(server.PadRight(8));

        if (online)
            Console.WriteLine("ONLINE");
        else
            Console.WriteLine("OFFLINE");
    }
}
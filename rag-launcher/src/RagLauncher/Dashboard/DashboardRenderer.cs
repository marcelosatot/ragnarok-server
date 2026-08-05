using RagLauncher.Core;
using RagLauncher.Logging;

namespace RagLauncher.Dashboard;

internal sealed class DashboardRenderer
{
    public void Render(LauncherContext context)
    {
        Console.SetCursorPosition(0, 0);

        Logger.Title("Rag Launcher");

        Console.WriteLine();

        DrawStatus(context);

        Console.WriteLine();

        DrawMemory(context);

        Console.WriteLine();

        DrawRuntime(context);

        Console.WriteLine();

        Console.WriteLine("Press CTRL+C to stop.");
    }

    private static void DrawStatus(LauncherContext context)
    {
        Console.WriteLine("STATUS");
        Console.WriteLine("────────────────────────────────────");

        Print("Database", context.Runtime.State.DatabaseOnline);
        Print("Login", context.Runtime.State.LoginOnline);
        Print("Char", context.Runtime.State.CharOnline);
        Print("Map", context.Runtime.State.MapOnline);
    }

    private static void DrawMemory(LauncherContext context)
    {
        Console.WriteLine("MEMORY");
        Console.WriteLine("────────────────────────────────────");

        Console.WriteLine($"Launcher : {context.Runtime.State.MemoryMB:F1} MB");
        Console.WriteLine($"Login    : {context.Runtime.State.LoginMemory} MB");
        Console.WriteLine($"Char     : {context.Runtime.State.CharMemory} MB");
        Console.WriteLine($"Map      : {context.Runtime.State.MapMemory} MB");
    }

    private static void DrawRuntime(LauncherContext context)
    {
        Console.WriteLine("RUNTIME");
        Console.WriteLine("────────────────────────────────────");

        Console.WriteLine($"Uptime   : {context.Runtime.State.Uptime:dd\\.hh\\:mm\\:ss}");
        Console.WriteLine($"Players  : {context.Runtime.State.PlayersOnline}");
        Console.WriteLine($"Restarts : {context.Runtime.State.RestartCount}");
    }

    private static void Print(string name, bool online)
    {
        var icon = online ? "[OK]" : "[--]";
        var status = online ? "ONLINE" : "OFFLINE";

        Console.WriteLine($"{icon,-6} {name,-10} {status}");
    }
}
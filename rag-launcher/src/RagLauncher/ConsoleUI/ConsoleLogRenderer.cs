using RagLauncher.Logging;

namespace RagLauncher.CommandLineUI;

internal sealed class ConsoleLogRenderer
{
    public void Subscribe(LogService service)
    {
        service.MessageReceived += Render;
    }

    private static void Render(LogMessage log)
    {
        Console.ForegroundColor = log.Level switch
        {
            LogLevel.Success => ConsoleColor.Green,
            LogLevel.Warning => ConsoleColor.Yellow,
            LogLevel.Error => ConsoleColor.Red,
            _ => ConsoleColor.Gray
        };

        Console.WriteLine(
            $"[{log.Time:HH:mm:ss}] [{log.Source}] {log.Message}");

        Console.ResetColor();
    }
}
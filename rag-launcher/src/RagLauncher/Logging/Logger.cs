namespace RagLauncher.Logging;

internal static class Logger
{
    public static void Title(string title)
    {
        Console.Clear();

        Console.WriteLine("========================================");
        Console.WriteLine($" {title}");
        Console.WriteLine("========================================");
    }

    public static void Line()
    {
        Console.WriteLine();
    }

    public static void Info(string message)
    {
        Write("[INFO]", ConsoleColor.Cyan, message);
    }

    public static void Success(string message)
    {
        Write("[ OK ]", ConsoleColor.Green, message);
    }

    public static void Warning(string message)
    {
        Write("[WARN]", ConsoleColor.Yellow, message);
    }

    public static void Error(string message)
    {
        Write("[FAIL]", ConsoleColor.Red, message);
    }

    private static void Write(string prefix, ConsoleColor color, string message)
    {
        var current = Console.ForegroundColor;

        Console.ForegroundColor = color;
        Console.Write(prefix);
        Console.ForegroundColor = current;

        Console.Write(" ");
        Console.WriteLine(message);
    }
}
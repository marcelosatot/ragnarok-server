using RagLauncher.Services;

namespace RagLauncher;

internal class Program
{
    static async Task Main(string[] args)
    {
        Console.Title = "Rag Launcher";

        Console.WriteLine("========================================");
        Console.WriteLine("              Rag Launcher");
        Console.WriteLine("========================================");
        Console.WriteLine();

        Console.WriteLine("Version: 0.1.0");
        Console.WriteLine();

        var launcher = new LauncherService();

        await launcher.Start();

        Console.WriteLine();
        Console.WriteLine("Press ENTER to exit...");

        Console.ReadLine();
    }
}
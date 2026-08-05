using RagLauncher.Models;
using RagLauncher.Runtime;
using System.Threading.Tasks;

namespace RagLauncher.Processes;

internal class ProcessManager
{
    public async Task InitializeAsync(ServerConfiguration configuration)
    {

        Console.WriteLine("[Process] Initializing...");

        await StartServer(
            configuration.Executables.Login,
            configuration.RathenaDirectory);

        await StartServer(
            configuration.Executables.Char,
            configuration.RathenaDirectory);

        await StartServer(
            configuration.Executables.Map,
            configuration.RathenaDirectory);

        Console.WriteLine();

        Console.WriteLine("[Process] Starting Login Server...");

        var login = new ServerProcess(
            configuration.RathenaDirectory,
            configuration.Executables.Login.Executable,
            configuration.Executables.Login.ReadyMessage);

        login.OutputReceived += line =>
        {
            Console.WriteLine($"[LOGIN] {line}");
        };

        login.Start();

        await login.WaitUntilReadyAsync();

        Console.WriteLine();
        Console.WriteLine("[Process] Login Server READY");
    }

    private static void ValidateExecutable(string directory, string executable)
    {
        var file = Path.Combine(directory, executable);

        if (!File.Exists(file))
            throw new FileNotFoundException(file);

        Console.WriteLine($"[Process] {executable} OK");
    }

    private async Task StartServer(ServerDefinition server, string workingDirectory)
    {
    Console.WriteLine();
    Console.WriteLine($"[Process] Starting {server.Executable}");

    var process = new ServerProcess(
        workingDirectory,
        server.Executable,
        server.ReadyMessage);

    process.OutputReceived += line =>
    {
        Console.WriteLine($"[{server.Executable}] {line}");
    };

    process.Start();

    await process.WaitUntilReadyAsync();

    Console.WriteLine($"[Process] {server.Executable} READY");
    }
}
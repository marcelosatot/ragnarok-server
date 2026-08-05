using System.Diagnostics;
using System.Net.Sockets;

namespace RagLauncher.Database;

internal class DatabaseManager
{
    private Process? _process;

    public async Task InitializeAsync(string mariadbRoot)
    {
        Start(mariadbRoot);

        await WaitForDatabaseAsync();
    }

    private async Task WaitForDatabaseAsync()
    {
        Console.WriteLine("[Database] Waiting for MariaDB...");

        while (true)
        {
            try
            {
                using var client = new TcpClient();

                await client.ConnectAsync("127.0.0.1", 3306);

                Console.WriteLine("[Database] MariaDB READY");

                return;
            }
            catch
            {
                await Task.Delay(500);
            }

            if (_process is { HasExited: true })
            {
                throw new Exception("MariaDB exited before becoming READY.");
            }
        }
    }

    public void Start(string mariadbRoot)
    {
        Console.WriteLine("[Database] Starting MariaDB...");

        var exe = Path.Combine(
            mariadbRoot,
            "bin",
            "mariadbd.exe");

        var config = Path.Combine(
            mariadbRoot,
            "my.ini");

        _process = new Process();

        _process.StartInfo = new ProcessStartInfo
        {
            FileName = exe,

            Arguments =
                $"--defaults-file=\"{config}\"",

            WorkingDirectory =
                Path.Combine(mariadbRoot, "bin"),

            UseShellExecute = true,
            CreateNoWindow = false
        };

        _process.Start();
    }

    public void Stop()
    {
        if (_process == null)
            return;

        if (!_process.HasExited)
            _process.Kill(true);
    }
}
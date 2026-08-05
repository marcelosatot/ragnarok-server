using System.Diagnostics;
using System.Net.Sockets;

namespace RagLauncher.Database;

internal class DatabaseManager
{
    private Process? _process;

    public async Task InitializeAsync(string mariadbRoot)
    {
        if (await IsRunningAsync())
        {
            Console.WriteLine("[Database] MariaDB already running.");
            return;
        }

        Start(mariadbRoot);

        await WaitForDatabaseAsync();
    }

    private async Task<bool> IsRunningAsync()
    {
        try
        {
            using var client = new TcpClient();

            await client.ConnectAsync("127.0.0.1", 3306);

            return true;
        }
        catch
        {
            return false;
        }
    }

    private async Task WaitForDatabaseAsync()
    {
        Console.WriteLine("[Database] Waiting for MariaDB...");

        while (true)
        {
            if (await IsRunningAsync())
            {
                Console.WriteLine("[Database] MariaDB READY");
                return;
            }

            if (_process != null && _process.HasExited)
            {
                throw new Exception("MariaDB exited before becoming READY.");
            }

            await Task.Delay(500);
        }
    }

private void Start(string mariadbRoot)
{
    Console.WriteLine("[Database] Starting MariaDB...");

    var exe = Path.Combine(mariadbRoot, "bin", "mariadbd.exe");
    var config = Path.Combine(mariadbRoot, "my.ini");

    _process = new Process();

    _process.StartInfo = new ProcessStartInfo
    {
        FileName = exe,
        Arguments = $"--defaults-file=\"{config}\" --console",
        WorkingDirectory = Path.Combine(mariadbRoot, "bin"),
        RedirectStandardOutput = true,
        RedirectStandardError = true,
        UseShellExecute = false,
        CreateNoWindow = true
    };

    _process.OutputDataReceived += (_, e) =>
    {
        if (!string.IsNullOrWhiteSpace(e.Data))
            Console.WriteLine($"[MariaDB] {e.Data}");
    };

    _process.ErrorDataReceived += (_, e) =>
    {
        if (!string.IsNullOrWhiteSpace(e.Data))
            Console.WriteLine($"[MariaDB] {e.Data}");
    };

    _process.Start();

    _process.BeginOutputReadLine();
    _process.BeginErrorReadLine();
}

    public void Stop()
    {
        if (_process == null)
            return;

        if (!_process.HasExited)
            _process.Kill(true);
    }
}
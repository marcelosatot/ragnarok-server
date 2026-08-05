using System.Diagnostics;

namespace RagLauncher.Runtime;

internal class ServerProcess
{
    private readonly string _workingDirectory;
    private readonly string _executable;

    private Process? _process;

    private readonly TaskCompletionSource<bool> _ready = new();

    private readonly string _readyMessage;

    public Task WaitUntilReadyAsync()
    {
        return _ready.Task;
    }

    public event Action<string>? OutputReceived;

    public ServerProcess(
        string workingDirectory,
        string executable,
        string readyMessage)
    {
        _workingDirectory = workingDirectory;
        _executable = executable;
        _readyMessage = readyMessage;
    }

    public void Start()
    {
        _process = new Process();

        _process.StartInfo = new ProcessStartInfo
        {
            FileName = Path.Combine(_workingDirectory, _executable),
            WorkingDirectory = _workingDirectory,

            RedirectStandardOutput = true,
            RedirectStandardError = true,

            UseShellExecute = false,
            CreateNoWindow = true
        };

       _process.OutputDataReceived += (_, e) =>
        {
            if (e.Data is null)
                return;

            OutputReceived?.Invoke(e.Data);

            if (e.Data.Contains(_readyMessage, StringComparison.OrdinalIgnoreCase))
            {
                _ready.TrySetResult(true);
            }
        };

        _process.ErrorDataReceived += (_, e) =>
        {
            if (e.Data is null)
                return;

            OutputReceived?.Invoke($"ERROR: {e.Data}");
        };

        _process.Start();
        _process.EnableRaisingEvents = true;

        _process.Exited += (_, _) =>
        {
            _ready.TrySetException(
                new Exception($"{_executable} exited before becoming READY."));
        };

        _process.BeginOutputReadLine();
        _process.BeginErrorReadLine();
    }
}
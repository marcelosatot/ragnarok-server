namespace RagLauncher.Logging;

internal sealed class LogService
{
    private readonly List<LogMessage> _messages = new();

    public IReadOnlyList<LogMessage> Messages => _messages;

    public event Action<LogMessage>? MessageReceived;

    public void Write(
        LogLevel level,
        string source,
        string message)
    {
        var log = new LogMessage
        {
            Level = level,
            Source = source,
            Message = message
        };

        _messages.Add(log);

        if (_messages.Count > 500)
            _messages.RemoveAt(0);

        MessageReceived?.Invoke(log);
    }
}
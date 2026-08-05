namespace RagLauncher.Logging;

internal sealed class LogMessage
{
    public DateTime Time { get; init; } = DateTime.Now;

    public string Source { get; init; } = "";

    public string Message { get; init; } = "";

    public LogLevel Level { get; init; }
}
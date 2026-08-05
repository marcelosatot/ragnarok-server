namespace RagLauncher.Core.Events;

internal sealed class LauncherEvent
{
    public required DateTime Time { get; init; }

    public required string Source { get; init; }

    public required string Message { get; init; }
}
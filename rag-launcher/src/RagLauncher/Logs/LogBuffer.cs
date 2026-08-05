using RagLauncher.Core.Events;

namespace RagLauncher.Logs;

internal sealed class LogBuffer
{
    private readonly Queue<LauncherEvent> _events = new();

    public IReadOnlyCollection<LauncherEvent> Events => _events;

    public void Add(LauncherEvent e)
    {
        _events.Enqueue(e);

        while (_events.Count > 300)
            _events.Dequeue();
    }
}
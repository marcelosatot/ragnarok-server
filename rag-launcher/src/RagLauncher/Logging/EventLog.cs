namespace RagLauncher.Logging;

internal sealed class EventLog
{
    private const int MaxEvents = 10;

    private readonly Queue<string> _events = new();

    public void Add(string message)
    {
        var line = $"{DateTime.Now:HH:mm:ss} {message}";

        _events.Enqueue(line);

        while (_events.Count > MaxEvents)
        {
            _events.Dequeue();
        }
    }

    public IReadOnlyCollection<string> GetEvents()
    {
        return _events.ToArray();
    }
}
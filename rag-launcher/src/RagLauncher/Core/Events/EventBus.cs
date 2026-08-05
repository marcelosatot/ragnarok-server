namespace RagLauncher.Core.Events;

internal sealed class EventBus
{
    public event Action<LauncherEvent>? EventReceived;

    public void Publish(
        string source,
        string message)
    {
        EventReceived?.Invoke(new LauncherEvent
        {
            Time = DateTime.Now,
            Source = source,
            Message = message
        });
    }
}
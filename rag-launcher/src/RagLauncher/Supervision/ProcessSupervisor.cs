namespace RagLauncher.Supervision;

internal sealed class ProcessSupervisor
{
    public async Task WatchAsync(
        Func<bool> isAlive,
        Func<Task> restart)
    {
        while (true)
        {
            if (!isAlive())
            {
                await Task.Delay(5000);

                await restart();
            }

            await Task.Delay(1000);
        }
    }
}
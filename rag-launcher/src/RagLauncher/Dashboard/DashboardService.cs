using RagLauncher.Core;

namespace RagLauncher.Dashboard;

internal sealed class DashboardService
{
    private readonly DashboardRenderer _renderer = new();

    public async Task StartAsync(LauncherContext context)
    {
        while (true)
        {
            _renderer.Render(context);

            await Task.Delay(1000);
        }
    }
}
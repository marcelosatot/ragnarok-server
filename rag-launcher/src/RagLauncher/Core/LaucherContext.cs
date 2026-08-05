using RagLauncher.Models;
using RagLauncher.Runtime;

namespace RagLauncher.Core;

internal sealed class LauncherContext
{
    public ServerConfiguration? Configuration { get; set; }

    public RuntimeStatistics Runtime { get; } = new();

    public DateTime StartedAt { get; } = DateTime.Now;
}
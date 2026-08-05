namespace RagLauncher.Runtime;

internal sealed class RuntimeState
{
    public bool LoginOnline { get; set; }

    public bool CharOnline { get; set; }

    public bool MapOnline { get; set; }

    public int PlayersOnline { get; set; }

    public DateTime StartedAt { get; } = DateTime.Now;

    public long LoginMemory { get; set; }

    public long CharMemory { get; set; }

    public long MapMemory { get; set; }
}
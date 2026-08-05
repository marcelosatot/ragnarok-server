namespace RagLauncher.Runtime;

internal sealed class RuntimeState
{
    // Status dos serviços
    public bool DatabaseOnline { get; set; }

    public bool LoginOnline { get; set; }

    public bool CharOnline { get; set; }

    public bool MapOnline { get; set; }

    // Memória (compatibilidade com o Dashboard)
    public long LoginMemory { get; set; }

    public long CharMemory { get; set; }

    public long MapMemory { get; set; }

    // Memória total do launcher
    public double MemoryMB { get; set; }

    // Runtime
    public DateTime StartedAt { get; set; } = DateTime.Now;

    public TimeSpan Uptime { get; set; }

    public int PlayersOnline { get; set; }

    public int RestartCount { get; set; }
}
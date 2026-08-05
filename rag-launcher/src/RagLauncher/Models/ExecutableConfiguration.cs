namespace RagLauncher.Models;

internal class ExecutableConfiguration
{
    public ServerDefinition Login { get; set; } = new();

    public ServerDefinition Char { get; set; } = new();

    public ServerDefinition Map { get; set; } = new();
}
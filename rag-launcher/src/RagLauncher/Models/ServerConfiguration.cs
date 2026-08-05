namespace RagLauncher.Models;

internal class ServerConfiguration
{
    public string ServerName { get; set; } = "";

    public string Version { get; set; } = "";

    public string RathenaDirectory { get; set; } = "";

    public ExecutableConfiguration Executables { get; set; } = new();
}
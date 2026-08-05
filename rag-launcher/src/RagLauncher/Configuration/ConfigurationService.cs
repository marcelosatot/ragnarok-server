using System.Text.Json;
using RagLauncher.Models;

namespace RagLauncher.Configuration;

internal class ConfigurationService
{
    public ServerConfiguration Load()
    {
        Console.WriteLine("[Configuration] Loading configuration...");

        var path = Path.Combine(
            AppContext.BaseDirectory,
            "config",
            "launcher.json");

        if (!File.Exists(path))
            throw new FileNotFoundException(path);

        var json = File.ReadAllText(path);

        var configuration =
            JsonSerializer.Deserialize<ServerConfiguration>(json);

        return configuration!;
    }
}
using RagLauncher.Models;
using RagLauncher.Services;

namespace RagLauncher.Validation;

internal class ValidationService
{
    public void Validate(ServerConfiguration configuration)
    {
        Console.WriteLine("[Validation] Validating installation...");

        configuration.RathenaDirectory =
            PathResolver.Resolve(configuration.RathenaDirectory);

        Console.WriteLine($"[Validation] rAthena : {configuration.RathenaDirectory}");

        if (!Directory.Exists(configuration.RathenaDirectory))
        {
            throw new DirectoryNotFoundException(
                $"Directory not found: {configuration.RathenaDirectory}");
        }

        Console.WriteLine("[Validation] OK");
    }
}
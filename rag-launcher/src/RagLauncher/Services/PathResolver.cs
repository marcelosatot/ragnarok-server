namespace RagLauncher.Services;

internal static class PathResolver
{
    public static string Resolve(string path)
    {
        var candidate = Path.Combine(AppContext.BaseDirectory, path);

        if (Directory.Exists(candidate))
            return candidate;

        throw new DirectoryNotFoundException(
            $"Directory '{candidate}' was not found.");
    }
}
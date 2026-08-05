namespace RagLauncher.Services;

internal static class PathResolver
{
    public static string Resolve(string path)
    {
        var current = new DirectoryInfo(Environment.CurrentDirectory);

        while (current != null)
        {
            var candidate = Path.Combine(current.FullName, path);

            if (Directory.Exists(candidate))
                return candidate;

            current = current.Parent;
        }

        throw new DirectoryNotFoundException(
            $"Unable to locate '{path}' starting from '{Environment.CurrentDirectory}'.");
    }
}
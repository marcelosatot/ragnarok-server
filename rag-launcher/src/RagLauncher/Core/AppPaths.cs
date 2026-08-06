namespace RagLauncher.Core;

internal static class AppPaths
{
    public static string Root =>
        AppContext.BaseDirectory;

    public static string MariaDb =>
        Path.Combine(Root, "mariadb");

    public static string Rathena =>
        Path.Combine(Root, "server");

    public static string Backup =>
        Path.Combine(Root, "backup");

    public static string Logs =>
        Path.Combine(Root, "logs");
}
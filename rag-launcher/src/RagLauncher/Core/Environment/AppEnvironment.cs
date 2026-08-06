namespace RagLauncher.Core.Environment;

internal sealed class AppEnvironment
{
    public string Root { get; }

    public string MariaDb { get; }

    public string Rathena { get; }

    public AppEnvironment()
    {
        Root = AppContext.BaseDirectory;

        MariaDb = Path.Combine(Root, "mariadb");

        Rathena = Root;
    }
}
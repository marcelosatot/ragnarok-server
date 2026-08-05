namespace RagLauncher.Core.Environment;

internal sealed class AppEnvironment
{
    public string Root { get; }

    public string MariaDb { get; }

    public string Rathena { get; }

    public AppEnvironment()
    {
        Root = Directory.GetParent(AppContext.BaseDirectory)!
                        .Parent!
                        .Parent!
                        .Parent!
                        .FullName;

        MariaDb = Path.Combine(Root, "mariadb");

        Rathena = Path.Combine(Root, "rathena");
    }
}
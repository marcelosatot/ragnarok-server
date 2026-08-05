using MySqlConnector;

namespace RagLauncher.Database;

internal class DatabaseInstaller
{
    private const string RootConnection =
        "Server=127.0.0.1;Port=3306;User ID=root;Password=;";

    public async Task EnsureDatabaseAsync()
    {
        Console.WriteLine("[Database] Checking database...");

        await using var connection =
            new MySqlConnection(RootConnection);

        await connection.OpenAsync();

        await ExecuteAsync(connection,
            """
            CREATE DATABASE IF NOT EXISTS ragnarok
            CHARACTER SET utf8mb4
            COLLATE utf8mb4_unicode_ci;
            """);

        await ExecuteAsync(connection,
            """
            CREATE USER IF NOT EXISTS 'ragnarok'@'localhost'
            IDENTIFIED BY 'ragnarok';
            """);

        await ExecuteAsync(connection,
            """
            GRANT ALL PRIVILEGES
            ON ragnarok.*
            TO 'ragnarok'@'localhost';
            """);

        await ExecuteAsync(connection,
            "FLUSH PRIVILEGES;");

        Console.WriteLine("[Database] Database OK");

        await ImportSchemaAsync();
    }

    private async Task ImportSchemaAsync()
{
    var connectionString =
        "Server=127.0.0.1;Port=3306;Database=ragnarok;User ID=root;Password=;";

    await using var connection =
        new MySqlConnection(connectionString);

    await connection.OpenAsync();

    // Já existe a tabela login?
    var check =
        new MySqlCommand(
            """
            SELECT COUNT(*)
            FROM information_schema.tables
            WHERE table_schema='ragnarok'
            AND table_name='login'
            """,
            connection);

    var exists = Convert.ToInt32(
        await check.ExecuteScalarAsync());

    if (exists > 0)
    {
        Console.WriteLine("[Schema] Already installed.");

        return;
    }

    var runner = new SqlScriptRunner();

    var root =
        @"C:\Users\satom\Documents\ragnarok-server\rathena\sql-files";

    await runner.ExecuteFileAsync(
        connection,
        Path.Combine(root, "main.sql"));

    await runner.ExecuteFileAsync(
        connection,
        Path.Combine(root, "logs.sql"));

    Console.WriteLine("[Schema] Installation completed.");
}

    private static async Task ExecuteAsync(
        MySqlConnection connection,
        string sql)
    {
        await using var command =
            new MySqlCommand(sql, connection);

        await command.ExecuteNonQueryAsync();
    }
}
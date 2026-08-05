using MySqlConnector;

namespace RagLauncher.Database;

internal class DatabaseInstaller
{
    public async Task EnsureDatabaseAsync()
    {
        Console.WriteLine("[Database] Checking database...");

        var connectionString =
            "Server=127.0.0.1;Port=3306;User ID=root;Password=;";

        await using var connection = new MySqlConnection(connectionString);

        await connection.OpenAsync();

        await using var command = new MySqlCommand(
            """
            CREATE DATABASE IF NOT EXISTS ragnarok
            CHARACTER SET utf8mb4
            COLLATE utf8mb4_unicode_ci;
            """,
            connection);

        await command.ExecuteNonQueryAsync();

        Console.WriteLine("[Database] Database 'ragnarok' OK");
    }
}
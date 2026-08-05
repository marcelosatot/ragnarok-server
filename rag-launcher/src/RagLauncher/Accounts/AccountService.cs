using MySqlConnector;

namespace RagLauncher.Accounts;

internal class AccountService
{
    private const string ConnectionString =
        "Server=127.0.0.1;Port=3306;Database=ragnarok;User ID=root;Password=;";

    public async Task EnsureAdminAccountAsync()
    {
        await using var connection = new MySqlConnection(ConnectionString);

        await connection.OpenAsync();

        const string checkSql =
        """
        SELECT COUNT(*)
        FROM login
        WHERE userid='admin'
        """;

        await using var check = new MySqlCommand(checkSql, connection);

        var exists = Convert.ToInt32(await check.ExecuteScalarAsync());

        if (exists > 0)
        {
            Console.WriteLine("[Account] Admin already exists.");
            return;
        }

        const string insertSql =
        """
        INSERT INTO login
        (
            userid,
            user_pass,
            sex,
            email,
            group_id
        )
        VALUES
        (
            @user,
            @pass,
            @sex,
            @mail,
            @group
        )
        """;

        await using var cmd = new MySqlCommand(insertSql, connection);

        cmd.Parameters.AddWithValue("@user", "admin");
        cmd.Parameters.AddWithValue("@pass", "admin");
        cmd.Parameters.AddWithValue("@sex", "M");
        cmd.Parameters.AddWithValue("@mail", "admin@localhost");
        cmd.Parameters.AddWithValue("@group", 99);

        await cmd.ExecuteNonQueryAsync();

        Console.WriteLine("[Account] Admin account created.");
    }
}